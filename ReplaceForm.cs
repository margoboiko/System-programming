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

        // ініціалізація тексту з яким працюємо
        public ReplaceForm(RichTextBox richTextBox)
        {
           
            __txtContent = richTextBox;
            InitializeComponent();
        }

        private void btnReplace_Click_1(object sender, EventArgs e)
        {
            // використовуємо регулярний вираз для заданої в полі пошуку маски
            Regex mask = new Regex(txtSearch.Text);

            // якщо заміна відбувається починаючи з початку
            if (btnStart.Checked)
            {
                // здійснюється перевірка співпадінь маски в тексті
                // якщо співпадінь немає
                var matches = Regex.Matches(__txtContent.Text, @"\b" + mask + @"\s*\b");
                if (matches.Count == 0)
                {
                    MessageBox.Show("Немає співпадінь!");
                    return;
                }

                // якщо співпадіння є
                int i = 0;
                int k = 0;
                foreach (Match m in matches)
                {
                    // коли знайшли перше співпадіння воно виділяється і відкривається діалогове вікно запиту
                    __txtContent.Select(m.Index + i, m.Length);
                    __txtContent.SelectionBackColor = Color.LightSalmon;
                    string message = "Ви впевнені що хочете замінити '" + m.ToString() + "'" + "?";

                    var result = MessageBox.Show(message, "Увага!",
                                     MessageBoxButtons.YesNo,
                                     MessageBoxIcon.Question);

                    // якщо ми згодні замінити підходяще слово - заміняємо
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

                    // якщо відхилити заміну
                    if (result == DialogResult.No)
                    {
                        __txtContent.SelectionBackColor = Color.White;
                    }

                }
            }

            if (btnCursor.Checked)
            {
                // використовуємо регулярний вираз для пошуку співпадінь з місця, де знаходиться курсор
                Regex r = new Regex(@"\b" + mask + @"\s*\b");

                // співпадінь немає
                if (r.Matches(__txtContent.Text, __txtContent.SelectionStart).Count == 0)
                {
                    MessageBox.Show("No matches!");
                    return;
                }

                // співпадіння є
                var matches = r.Matches(__txtContent.Text, __txtContent.SelectionStart);
                int i = 0;
                foreach (Match m in matches)
                {
                    __txtContent.Select(m.Index + i, m.Length);
                    __txtContent.SelectionBackColor = Color.Orange;
                    string message = "Ви впевнені що хочете замінити '" + m.ToString() + "'" + "?";

                    var result = MessageBox.Show(message, "Увага!",
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
            // використовуємо регулярний вираз для заданої в полі пошуку маски
            Regex mask = new Regex(txtSearch.Text);

            // здійснюється перевірка співпадінь маски в тексті
            // якщо співпадінь немає
            var matches = Regex.Matches(__txtContent.Text, @"\b" + mask + @"\s*\b");
            if (matches.Count == 0)
            {
                MessageBox.Show("Немає співпадінь!");
                return;
            }

            // якщо співпадіння є
            int i = 0;
            foreach (Match m in matches)
            {
                __txtContent.Select(m.Index + i, m.Length);
                __txtContent.SelectionBackColor = Color.Red;

            }
            string message = "Замінити все?";

            var result = MessageBox.Show(message, "Увага!",
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

        // натискаючи на "Закрити" згортаємо форму заміни
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

 
}
