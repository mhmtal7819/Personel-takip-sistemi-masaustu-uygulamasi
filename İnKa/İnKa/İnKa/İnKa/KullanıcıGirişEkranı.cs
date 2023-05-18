using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb; //Veritabanına erişmek için bu kütüphaneyi ekleriz

namespace İnKa
{
    public partial class KullanıcıGirişEkranı : Form
    {
        public OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.ACE.OleDb.12.0;Data Source=İnKa.accdb"); //Access veri tabanı bağlantısı
        public static string ad, soyad, tcno, telno, cinsiyet, deneyim, maas, saglikbilgisi, egitimbilgisi, isdeneyimi, yillikizin, pozisyon, adres, calismasaati, yetki;

        public KullanıcıGirişEkranı()
        {
            InitializeComponent();
        }
        public static string veri;
       
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ParolaGuncelleme ParolaGuncelleme = new ParolaGuncelleme();
            ParolaGuncelleme.Show();
            this.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 Form1= new Form1();
            Form1.Show();
            this.Hide();
        }

        private void KullanıcıGirişEkranı_Load(object sender, EventArgs e)
        {
            
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
        bool kayit_arama_durumu = false;
        
        private void button1_Click(object sender, EventArgs e)
        {

            
            if (hak != 0)
            {
                baglantim.Open();
                OleDbCommand selectsorgu = new OleDbCommand("select * from kullanicilar", baglantim); // verileri almak için kullanılır veriler baglantim adında değişkende tutulur
                OleDbDataReader kayitokuma = selectsorgu.ExecuteReader(); //veri okuma işlemi
               /* OleDbCommand selectsorgu2 = new OleDbCommand("select * from kullanicilar where kullaniciadi='" + textBox1.Text + "'", baglantim);
                OleDbDataReader kayitokuma2 = selectsorgu2.ExecuteReader(); */

                while (kayitokuma.Read())
                {
                    if (radioButton1.Checked == true)
                    {
                        if (kayitokuma["kullaniciadi"].ToString() == textBox1.Text && kayitokuma["parola"].ToString() == textBox2.Text && kayitokuma["yetki"].ToString() == "Yönetici")
                        {
                            durum = true;
                            tcno = kayitokuma.GetValue(0).ToString();
                            yetki = kayitokuma.GetValue(13).ToString();
                            this.Hide();
                            YöneticiEkranı YöneticiEkranı = new YöneticiEkranı();
                            YöneticiEkranı.Show();
                            break;
                        }
                    }
                    if (radioButton2.Checked == true)
                     {
                        if (kayitokuma["kullaniciadi"].ToString() == textBox1.Text && kayitokuma["parola"].ToString() == textBox2.Text && kayitokuma["yetki"].ToString() == "Personel")
                        {
                            veri = textBox1.Text;
                            durum = true;
                            tcno = kayitokuma.GetValue(0).ToString();
                            yetki = kayitokuma.GetValue(13).ToString();
                            this.Hide();
                            PersonelEkranı PersonelEkranı = new PersonelEkranı();
                            PersonelEkranı.Show();




                            break;
                        }
                    }
                    
                }
              
                if (durum == false)
                    hak-- ;
                baglantim.Close();
                }
            label7.Text = Convert.ToString(hak);
            if(hak==0)
            {
                button1.Enabled = false;
                MessageBox.Show("Giriş Hakkınız Kalmadı Yöneticinizden Yeni Parola İsteyiniz","İnsan Kaynakları Yönetim Sistemi",MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.Close();
            }
         /*   if(textBox1.Text.Length>2)
            {
                baglantim.Open();
                OleDbCommand selectsorgu2 = new OleDbCommand("select * from kullanicilar where kullaniciadi='" + textBox1.Text + "'", baglantim);
                OleDbDataReader kayitokuma2=selectsorgu2.ExecuteReader();
                while (kayitokuma2.Read())
                {
                    kayit_arama_durumu = true;
                    personelekrani.textBox2.Text= kayitokuma2.GetValue(1).ToString();
                    personelekrani.textBox3.Text = kayitokuma2.GetValue(2).ToString();
                    personelekrani.textBox4.Text = kayitokuma2.GetValue(0).ToString();
                    personelekrani.textBox5.Text = kayitokuma2.GetValue(3).ToString();
                    personelekrani.comboBox1.Text = kayitokuma2.GetValue(4).ToString();
                    personelekrani.comboBox3.Text = kayitokuma2.GetValue(5).ToString(); //unvan
                    personelekrani.textBox8.Text = kayitokuma2.GetValue(9).ToString(); //is deneyimi
                    personelekrani.textBox9.Text = kayitokuma2.GetValue(7).ToString(); //saglik
                    personelekrani.comboBox2.Text = kayitokuma2.GetValue(8).ToString(); //egitim
                    personelekrani.textBox11.Text = kayitokuma2.GetValue(10).ToString();
                    //  personelekrani.textBox12.Text = kayitokuma.GetValue(9).ToString(); //pozisyon?
                    personelekrani.textBox13.Text = kayitokuma2.GetValue(6).ToString();
                    personelekrani.textBox14.Text = kayitokuma2.GetValue(11).ToString();
                    personelekrani.textBox15.Text = kayitokuma2.GetValue(12).ToString();
                    

                } 
                baglantim.Close();
            }
            else
            {
                MessageBox.Show("Yanlış bir kullanıcı adı girdiniz","Personel Takip",MessageBoxButtons.OK,MessageBoxIcon.Error);
            } */

            } 

        int hak = 3;bool durum = false;
        private void label3_Click(object sender, EventArgs e)
        {
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.Text = "Kullanıcı Girişi..";
            this.AcceptButton = button1;
            label7.Text = Convert.ToString(hak);
            radioButton1.Checked = true;
        }
    }
}
