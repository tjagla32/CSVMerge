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
        unsafe void saveToFile(string read, string write, int *numberOfFile)
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
                //if (!line.Contains(blankRow))
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

        private unsafe void find(object sender, EventArgs e)
        {
            int i = 1; //numer pliku
            int* ptr = &i; //wskaźnik - w funkcji liczy, który plik jest obsługiwany
            string read = "C:/Users/Tomek/Desktop/export_1.csv";
            string read2 = "C:/Users/Tomek/Desktop/export_1.csv";

            string write = "C:/Users/Tomek/Desktop/export_wynik.csv";

            saveToFile(read, write, ptr);
            //saveToFile(read2, write, ptr);
            

            //https://www.youtube.com/watch?v=C7t8qO3iI5s

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
