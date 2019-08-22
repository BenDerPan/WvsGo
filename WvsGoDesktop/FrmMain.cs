using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WvsGo.GraphViewer;

namespace WvsGoDesktop
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void MuOpenXml_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Filter = "Wvs Xml扫描结果(*.xml)|*.xml";
            dialog.CheckFileExists = true;
            if (dialog.ShowDialog()==DialogResult.OK)
            {
                var file = dialog.FileName;
                var obj= graphViewer.LoadNodesFromXml(file);
            }
            
        }
    }
}
