using System;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace SSHomeRestoration
{
    public partial class Options : MaterialForm
    {
        public string filePath;
        public Options()
        {
            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);

        }



        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dR = savePath.ShowDialog();
            if (dR == DialogResult.OK)
            {

                filePath = savePath.SelectedPath.ToString();
                Opt options = new Opt();
                options.FilePath = filePath.ToString();
                options.Save();
                generalLogTb.Text = filePath;
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DialogResult dR = savePath.ShowDialog();
            if (dR == DialogResult.OK)
            {

                filePath = savePath.SelectedPath.ToString();
                Opt options = new Opt();
                options.generalLog = filePath.ToString();
                options.Save();
                generalLogTb.Text = filePath;
            }
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            Opt options = new Opt();
            options.Save();
        }
    }
}
