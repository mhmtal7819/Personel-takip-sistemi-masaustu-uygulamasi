﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace İnKa
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            İsBasvurusu İsBasvurusu = new İsBasvurusu();
            İsBasvurusu.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            KullanıcıGirişEkranı KullanıcıGirişEkranı = new KullanıcıGirişEkranı();
            KullanıcıGirişEkranı.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
