using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.SqlServer.Management.Smo;

namespace SPGen2008.Components.UI.ASPX
{
    class Gen_Table_JQGrid : IGenComponent
    {
        // todo: 多主键支持

        #region Init

        public Gen_Table_JQGrid()
        {
            this._properties.Add(GenProperties.Name, "Gen_Table_JQGrid");
            this._properties.Add(GenProperties.Caption, "JQGrid");
            this._properties.Add(GenProperties.Group, "ASP.NET");
            this._properties.Add(GenProperties.Tips, "为 Table 生成 JQGrid 的 JS 代码及 ASHX 后台");
        }
        public SqlElementTypes TargetSqlElementType
        {
            get { return SqlElementTypes.Table; }
        }

        #endregion

        #region Misc

        Dictionary<GenProperties, object> _properties = new Dictionary<GenProperties, object>();
        public Dictionary<GenProperties, object> Properties
        {
            get
            {
                return this._properties;
            }
        }

        public event System.ComponentModel.CancelEventHandler OnProcessing;

        private Server _server;
        public Server Server
        {
            set { _server = value; }
        }

        private Database _db;
        public Database Database
        {
            set { _db = value; }
        }

        #endregion

        public string JsEscape(string s)
        {
            return s.Replace(@"'", @"\'").Replace(@"""", @"\"""); ;
        }

        public bool Validate(params object[] sqlElements)
        {
            Table t = (Table)sqlElements[0];

            return Utils.GetPrimaryKeyColumns(t).Count > 0;
        }

        public GenResult Gen(params object[] sqlElements)
        {
            #region Init

            GenResult gr;
            Table t = (Table)sqlElements[0];

            List<Column> pks = Utils.GetPrimaryKeyColumns(t);

            if (pks.Count == 0)
            {
                gr = new GenResult(GenResultTypes.Message);
                gr.Message = "无法为没有主键字段的表生成该UI代码！";
                return gr;
            }

            List<Column> wcs = Utils.GetWriteableColumns(t);
            List<Column> socs = Utils.GetSortableColumns(t);
            List<Column> sacs = Utils.GetSearchableColumns(t);

            List<Column> ocs = new List<Column>();                      // 实际输出的字段集
            foreach (Column c in t.Columns) if (Utils.CheckIsBinaryType(c)) continue; else ocs.Add(c);      // 过滤掉不输出的字段

            // find pk's index
            int pkcidx = 0;
            for (int i = 0; i < ocs.Count; i++)
            {
                if (ocs[i].InPrimaryKey) { pkcidx = i; break; }
            }
            Column pkc = ocs[pkcidx];


            StringBuilder sb = new StringBuilder();

            #endregion

            #region Gen

            string tbn = t.Name;


            sb.Append(@"
<script type=""text/javascript"">
    $().ready(function() {
        

        $(""#table"").jqGrid({
            pager       : $(""#pager""),

            url         : ""query.ashx"",
            caption     : ""JSON Mapping"",
            sortname    : """ + JsEscape(pkc.Name) + @""",
            sortorder   : ""desc"",
            rowNum      : 10,
            imgpath     : ""css/images"",


            colModel    : [");


            for (int i = 0; i < ocs.Count; i++)
            {
                Column c = ocs[i];

                string caption = Utils.GetCaption(c);
                string cn = JsEscape(c.Name);
                string width = "80";                                       // todo: 根据各种数据类型及其长度来推断出显示宽度
                string align = Utils.CheckIsNumericType(c) ? "right" : "left";
                string sortable = socs.Contains(c).ToString().ToLower();

                // todo: 格式化日期，货币显示

                sb.Append(@"
	            { label: """ + caption + @""", name: """ + cn + @""", index: """ + cn + @""", width: " + width + @", align: """ + align + @""", sortable: " + sortable + @" }");

                if (i < ocs.Count - 1) sb.Append(@",");
            }

            sb.Append(@"
            ],
            viewrecords : true,
            datatype    : ""json"",
            jsonReader  : {
	            repeatitems : false,
	            id          : """ + pkcidx.ToString() + @"""
            },
            rowList     : [10, 20, 30],
            height      : ""100%"",
            autowidth   : true

        }).navGrid(""#pager"", {edit: false, add: false, del: false});
    });
</script>



<table id=""table"" class=""scroll"" cellpadding=""0"" cellspacing=""0""></table>
<div id=""pager"" class=""scroll"" style=""text-align:center;""></div>




");

            #endregion

            #region return

            gr = new GenResult(GenResultTypes.CodeSegment);
            gr.CodeSegment = new KeyValuePair<string, string>(this._properties[GenProperties.Tips].ToString(), sb.ToString());
            return gr;

            #endregion
        }
    }
}
