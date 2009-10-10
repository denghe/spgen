using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Smo;

namespace SPGen2008.Components.Selector
{
    public partial class FSelector_Columns : Form
    {
        protected Table _t = null;

        public FSelector_Columns(Table t)
        {
            InitializeComponent();

            _t = t;
        }

        private void FSelector_Columns_Load(object sender, EventArgs e)
        {
            if (_t != null)
            {
                List<string> ucns = new List<string>();
                foreach (Index idx in _t.Indexes)
                {
                    //idx.IsUnique
                    foreach (IndexedColumn idxc in idx.IndexedColumns)
                    {
                        ucns.Add(idxc.Name);
                    }
                }

                foreach (Column c in _t.Columns)
                {
                    int i = _DataGridView.Rows.Add((c.InPrimaryKey ? Properties.Resources.SQL_Key : (c.IsForeignKey ? Properties.Resources.SQL_ForeignKey : Properties.Resources.SQL_Empty)), c.Name, Utils.GetCaption(c), Utils.GetDescription(c), c.DataType.Name, c.DataType.MaximumLength, c.Nullable, c.InPrimaryKey || ucns.Contains(c.Name));
                    _DataGridView.Rows[i].Tag = c;
                }



            }
            
        }
    }
}
