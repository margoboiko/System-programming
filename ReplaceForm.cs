using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Replace
{
    
    public partial class ReplaceForm : Form
    {
        private RichTextBox __txtContent;
        private RegularCheck wordChecker = new RegularCheck();
      

        public void ReplaceForm_Load(object sender, EventArgs e) {
            
        }

        public ReplaceForm(RichTextBox richTextBox)
        {
           
            __txtContent = richTextBox;
            InitializeComponent();
        }

        
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

            bool isEmpty = txtSearch.Text.Length == 0;

            btnReplace.Enabled = !isEmpty;
            btnReplaceAll.Enabled = !isEmpty;

        }

        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReplace_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show(txtSearch.Text);

            Regex mask = new Regex(txtSearch.Text);


            if (btnStart.Checked)
            {
                var matches = Regex.Matches(__txtContent.Text, @"\b" + mask + @"\s*\b");
                if (matches.Count == 0)
                {
                    MessageBox.Show("No matches!");
                    return;
                }

                int i = 0;
                int k = 0;
                foreach (Match m in matches)
                {
                    __txtContent.Select(m.Index + i, m.Length);
                    __txtContent.SelectionBackColor = Color.DeepSkyBlue;
                    string message = "Replace '" + m.ToString() + "'" + "?";

                    var result = MessageBox.Show(message, "Waring!",
                                     MessageBoxButtons.YesNo,
                                     MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        if (m.Length > txtReplace.Text.Length)
                            i = txtReplace.Text.Length - m.Length + 1;
                        else if (m.Length < txtReplace.Text.Length)
                        {
                            i += txtReplace.Text.Length - m.Length + k;
                            k += 2;
                        }

                        __txtContent.SelectedText = txtReplace.Text + " ";
                        __txtContent.SelectionBackColor = Color.White;
                    }

                    if (result == DialogResult.No)
                    {
                        __txtContent.SelectionBackColor = Color.White;
                    }

                }
            }

            if (btnCursor.Checked)
            {
                Regex r = new Regex(@"\b" + mask + @"\s*\b");
                if (r.Matches(__txtContent.Text, __txtContent.SelectionStart).Count == 0)
                {
                    MessageBox.Show("No matches!");
                    return;
                }
                var matches = r.Matches(__txtContent.Text, __txtContent.SelectionStart);
                int i = 0;
                foreach (Match m in matches)
                {
                    __txtContent.Select(m.Index + i, m.Length);
                    __txtContent.SelectionBackColor = Color.Violet;
                    string message = "Replace '" + m.ToString() + "'" + "?";

                    var result = MessageBox.Show(message, "Waring!",
                                     MessageBoxButtons.YesNo,
                                     MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        if (m.Length > txtReplace.Text.Length)
                            i = txtReplace.Text.Length - m.Length + 1;
                        else if (m.Length < txtReplace.Text.Length)
                            i = Math.Abs(m.Length - txtReplace.Text.Length) + 1;
                        __txtContent.SelectedText = txtReplace.Text + " ";
                        __txtContent.SelectionBackColor = Color.White;
                    }
                    if (result == DialogResult.No)
                    {
                        __txtContent.SelectionBackColor = Color.White;
                    }

                }

            }

        }

        private void btnReplaceAll_Click(object sender, EventArgs e)
        {
            MessageBox.Show(txtSearch.Text); //

            Regex mask = new Regex(txtSearch.Text);
            var matches = Regex.Matches(__txtContent.Text, @"\b" + mask + @"\s*\b");
            if (matches.Count == 0)
            {
                MessageBox.Show("Немає співпадінь");
                return;
            }

            int i = 0;
            foreach (Match m in matches)
            {
                __txtContent.Select(m.Index + i, m.Length);
                __txtContent.SelectionBackColor = Color.Red;

            }
            string message = "Replace all?";

            var result = MessageBox.Show(message, "Waring!",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string res = Regex.Replace(__txtContent.Text, @"\b" + mask + @"\s*\b", txtReplace.Text + " ");
                __txtContent.Text = res;
                __txtContent.SelectionBackColor = Color.White;
            }
            if (result == DialogResult.No)
            {
                __txtContent.SelectionBackColor = Color.White;
            }
        }
    }
 
}