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
using System.Reflection.Emit;

namespace İnKa
{
    public partial class ParolaGuncelleme : Form
    {
        public ParolaGuncelleme()
        {
            InitializeComponent();
        }
        OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.ACE.OleDb.12.0;Data Source=İnKa.accdb");
        private void ParolaGuncelleme_Load(object sender, EventArgs e)
        {
           // kullanicilari_goster();
        }
        private void kullanicilari_goster()
        {
            try
            {
                baglantim.Open();
                OleDbDataAdapter kullanicilari_listele = new OleDbDataAdapter
                    ("select parola AS[Parola],kullaniciadi AS[Kullanıcı Adı] from kullanicilar Order By ad ASC",baglantim);
                DataSet dshafiza= new DataSet();
                kullanicilari_listele.Fill(dshafiza);
              //  DataGridView.DataSource = dshafiza.Tables[0];
                baglantim.Close();
            }
            catch (Exception hatamesaji)
            {
                MessageBox.Show(hatamesaji.Message, "İnsan Kaynakları Yönetim Sistemi",MessageBoxButtons.OK,MessageBoxIcon.Error);
                baglantim.Close();
                
            }



        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Length != 8)
                errorProvider1.SetError(textBox2, "Kullanıcı adı 8 karakter olmalı!");
            else
                errorProvider1.Clear();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(char.IsLetter(e.KeyChar)==true || char.IsControl(e.KeyChar)==true || char.IsDigit(e.KeyChar)==true)
                e.Handled = false;
            else
                e.Handled = true;
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            bool kayitkontrol = false;
            baglantim.Open();
            OleDbCommand selectsorgu = new OleDbCommand("select*from kullanicilar where kullaniciadi='" + textBox2.Text + "'", baglantim);
            OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();
            while (kayitokuma.Read())
            {
                kayitkontrol = true;
                break;
            }
            baglantim.Close();
            if (kayitkontrol == true)
            {
                if (textBox16.Text == "" || parola_skoru < 70)
                {
                    label1.ForeColor = Color.Red;

                }
                else
                {
                    label1.ForeColor = Color.Black;
                }
                if (textBox17.Text != textBox16.Text)
                {
                    label16.ForeColor = Color.Red;
                }
                else
                {
                    label16.ForeColor = Color.Black;
                }
                if (parola_skoru >= 70 && textBox16.Text != "" && textBox17.Text == textBox16.Text)
                {
                    try
                    {
                        baglantim.Open();
                        OleDbCommand guncellemekomutu = new OleDbCommand("update kullanicilar set parola='" + textBox16.Text + "'where kullaniciadi ='" + textBox2.Text + "'", baglantim);
                        guncellemekomutu.ExecuteNonQuery();
                        baglantim.Close();
                        MessageBox.Show("Güncelleme tamamlandı!", "Personel takip", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    catch (Exception hatamsj)
                    {

                        MessageBox.Show(hatamsj.Message, "Personel takip", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        baglantim.Close();
                    }
                }

                else
                {
                    MessageBox.Show("Yazı rengi kırmızı olan alanları gözden geciriniz!!", "Personel takip", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            else
            {
                MessageBox.Show("Girilen kullanıcı adı bulunamadı", "Personel takip", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

          






        }
        int parola_skoru = 0;
        private void textBox16_TextChanged(object sender, EventArgs e)
        {
            string parolaseviyesi = "";
            int kucuk_harf_skoru = 0;
            int buyuk_harf_skoru = 0, rakam_skoru = 0, sembol_skoru = 0;
            string sifre = textBox16.Text;
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
                textBox16.Text = sifre;
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
                label4.Text = "Buyuk harf,küçük harf ,rakam ve sembol mutlaka kullanmalısın!";

            }
            if (kucuk_harf_skoru != 0 && buyuk_harf_skoru != 0 && rakam_skoru != 0 && sembol_skoru != 0)
            {
                label4.Text = "";
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
            label17.Text = "%" + Convert.ToString(parola_skoru);
            label18.Text = parolaseviyesi;
            progressBar1.Value = parola_skoru;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            KullanıcıGirişEkranı kullanıcıGirişEkranı = new KullanıcıGirişEkranı();
            kullanıcıGirişEkranı.Show();
            this.Hide();
        }
    }
}
