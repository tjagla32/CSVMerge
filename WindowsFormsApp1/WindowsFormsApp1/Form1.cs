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

            String line;
            System.IO.StreamReader file = new System.IO.StreamReader(read, Encoding.GetEncoding("Windows-1250"));
            
            bool firstRow = false; //usuwa tekst z pierwszego rzędu

            while ((line = file.ReadLine()) != null)
            {
                {
                    if (*numberOfFile == 1)
                        textBox1.Text += line + Environment.NewLine;
                    else
                    {
                        if (!firstRow)
                            firstRow = !firstRow;
                        else
                            textBox1.Text += line + Environment.NewLine;
                    }
                }

            }
            File.WriteAllText(write, textBox1.Text, Encoding.GetEncoding("Windows-1250"));
            *numberOfFile += 1;
        }


        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
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
                    for(int i=0; i< dialog.SafeFileNames.Length; i++)
                        textBox3.Text += dialog.SafeFileNames[i] + Environment.NewLine;
                }
            }
        }

         public unsafe void find(object sender, EventArgs e)
        {
            int i = 1; //numer pliku
            int* ptr = &i; //wskaźnik - w funkcji liczy, który plik jest obsługiwany

           // textBox1.Text = "Wybrane pliki: " + dialog.SafeFileNames.Length + Environment.NewLine;


            string out_path = Path.GetDirectoryName(dialog.FileNames[0]);
            string write_file = out_path + @"\" + textBox2.Text + ".csv";

            textBox1.Text += "Dane będą zapisane w: " + Environment.NewLine + write_file + Environment.NewLine + Environment.NewLine;


            for (int n = 0; n < dialog.SafeFileNames.Length; n++)
            {
                textBox1.Text += "Scalam plik nr " + (n+1) + ": " + Environment.NewLine;
                textBox1.Text += dialog.FileNames[n] + Environment.NewLine + Environment.NewLine;
                saveToFile(dialog.FileNames[n], write_file, ptr);
            }
                

            //saveToFile(read, write, ptr);
            //saveToFile(read2, write, ptr);


        }

        private void label1_Click(object sender, EventArgs e)
        {

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
            if( (textBox2.Text != "") && (textBox3.Text != "") )
                button1.Enabled = true;
            else
                button1.Enabled = false;
        }

        private void TextBox1_TextChanged_1(object sender, EventArgs e)
        {
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();
        }
    }
}
