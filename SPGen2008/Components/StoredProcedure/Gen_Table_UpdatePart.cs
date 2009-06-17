﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

// SMO
using Microsoft.SqlServer;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace SPGen2008.Components.StoredProdcedure
{
    public class Gen_Table_UpdatePart : IGenComponent
    {
        #region Init

        public Gen_Table_UpdatePart()
        {
            this._properties.Add(GenProperties.Name, "Gen_Table_UpdatePart");
            this._properties.Add(GenProperties.Caption, "更新单条（部分字段）");
            this._properties.Add(GenProperties.Group, "生成过程脚本_UPDATE类");
            this._properties.Add(GenProperties.Tips, "生成更新单条数据部分字段的存储过程");
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

        public bool Validate(params object[] sqlElements)
        {
            Table t = (Table)sqlElements[0];

            List<Column> pks = Utils.GetPrimaryKeyColumns(t);
            List<Column> wcs = Utils.GetWriteableColumns(t);

            return !(wcs.Count == 0 || pks.Count == 0);
        }

        public GenResult Gen(params object[] sqlElements)
        {
            #region Init

            GenResult gr;
            Table t = (Table)sqlElements[0];


            List<Column> pks = Utils.GetPrimaryKeyColumns(t);
            List<Column> wcs = Utils.GetWriteableColumns(t);
            List<Column> mwcs = Utils.GetMustWriteColumns(t);

            if (wcs == null || wcs.Count == 0)
            {
                gr = new GenResult(GenResultTypes.Message);
                gr.Message = "无法为没有可写入字段的表生成该过程！";
                return gr;
            }
            if (pks == null || pks.Count == 0)
            {
                gr = new GenResult(GenResultTypes.Message);
                gr.Message = "无法为没有主键字段的表生成该过程！";
                return gr;
            }

            StringBuilder sb = new StringBuilder();

            #endregion

            #region Gen

            sb.Append(@"
-- 针对 表 " + t.ToString() + @"
-- 根据主键的值定位一行数据并修改有传入的部分字段值
CREATE PROCEDURE [" + Utils.GetEscapeSqlObjectName(t.Schema) + @"].[usp_" + Utils.GetEscapeSqlObjectName(t.Name) + @"_Update] (");
            for (int i = 0; i < pks.Count; i++)
            {
                Column c = pks[i];
                string cn = Utils.GetEscapeName(c);
                sb.Append(@"
    " + (i > 0 ? ", " : "  ") + Utils.FormatString("@Original_" + cn, Utils.GetParmDeclareStr(c), 40));
            }
            for (int i = 0; i < wcs.Count; i++)
            {
                Column c = wcs[i];
                string cn = Utils.GetEscapeName(c);
                sb.Append(@"
    , " + Utils.FormatString("@" + cn, Utils.GetParmDeclareStr(c), "= NULL", 40, 40));
            }
            sb.Append(@"
) AS
BEGIN
    SET NOCOUNT ON;
");
            //判断原始主键必填
            for (int i = 0; i < pks.Count; i++)
            {
                Column c = pks[i];
                string cn = Utils.GetEscapeName(c);
                string cc = Utils.GetCaption(c);
                sb.Append(@"
    IF @Original_" + cn + @" IS NULL
    BEGIN
        RAISERROR ('" + t.Schema + @"." + t.Name + @".Update|Required.Original_" + c.Name + @" 必须填写 原始的" + cc + @"值', 11, 1); RETURN -1;
    END;
");
            }

            //判断原始主键必存在
            sb.Append(@"
	-- 这里也可以先不判断原记录是否存在，直接取值，并任意判断一个非空值是否取到
    IF NOT EXISTS (
        SELECT 1 FROM [" + Utils.GetEscapeSqlObjectName(t.Schema) + @"].[" + Utils.GetEscapeSqlObjectName(t.Name) + @"]
--          WITH (UPDLOCK)
         WHERE ");
            for (int i = 0; i < pks.Count; i++)
            {
                Column c = pks[i];
                string cn = Utils.GetEscapeName(c);
                if (i > 0) sb.Append(@" AND ");
                sb.Append(@"[" + Utils.GetEscapeSqlObjectName(c.Name) + @"] = @Original_" + cn);
            }
            sb.Append(@"
    ) 
    BEGIN
        RAISERROR ('" + t.Schema + @"." + t.Name + @".Update|NotFound.Original_PrimaryKeys 原主键值对应的数据未找到', 11, 1); RETURN -1;
    END;
");

            // 从原数据行读出必填字段原始值并锁定

            // 声明字段原始值
            string s = "";
            foreach (Column c in wcs)
            {
                if (pks.Contains(c)) continue;            // 主键已传入了原始值
                if (c.Nullable) continue;
                string cn = Utils.GetEscapeName(c);
                string cc = Utils.GetCaption(c);
                if (mwcs.Contains(c))
                {
                    sb.Append(@"
    DECLARE @Original_" + cn + @" " + Utils.GetParmDeclareStr(c) + @";");

                    s += (s.Length > 0 ? ", " : "") + "@Original_" + cn + @" = [" + Utils.GetEscapeSqlObjectName(c.Name) + @"]";
                }
            }
            if (s.Length > 0)
            {
                sb.Append(@"
    SELECT " + s + @"
      FROM [" + Utils.GetEscapeSqlObjectName(t.Schema) + @"].[" + Utils.GetEscapeSqlObjectName(t.Name) + @"]
--      WITH (UPDLOCK)
     WHERE ");
                for (int i = 0; i < pks.Count; i++)
                {
                    Column c = pks[i];
                    string cn = Utils.GetEscapeName(c);
                    if (i > 0) sb.Append(@" AND ");
                    sb.Append(@"[" + Utils.GetEscapeSqlObjectName(c.Name) + @"] = @Original_" + cn);
                }
                sb.Append(@";");
            }

            //判断必填字段是否填写了空值
            foreach (Column c in wcs)
            {
                if (c.Nullable) continue;
                string cn = Utils.GetEscapeName(c);
                string cc = Utils.GetCaption(c);
                if (mwcs.Contains(c))
                {
                    sb.Append(@"
    IF @" + cn + @" IS NULL SET @" + cn + @" = @Original_" + cn + @";
    -- ELSE IF LEN(@" + cn + @") = 0
    -- BEGIN
    --     RAISERROR ('" + t.Schema + @"." + t.Name + @".Update|Required." + c.Name + @" 必须填写 " + cc + @"', 11, 1); RETURN -1;
    -- END;
");
                }
                else
                {
                    sb.Append(@"
    -- IF @" + cn + @" IS NULL SET @" + cn + @" = " + c.DefaultConstraint.Text + @";
");
                }
            }


            //判断新主键值是否和已有主键重复
            if (pks.Count == 1 && pks[0].Identity)
            {
            }
            else
            {
                sb.Append(@"
    IF ");
                bool isFirst = true;
                foreach (Column c in pks)
                {
                    if (c.Identity) continue;
                    string cn = Utils.GetEscapeName(c);
                    string cc = Utils.GetCaption(c);
                    if (!isFirst) sb.Append(@" OR ");
                    sb.Append(@"@Original_" + cn + @" <> @" + cn);
                    isFirst = false;
                }
                sb.Append(@"
    BEGIN
        IF EXISTS (
            SELECT 1 FROM [" + Utils.GetEscapeSqlObjectName(t.Schema) + @"].[" + Utils.GetEscapeSqlObjectName(t.Name) + @"]
             WHERE ");
                for (int i = 0; i < pks.Count; i++)
                {
                    Column c = pks[i];
                    string cn = Utils.GetEscapeName(c);
                    string cc = Utils.GetCaption(c);
                    if (i > 0) sb.Append(@" AND ");
                    sb.Append(@"[" + Utils.GetEscapeSqlObjectName(c.Name) + @"] = @Original_" + cn);
                }
                sb.Append(@"
        ) 
        RAISERROR ('" + t.Schema + @"." + t.Name + @".Update|Exists.PrimaryKeys 欲更新的主键值已存在', 11, 1); RETURN -1;
    END;
");
                //判断外键字段是否在外键表中存在
                foreach (ForeignKey fk in t.ForeignKeys)
                {
                    Table ft = t.Parent.Tables[fk.ReferencedTable, fk.ReferencedTableSchema];
                    sb.Append(@"
    IF NOT EXISTS (
        SELECT 1 FROM [" + Utils.GetEscapeSqlObjectName(ft.Schema) + @"].[" + Utils.GetEscapeSqlObjectName(ft.Name) + @"]
         WHERE ");
                    string s1 = "";
                    for (int i = 0; i < fk.Columns.Count; i++)
                    {
                        ForeignKeyColumn fkc = fk.Columns[i];
                        Column c = t.Columns[fkc.Name];
                        string cn = Utils.GetEscapeName(c);
                        string cc = Utils.GetCaption(c);

                        if (i > 0) sb.Append(@" AND ");
                        sb.Append("(" + (c.Nullable ? (" @" + cn + @" IS NULL OR ") : "") + @"[" + Utils.GetEscapeSqlObjectName(fkc.ReferencedColumn) + @"] = @" + cn + @")");
                        if (i > 0) s1 += "，";
                        s1 += Utils.GetCaption(t.Columns[fkc.Name]);
                    }
                    sb.Append(@"
    )
    BEGIN
        RAISERROR ('" + t.Schema + @"." + t.Name + @".Update|NotFound." + s1 + @" " + s1 + @" 的值在表：" + Utils.GetCaption(ft) + @" 中未找到', 11, 1); RETURN -1;
    END;
");
                }
            }

            sb.Append(@"
/*
    --prepare trans & error
    DECLARE @TranStarted bit; SET @TranStarted = 0; IF @@TRANCOUNT = 0 BEGIN BEGIN TRANSACTION; SET @TranStarted = 1 END;
*/

    UPDATE [" + Utils.GetEscapeSqlObjectName(t.Schema) + @"].[" + Utils.GetEscapeSqlObjectName(t.Name) + @"]
       SET ");
            for (int i = 0; i < wcs.Count; i++)
            {
                Column c = wcs[i];
                string cn = Utils.GetEscapeName(c);
                sb.Append((i > 0 ? @"
         , " : "") + Utils.FormatString("[" + Utils.GetEscapeSqlObjectName(c.Name) + @"]", "= @" + cn, 40));
            }
            s = "";
            for (int i = 0; i < pks.Count; i++)
            {
                Column c = pks[i];
                string cn = Utils.GetEscapeName(c);
                if (i > 0) s += " AND ";
                s += @"[" + Utils.GetEscapeSqlObjectName(c.Name) + @"] = @Original_" + cn;
            }
            if (s.Length > 0) sb.Append(@"
     WHERE " + s);
            sb.Append(@";
    IF @@ERROR <> 0 OR @@ROWCOUNT = 0
    BEGIN
        RAISERROR ('" + t.Schema + @"." + t.Name + @".Update|Failed 数据更新失败', 11, 1); RETURN -1;  -- GOTO Cleanup;
    END

/*
    --cleanup trans
    IF @TranStarted = 1 COMMIT TRANSACTION; RETURN 0;
Cleanup:
    IF @TranStarted = 1 ROLLBACK TRANSACTION; RETURN -1;
*/
    RETURN 0;

END


-- 下面这几行用于生成智能感知代码，以及强类型返回值，请注意同步修改（SP名称，备注，返回值类型）

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'针对 表 " + t.ToString() + @"
根据主键的值定位一行数据并修改有传入的部分字段值' , @level0type=N'SCHEMA',@level0name=N'" + t.Schema + @"', @level1type=N'PROCEDURE',@level1name=N'usp_" + Utils.GetEscapeSqlObjectName(t.Name) + @"_UpdatePart'

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
