using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QrCodeGenerator
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void generatebtn_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(param.Text) || String.IsNullOrWhiteSpace(param.Text)) { MessageBox.Show("Error text cannot be empty"); return; }
            WebClient web = new WebClient();
            string url = $"https://chart.googleapis.com/chart?chs=400x400&cht=qr&chl={param.Text}";
            web.DownloadDataCompleted += (g, k) => {
                using (var ms = new MemoryStream(k.Result))
                {
                    preview.BackgroundImage = Image.FromStream(ms);
                }
            };
            web.DownloadDataAsync(new Uri(url));
        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Filter = "png|*.png|all files|*.*";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    preview.BackgroundImage.Save(dlg.FileName);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            savebtn.Enabled = preview.BackgroundImage!=null;
        }
    }
}
