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

namespace İnKa
{
    public partial class PersonelEkranı : Form
    {
        public PersonelEkranı()
        {
            InitializeComponent();
        }
        OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Ace.Oledb.12.0;Data Source=İnKa.accdb"); //Access veri tabanı bağlantısı
                                                                                                                     //  public static string ad, soyad, tcno, telno, cinsiyet, deneyim, maas, saglikbilgisi, egitimbilgisi, isdeneyimi, yillikizin, pozisyon, adres, calismasaati, yetki;
        public static string gonderenadi, aliciadi, mesaj;
        KullanıcıGirişEkranı k1 = new KullanıcıGirişEkranı();
        private void tabPage1_Click(object sender, EventArgs e)
        {
          
        } 

        private void tabPage4_Click(object sender, EventArgs e)
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
        private void PersonelEkranı_Load(object sender, EventArgs e)
        {

         }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {

        }

        public void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    
        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = KullanıcıGirişEkranı.veri;
            bool kayit_arama_durumu = false;
            if (label1.Text.Length > 2)
            {
                baglantim.Open();
                OleDbCommand selectsorgu = new OleDbCommand("select tcno,ad,soyad,telno,cinsiyet,unvan,maas,saglikbilgisi,egitimbilgisi,isdeneyimi,yillikizin,adres,calismasaati from kullanicilar where kullaniciadi='" + label1.Text + "'", baglantim);
                OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();
                while (kayitokuma.Read())
                {
                     kayit_arama_durumu = true;
                    textBox2.Text = kayitokuma["ad"].ToString(); //ad
                    textBox3.Text = kayitokuma["soyad"].ToString(); //soyad
                    textBox4.Text = kayitokuma["tcno"].ToString(); //tcno
                    textBox5.Text = kayitokuma["telno"].ToString(); //telno
                    comboBox1.Text = kayitokuma["cinsiyet"].ToString(); //cinsiyet
                    comboBox3.Text = kayitokuma["unvan"].ToString(); //unvan
                    textBox8.Text = kayitokuma["isdeneyimi"].ToString(); //is deneyimi
                    textBox9.Text = kayitokuma["saglikbilgisi"].ToString(); //saglik
                    comboBox2.Text = kayitokuma["egitimbilgisi"].ToString(); //egitim
                    textBox11.Text = kayitokuma["yillikizin"].ToString(); //yillikizin
                                                                          //  personelekrani.textBox12.Text = kayitokuma.GetValue(9).ToString(); //pozisyon?
                    textBox13.Text = kayitokuma["maas"].ToString(); //maas
                    textBox14.Text = kayitokuma["adres"].ToString(); //adres
                    textBox15.Text = kayitokuma["calismasaati"].ToString(); //calismasaati


                }
                 if (kayit_arama_durumu == false)
                 {
                     MessageBox.Show("Hata", "Personel Takip", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                 } 
                baglantim.Close();
            }
            else
                MessageBox.Show("hata var","personel takip",MessageBoxButtons.OK,MessageBoxIcon.Error); 

        }

        private void PersonelEkranı_Load_1(object sender, EventArgs e)
        {
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

            this.Text = "Kullanıcı İşlemleri";
            label20.ForeColor = Color.Black;
            label20.Text = KullanıcıGirişEkranı.veri;
            mesajlari_goster();

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void mesajlari_goster()
        {
            try
            {
                baglantim.Open();
                OleDbDataAdapter mesajlari_listele = new OleDbDataAdapter("select gonderenadi AS [GONDEREN], aliciadi AS [ALICI],konu AS [KONU], mesaj AS [MESAJ] from mesajlar WHERE aliciadi = @alici ORDER BY gonderenadi DESC", baglantim);
                mesajlari_listele.SelectCommand.Parameters.AddWithValue("@alici", KullanıcıGirişEkranı.veri); // Kullanıcı adını burada ekleyin
                DataSet dshafizaaa = new DataSet();
                mesajlari_listele.Fill(dshafizaaa);
                dataGridView3.DataSource = dshafizaaa.Tables[0];
                baglantim.Close();
            }
            catch (Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message, "Personel Takip", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglantim.Close();
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            label20.Text = KullanıcıGirişEkranı.veri;
            try
            {
                baglantim.Open();
                OleDbCommand eklekomutu = new OleDbCommand("insert into mesajlar values('" + label20.Text + "','" + comboBox4.Text+ "','" + textBox1.Text + "','" + textBox6.Text+"')", baglantim);
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
    }
}
