using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Replace
{
	public partial class Form1 : Form
	{

        //ініціалізація компонентів
		public Form1()
		{
			InitializeComponent();
		}

        public void Form1_Load(object sender, EventArgs e) {
        }

        //створюємо форму для заміни і зв'язуємо їх
        private void btnRepl_Click_1(object sender, EventArgs e)
        {
            ReplaceForm replaceForm = new ReplaceForm(txtContent);
            replaceForm.Show();
        }

        private void btnFile_Click_1(object sender, EventArgs e)
        {
            // відкриваємо діалогове вікно та обираємо потрібний документ
            Stream inputFIle;
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.InitialDirectory = Path.GetFullPath("../../../");
            fileDialog.Filter = "txt files (*.txt)|*.txt";
            fileDialog.RestoreDirectory = true;

            //обраний документ зчитується і його вміст відображається в текстоволу полі
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                inputFIle = fileDialog.OpenFile();
                StreamReader reader = new StreamReader(inputFIle);
                txtContent.Text = reader.ReadToEnd();
            }
        }

        //очистка текстового поля
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtContent.Clear();
        }
    }
}
