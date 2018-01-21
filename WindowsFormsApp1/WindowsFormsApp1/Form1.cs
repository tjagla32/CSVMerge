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
        void saveToFile(string read, string write)
        {
            String line;
            System.IO.StreamReader file = new System.IO.StreamReader(read, Encoding.GetEncoding("Windows-1250"));

            var lines = File.ReadAllLines(read);
            var columnAmount = lines[0].Split(';').Length; //sprawdza ilosć kolumn
            columnAmount--;
            string blankRow = "";
            bool firstRow = false; //usuwa tekst z pierwszego rzędu

            for (int i = 0; i < columnAmount; i++)
                blankRow += ";";

            while ((line = file.ReadLine()) != null)
            {

                if (!line.Contains(blankRow))
                {
                    if (!firstRow)
                        firstRow = !firstRow;
                    else
                        textBox1.Text += line + Environment.NewLine;
                    
                }
                    
            }

            File.WriteAllText(write, textBox1.Text, Encoding.GetEncoding("Windows-1250"));
        }


        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void find(object sender, EventArgs e)
        {

            string read = "worki.csv";
            string read2 = "szczotki.csv";

            string write = "wynik.csv";

            saveToFile(read, write);
            saveToFile(read2, write);

            //https://www.youtube.com/watch?v=C7t8qO3iI5s

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
