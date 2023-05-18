using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Text.RegularExpressions;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Reflection.Emit;

namespace İnKa
{
    public partial class YöneticiEkranı : Form
    {
        public static string Ad, Soyad, Tcno, Telno, cinsiyet, deneyim, isdeneyimi, saglikbilgisi, adres, egitimbilgisi, maasbeklentisi;
        public YöneticiEkranı()
        {
            InitializeComponent();
        }
        OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Ace.Oledb.12.0;Data Source=İnKa.accdb");
        KullanıcıGirişEkranı k2 = new KullanıcıGirişEkranı();
        
        private void kullanicilari_goster()
        {
            try
            {
                baglantim.Open();
                OleDbDataAdapter kullanicilari_listele = new OleDbDataAdapter("select tcno AS[TC KİMLİK NO],ad AS [ADI],soyad AS[SOYADI]," +
                    "telno AS[TELEFON NO],cinsiyet AS[CİNSİYETİ],unvan AS[ÜNVANI],maas AS[MAAŞI],saglikbilgisi AS[SAĞLIK BİLGİSİ],egitimbilgisi " +
                    "AS[EĞİTİM BİLGİSİ],isdeneyimi AS[İŞ DENEYİMİ],yillikizin AS[YILLIK İZNİ],adres AS[ADRESİ],calismasaati AS[ÇALIŞMA SAATİ],yetki " +
                    "AS[YETKİSİ],kullaniciadi AS[KULLANICI ADI],parola AS[PAROLA] from kullanicilar Order by ad ASC", baglantim);
                DataSet dshafiza = new DataSet();
                kullanicilari_listele.Fill(dshafiza);
                dataGridView1.DataSource = dshafiza.Tables[0];
                baglantim.Close();
            }
            catch (Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message, "Personel Takip", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglantim.Close();
                throw;

            }
        }
        private void isbasvurulari_goster()
        {
            try
            {
                baglantim.Open();
                OleDbDataAdapter basvurulari_listele = new OleDbDataAdapter("select Ad AS[AD], Soyad AS [SOYADI],Tcno AS [TCNO],cinsiyet as [CİNSİYET],deneyim AS[MESLEK],isdeneyimi AS[İŞ DENEYİMİ],saglikbilgisi AS [SAĞLIK DURUMU],adres AS [ADRES],egitimbilgisi AS[EĞİTİM BİLGİSİ],maasbeklentisi AS[MAAS BEKLENTİSİ] from isbasvurusu Order By ad ASC", baglantim);
                DataSet dshafizaa = new DataSet();
                basvurulari_listele.Fill(dshafizaa);
                dataGridView2.DataSource = dshafizaa.Tables[0];
                baglantim.Close();
            }
            catch(Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message,"Personel Takip",MessageBoxButtons.OK,MessageBoxIcon.Error);
                baglantim.Close();
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            KullanıcıGirişEkranı KullanıcıGirişEkranı = new KullanıcıGirişEkranı();
            KullanıcıGirişEkranı.Show();
            this.Hide();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            YeniKayit YeniKayit = new YeniKayit();
            YeniKayit.Show();
            this.Hide();
        }
        private void topPage1_temizle()
        {
            textBox1.Clear(); textBox3.Clear(); textBox4.Clear(); textBox5.Clear(); textBox6.Clear(); textBox7.Clear(); comboBox1.SelectedItem = null; //
            textBox9.Clear(); comboBox2.SelectedItem=null; textBox12.Clear(); comboBox3.SelectedItem = null; textBox14.Clear(); textBox15.Clear(); textBox17.Clear();
            //tcden sonuna kadar olan kısım
        }
        private void button6_Click(object sender, EventArgs e)
        {
            bool kayit_arama_durumu = false;
            if (textBox5.Text.Length == 11)
            {
                baglantim.Open();
                OleDbCommand selectsorgu=new OleDbCommand("select  * from kullanicilar where tcno='"+textBox5.Text+"'",baglantim);
                OleDbDataReader kayitokuma=selectsorgu.ExecuteReader();
                while(kayitokuma.Read())
                {
                    kayit_arama_durumu = true;
                    textBox1.Text=kayitokuma.GetValue(1).ToString(); //acces tablosunda 1. sıra 
                    textBox4.Text = kayitokuma.GetValue(2).ToString();//burda tüm bilgileri yansıtabiliriz
                    textBox6.Text = kayitokuma.GetValue(3).ToString();
                    comboBox1.Text = kayitokuma.GetValue(4).ToString();
                    comboBox2.Text = kayitokuma.GetValue(5).ToString();
                    textBox12.Text = kayitokuma.GetValue(6).ToString();
                    textBox7.Text = kayitokuma.GetValue(7).ToString();
                    comboBox3.Text = kayitokuma.GetValue(8).ToString();
                    textBox17.Text = kayitokuma.GetValue(9).ToString();
                    textBox9.Text = kayitokuma.GetValue(10).ToString();
                    textBox14.Text = kayitokuma.GetValue(11).ToString();
                    textBox15.Text = kayitokuma.GetValue(12).ToString();
                    

                    if (kayitokuma.GetValue(13).ToString() == "Yönetici")
                    {
                        radioButton1.Checked = true; //yönetici buyonu 
                    }
                    else
                    {
                        radioButton2.Checked = true;
                    }
                }
                if(kayit_arama_durumu==false)
                {
                    MessageBox.Show("Aranan kayıt bulunamadı!!","Personel takip programı",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);

                }
                baglantim.Close();
            }
            else
            {
                MessageBox.Show("Lütfen 11 haneli bir TC no giriniz!!","Personel takip programı",MessageBoxButtons.OK, MessageBoxIcon.Error);
                //toppage_1 temizle text boxları silicek fonksiyon ekle
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            label7.Text = KullanıcıGirişEkranı.veri;
            try
            {
                baglantim.Open();
                OleDbCommand eklekomutu = new OleDbCommand("insert into mesajlar values('" + label7.Text + "','" + comboBox4.Text + "','" + textBox3.Text + "')", baglantim);
                eklekomutu.ExecuteNonQuery();
                baglantim.Close();
                MessageBox.Show("Mesajınız Gönderildi!", "Personel takip programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //topPage1_temizle();
            }
            catch (Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message);
                baglantim.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string yetki = "";
            if(textBox5.Text.Length<11 || textBox5.Text == "")
            label29.ForeColor = Color.Red;
            else
                label29.ForeColor = Color.Black;

            if (textBox1.Text.Length < 2 || textBox1.Text == "")
                label1.ForeColor = Color.Red;
            else
                label1.ForeColor = Color.Black;

            if (textBox4.Text.Length < 2 || textBox4.Text == "")
                label28.ForeColor = Color.Red;
            else
                label28.ForeColor = Color.Black;

            if (textBox6.Text.Length < 11 || textBox6.Text == "")
                label30.ForeColor = Color.Red;
            else
                label30.ForeColor = Color.Black;

            if(textBox5.Text.Length==11 &&  textBox5.Text != "" && textBox1.Text!="" && textBox1.Text.Length>1 && textBox4.Text != "" && textBox4.Text.Length>1 )
            {
                if (radioButton1.Checked == true)
                    yetki = "Yönetici";
               else if (radioButton2.Checked == true)
                    yetki = "Personel";
                try
                {
                    baglantim.Open();
                    OleDbCommand guncellekomutu= new OleDbCommand("update kullanicilar set ad='"+textBox1.Text+"',soyad='"+textBox4.Text+"',telno='"+textBox6.Text+"',cinsiyet='"+comboBox1.Text+"',unvan='"+comboBox2.Text+"',maas='"+textBox12.Text+"',saglikbilgisi='"+textBox7.Text+"',egitimbilgisi='"+comboBox3.Text+"',isdeneyimi='"+textBox17.Text+"',yillikizin='"+textBox9.Text+"',adres='"+textBox14.Text+"',calismasaati='"+textBox15.Text+"',yetki='"+yetki+"'where tcno='"+textBox5.Text+"'",baglantim );
                    guncellekomutu.ExecuteNonQuery();
                    baglantim.Close();
                    MessageBox.Show("Güncelleme tamamlandı!", "Persone takip", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    kullanicilari_goster(); //istege baglı temizle butonu da gelecek..
                }
                catch (Exception hatamsj)
                {

                    MessageBox.Show(hatamsj.Message, "Personel takip", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    baglantim.Close() ;
                }
            }
            else
            {
                MessageBox.Show("Yazı rengi kırmızı olan alanları gözden geciriniz!!", "Personel takip", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }






        }

        private void YöneticiEkranı_Load(object sender, EventArgs e)
        {
            kullanicilari_goster();
            isbasvurulari_goster();

            comboBox1.Items.Add("ERKEK"); comboBox1.Items.Add("KADIN");
            comboBox2.Items.Add("MUHENDİS"); comboBox2.Items.Add("ŞÖFÖR"); comboBox2.Items.Add("İŞCİ"); comboBox2.Items.Add("GÜVENLİK");
            comboBox3.Items.Add("İLKÖĞRETİM"); comboBox3.Items.Add("ORTAÖĞRETİM"); comboBox3.Items.Add("LİSE"); comboBox3.Items.Add("ÜNİVER" +
                "STE"); comboBox3.Items.Add("YÜKSEK LİSANS");

            OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Ace.Oledb.12.0;Data Source=İnKa.accdb");
            OleDbCommand komut = new OleDbCommand();
            komut.CommandText = "SELECT * from kullanicilar ";
            komut.Connection = baglantim;
            komut.CommandType = CommandType.Text;
            OleDbDataReader dr;
            baglantim.Open();
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBox4.Items.Add(dr["kullaniciadi"]);
            }
            baglantim.Close();
            
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.Text.Length < 11)
                errorProvider1.SetError(textBox5, "TC Kimlik No 11 karakter olmalı!");
            else
                errorProvider1.Clear();
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57) || (int)e.KeyChar == 8)
                e.Handled = false;
            else
                e.Handled = true;

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || Char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || Char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void label34_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox5.Text.Length == 11)
            {
                bool kayit_arama_durumu = false;
                baglantim.Open();
                OleDbCommand selectsorgu=new OleDbCommand("select * from kullanicilar where tcno='"+textBox5.Text+"'",baglantim); //girilen tcno hangisine eşitse onu aldı.
                OleDbDataReader kayitokuma=selectsorgu.ExecuteReader();
                while(kayitokuma.Read())
                {
                    kayit_arama_durumu = true;
                    OleDbCommand deletesorgu=new OleDbCommand("delete from kullanicilar where tcno='"+textBox5.Text+"'",baglantim); //tcno ya eşit olan kullanıcıyı silicez 
                    deletesorgu.ExecuteNonQuery();
                    MessageBox.Show("Kullanıcı kaydı silindi!", "Personel takip programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    baglantim.Close();
                    kullanicilari_goster();
                    topPage1_temizle();
                    break;

                }
                if(kayit_arama_durumu==false)
                
                    MessageBox.Show("Silinecek kayit bulunamadi!", "Personel takip programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    baglantim.Close() ;
                topPage1_temizle() ;



            }
            else
                MessageBox.Show("Lütfen 11 karakterli bir sayı giriniz!", "Personel takip programı", MessageBoxButtons.OK, MessageBoxIcon.Error);



        }

        private void button7_Click(object sender, EventArgs e)
        {
            topPage1_temizle();
        }

        private void tabPage5_Click_1(object sender, EventArgs e)
        {

        }
    }
}
