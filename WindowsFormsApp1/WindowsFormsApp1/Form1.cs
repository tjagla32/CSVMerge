using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        OpenFileDialog dialog = new OpenFileDialog();

        unsafe void saveToFile(string read, string write, int *numberOfFile)
        {
           
            var lines = File.ReadAllLines(read);

            if (*numberOfFile != 1)
            {
                lines = lines.Skip(1).ToArray();
                File.AppendAllLines(write, lines);
            }
            else
            {
                File.WriteAllLines(write, lines);
            }
            *numberOfFile += 1;
        }


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void upload(object sender, EventArgs e)
        {
            
            dialog.Filter = "CSV files | *.csv"; // file types, that will be allowed to upload
            dialog.Multiselect = true; // allow/deny user to upload more than one file at a time
            dialog.Title = "Wybierz pliki *.CSV";
            if (dialog.ShowDialog() == DialogResult.OK) // if user clicked OK
            {
                String path = dialog.FileName; // get name of file
                using (StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open), new UTF8Encoding())) // do anything you want, e.g. read it
                {
                    for (int i = 0; i < dialog.SafeFileNames.Length; i++)
                        textBox3.Text += dialog.SafeFileNames[i] 
                            + Environment.NewLine;
                }
                textBox3.Text += Environment.NewLine + Environment.NewLine;
            }
        }

         public unsafe void find(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            int i = 1; //numer pliku
            int* ptr = &i; //wskaźnik - w funkcji liczy, który plik jest obsługiwany
            


            string out_path = Path.GetDirectoryName(dialog.FileNames[0]);
            string write_file = out_path + @"\" + textBox2.Text + ".csv";

            textBox3.Text += "Dane będą zapisane w: " + Environment.NewLine + write_file + Environment.NewLine + Environment.NewLine;


            for (int n = 0; n < dialog.SafeFileNames.Length; n++)
            {
                textBox3.Text += "Scalam plik nr " + (n+1) + ": " + Environment.NewLine;
                textBox3.Text += dialog.FileNames[n] + Environment.NewLine + Environment.NewLine;
                saveToFile(dialog.FileNames[n], write_file, ptr);
            }

            textBox1.Text = "Zakończono scalanie plików!";

            button3.Visible = true;
            button1.Enabled = false;
            Cursor.Current = Cursors.Arrow;


        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if( (textBox2.Text != "") && (textBox3.Text != "") )
                button1.Enabled = true;
            else
                button1.Enabled = false;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox3.SelectionStart = textBox3.Text.Length;
            textBox3.ScrollToCaret();

            if ( (textBox2.Text != "") && (textBox3.Text != "") )
                button1.Enabled = true;
            else
                button1.Enabled = false;
        }

        private void TextBox1_TextChanged_1(object sender, EventArgs e)
        {
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();
        }

        private void zakoncz(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
