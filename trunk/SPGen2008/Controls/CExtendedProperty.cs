using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Microsoft.SqlServer.Management.Smo;

namespace SPGen2008
{
    public partial class CExtendedProperty : UserControl
    {
        public CExtendedProperty(ExtendedProperty ep)
        {
            InitializeComponent();
        }
    }
}
