using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace İnKa
{
    public partial class YeniKayit : Form
    {
        public YeniKayit()
        {
            InitializeComponent();
        }
        OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Ace.Oledb.12.0;Data Source=İnKa.accdb");
        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void YeniKayit_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 Form1 = new Form1();
            Form1.Show();
            this.Hide();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text.Length < 11)
                errorProvider1.SetError(textBox3, "TC Kimlik no 11 karakter olmalı!");
            else errorProvider1.Clear();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57 || (int)e.KeyChar == 8)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
            { e.Handled = false; }
            else { e.Handled = true; }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
            { e.Handled = false; }
            else { e.Handled = true; }
        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {
            if (textBox15.Text.Length != 8)
                errorProvider1.SetError(textBox15, "Kullanıcı adı 8 karakter olmalı!");
            else
                errorProvider1.Clear();
        }

        private void textBox15_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsDigit(e.KeyChar) == true)
            { e.Handled = false; }
            else { e.Handled = true; }
        }
        int parola_skoru = 0;
        private void textBox17_TextChanged(object sender, EventArgs e)
        {
            string parolaseviyesi = " ";
            int kucuk_harf_skoru = 0;
            int buyuk_harf_skoru = 0, rakam_skoru = 0, sembol_skoru = 0;
            string sifre = textBox17.Text;
            //regex kutuphanesi türkce karakterleri ing karakterlere donusturuyor
            string duzeltilmis_sifre = " ";
            duzeltilmis_sifre = sifre;
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('İ', 'I');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ç', 'C');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ç', 'c');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ş', 'S');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ş', 's');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ğ', 'G');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ğ', 'g');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ü', 'U');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ü', 'u');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ö', 'O');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ö', 'o');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ı', 'i');
            if (sifre != duzeltilmis_sifre)
            {
                sifre = duzeltilmis_sifre;
                textBox17.Text = sifre;
                MessageBox.Show("Paroladaki Türkçe karakterler İngilizce karakterlere dönüştürülmüştür");

            }
            //1 kucuk harf 10 puan 2 ve üzeri 20 puan
            int az_karakter_sayisi = sifre.Length - Regex.Replace(sifre, "[a-z]", "").Length;
            kucuk_harf_skoru = Math.Min(2, az_karakter_sayisi) * 10;

            //1 buyuk harf 10 puan 2 ve üzeri 20 puan
            int AZ_karakter_sayisi = sifre.Length - Regex.Replace(sifre, "[A-Z]", "").Length;
            buyuk_harf_skoru = Math.Min(2, AZ_karakter_sayisi) * 10;

            //1 rakam 10 puan 2 ve üzeri 20 puan
            int rakam_sayisi = sifre.Length - Regex.Replace(sifre, "[0-9]", "").Length;
            rakam_skoru = Math.Min(2, rakam_sayisi) * 10;


            //1 sembol 10 puan,2 ve üzeri 20 puan
            int sembol_sayisi = sifre.Length - az_karakter_sayisi - AZ_karakter_sayisi - rakam_sayisi;
            sembol_skoru = Math.Min(2, sembol_sayisi) * 10;

            parola_skoru = kucuk_harf_skoru + buyuk_harf_skoru + rakam_skoru + sembol_skoru;

            if (sifre.Length == 9)
            {
                parola_skoru += 10;
            }
            else if (sifre.Length == 10)
            {
                parola_skoru += 20;

            }
            if (kucuk_harf_skoru == 0 || buyuk_harf_skoru == 0 || rakam_skoru == 0 || sembol_skoru == 0)
            {
                label23.Text = "Buyuk harf,küçük harf ,rakam ve sembol mutlaka kullanmalısın!";

            }
            if (kucuk_harf_skoru != 0 && buyuk_harf_skoru != 0 && rakam_skoru != 0 && sembol_skoru != 0)
            {
                label23.Text = "";
            }
            if (parola_skoru < 70)
            {
                parolaseviyesi = "Kabul edilemez!!";
            }
            else if (parola_skoru == 70 || parola_skoru == 80)
            {
                parolaseviyesi = "Güçlü";
            }
            else if (parola_skoru == 90 || parola_skoru == 100)
            {
                parolaseviyesi = "Çok Güçlü";
            }
            label20.Text = "%" + Convert.ToString(parola_skoru);
            label21.Text = parolaseviyesi;
            progressBar1.Value = parola_skoru;


        }
        private void topPage1_temizle()
        {
            textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); textBox4.Clear(); textBox5.Clear(); textBox7.Clear(); textBox8.Clear();
            textBox9.Clear(); textBox10.Clear(); textBox11.Clear(); textBox12.Clear(); textBox13.Clear(); textBox14.Clear(); textBox15.Clear(); textBox17.Clear();
            //tcden sonuna kadar olan kısım
        }
        private void topPage2_temizle()
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            string yetki = "";
            bool kayitkontrol = false;
            baglantim.Open();
            OleDbCommand selectsorgu = new OleDbCommand("select*from kullanicilar where tcno='" + textBox3.Text + "'", baglantim);
            OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();
            while (kayitokuma.Read())
            {
                kayitkontrol = true;
                break;
            }
            baglantim.Close();

            if (kayitkontrol == false)
            {
                //tcno kontrolu
                if (textBox3.Text.Length < 11 || textBox3.Text == "")
                {
                    label4.ForeColor = Color.Red;

                }
                else
                {
                    label4.ForeColor = Color.Black;
                }

                //adkontrol
                if (textBox1.Text.Length < 2 || textBox3.Text == "")
                {
                    label15.ForeColor = Color.Red;

                }
                else
                {
                    label15.ForeColor = Color.Black;
                }
                //soyadkontrol
                if (textBox2.Text.Length < 2 || textBox3.Text == "")
                {
                    label3.ForeColor = Color.Red;

                }
                else
                {
                    label3.ForeColor = Color.Black;
                }
                //kullanıcıadkontrol
                if (textBox15.Text.Length != 8 || textBox3.Text == "")
                {
                    label17.ForeColor = Color.Red;

                }
                else
                {
                    label17.ForeColor = Color.Black;
                }
                //parolakontrol
                if (textBox17.Text == "" || parola_skoru < 70)
                {
                    label18.ForeColor = Color.Red;

                }
                else
                {
                    label18.ForeColor = Color.Black;
                }
                if (textBox3.Text.Length == 11 && textBox3.Text != "" && textBox1.Text != "" && textBox1.Text.Length > 1 && textBox2.Text != "" && textBox2.Text.Length > 1 && textBox15.Text != "" && textBox17.Text != "" && parola_skoru >= 70)
                {
                    if (radioButton1.Checked == true)
                        yetki = "Yönetici";
                    else if (radioButton2.Checked == true)
                        yetki = "Personel";
                    try
                    {
                        baglantim.Open();
                        OleDbCommand eklekomutu = new OleDbCommand("insert into kullanicilar values('" + textBox3.Text + "','" + textBox1.Text + "','" + textBox2.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox11.Text + "','" + textBox12.Text + "','" + textBox8.Text + "','" + textBox9.Text + "','" + textBox7.Text + "','" + textBox10.Text + "','" + textBox13.Text + "','" + textBox14.Text + "','" + yetki + "','" + textBox15.Text + "','" + textBox17.Text + "')", baglantim);
                        eklekomutu.ExecuteNonQuery();
                        baglantim.Close();
                        MessageBox.Show("Kayıt oluşturuldu!", "Personel takip programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        topPage1_temizle();
                    }
                    catch (Exception hatamsj)
                    {
                        MessageBox.Show(hatamsj.Message);
                        baglantim.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Yazı rengi kırmzı olan alanları gözden geçiriniz!", "Personel takip", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            else
            {
                MessageBox.Show("Girilen TC no zaten kayıtlı", "Personel takip", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            YöneticiEkranı yönetici = new YöneticiEkranı();
            yönetici.Show();
            this.Hide();
        }
    }
}
