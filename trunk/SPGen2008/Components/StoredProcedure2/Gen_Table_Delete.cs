﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

// SMO
using Microsoft.SqlServer;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using SPGen2008.Components.Selector;

namespace SPGen2008.Components.StoredProdcedure2
{
	public class Gen_Table_Delete : IGenComponent
	{
		#region Init

		public Gen_Table_Delete()
		{
			this._properties.Add(GenProperties.Name, "Gen_Table_Delete");
			this._properties.Add(GenProperties.Caption, "删除单条");
			this._properties.Add(GenProperties.Group, "生成过程脚本_DELETE类（RETURN带错误返回）");
			this._properties.Add(GenProperties.Tips, "生成根据主键删除单条数据的存储过程");
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

			return !(pks.Count == 0);
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
				gr.Message = "无法为没有主键字段的表生成该过程！";
				return gr;
			}

			StringBuilder sb = new StringBuilder();

			#endregion

            using (FSelector_Columns f = new FSelector_Columns(t))
            {
                f.ShowDialog();
            }

			#region Gen

			sb.Append(@"
-- 针对 表 " + t.ToString() + @"
-- 根据主键值删除一行数据
-- 操作成功返回 受影响行数, 失败返回
-- -1: 主键为空
-- -2: 主键未找到
-- -3: 删除失败
CREATE PROCEDURE [" + Utils.GetEscapeSqlObjectName(t.Schema) + @"].[usp_" + Utils.GetEscapeSqlObjectName(t.Name) + @"_Delete] (");
			for (int i = 0; i < pks.Count; i++)
			{
				Column c = pks[i];
				string cn = Utils.GetEscapeName(c);
				sb.Append(@"
    " + (i > 0 ? ", " : "  ") + Utils.FormatString("@Original_" + cn, Utils.GetParmDeclareStr(c), 40));
			}
			sb.Append(@"
) AS
BEGIN
    SET NOCOUNT ON;
");
			for (int i = 0; i < pks.Count; i++)
			{
				Column c = pks[i];
				string cn = Utils.GetEscapeName(c);
				sb.Append(@"
    IF @Original_" + cn + @" IS NULL RETURN -1;
");
			}

            string s = "";                                      // 构造个主键条件判断出来
            for (int i = 0; i < pks.Count; i++)
            {
                Column c = pks[i];
                string cn = Utils.GetEscapeName(c);
                if (i > 0) s += " AND ";
                s += @"[" + Utils.GetEscapeSqlObjectName(c.Name) + @"] = @Original_" + cn;
            }

			sb.Append(@"

    IF NOT EXISTS (
        SELECT 1 
          FROM [" + Utils.GetEscapeSqlObjectName(t.Schema) + @"].[" + Utils.GetEscapeSqlObjectName(t.Name) + @"]
--        WITH (UPDLOCK)
         WHERE " + s+ @"
    ) RETURN -2;

/*
    --prepare trans & error
    DECLARE @TranStarted bit, @ReturnValue int;
    SELECT @TranStarted = 0, @ReturnValue = 0;
    IF @@TRANCOUNT = 0 
    BEGIN
        BEGIN TRANSACTION;
        SET @TranStarted = 1
    END;
*/

    BEGIN TRY
        DELETE FROM [" + Utils.GetEscapeSqlObjectName(t.Schema) + @"].[" + Utils.GetEscapeSqlObjectName(t.Name) + @"]
    --    FROM [" + Utils.GetEscapeSqlObjectName(t.Schema) + @"].[" + Utils.GetEscapeSqlObjectName(t.Name) + @"]");
    			
			    sb.Append(@"
    --  OUTPUT Inserted.*	-- 取消注释可返回该行（SQL2005+）");
			    if (s.Length > 0) sb.Append(@"
         WHERE " + s);
			    sb.Append(@"

/*
        @ReturnValue = @@ROWCOUNT;
        GOTO Cleanup;
*/
        RETURN @@ROWCOUNT;

    END TRY
    BEGIN CATCH
/*
        @ReturnValue = -3;
        GOTO Cleanup;
*/
        RETURN -3;
    END CATCH

/*
    --cleanup trans
    IF @TranStarted = 1 COMMIT TRANSACTION;
    RETURN @ReturnValue;
Cleanup:
    IF @TranStarted = 1 ROLLBACK TRANSACTION;
    RETURN @ReturnValue;
*/

    RETURN @@ROWCOUNT;

END

-- 下面这几行用于生成智能感知代码，以及强类型返回值，请注意同步修改（SP名称，备注，返回值类型）

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'针对 表 " + t.ToString() + @"
根据主键值删除一行数据
操作成功返回 受影响行数, 失败返回
-1: 主键为空; 
-2: 主键未找到; 
-3: 删除失败' , @level0type=N'SCHEMA',@level0name=N'" + t.Schema + @"', @level1type=N'PROCEDURE',@level1name=N'usp_" + Utils.GetEscapeSqlObjectName(t.Name) + @"_Delete'
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
