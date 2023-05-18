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
using System.IO.Ports;
using System.Reflection.Emit;

namespace İnKa
{
    public partial class İsBasvurusu : Form
    {
        public İsBasvurusu()
        {
            InitializeComponent();
        }
        OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Ace.Oledb.12.0;Data Source=İnKa.accdb");
        private void İsBasvurusu_Load(object sender, EventArgs e)
        {
            textBox3.MaxLength = 11;
            toolTip1.SetToolTip(this.textBox3, "TC NO 11 KARAKTER OLMALIDIR");

            textBox1.CharacterCasing=CharacterCasing.Upper;
            textBox2.CharacterCasing = CharacterCasing.Upper;
            textBox6.CharacterCasing = CharacterCasing.Upper;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 Form1 = new Form1();
            Form1.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
        private void toppage1_temizle()
        {
            textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); textBox4.Clear(); comboBox1.SelectedItem = null; comboBox2.SelectedItem = null;
            comboBox3.SelectedItem = null; textBox6.Clear(); textBox11.Clear(); textBox9.Clear(); textBox7.Clear();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            bool kayitkontrol=false;
            baglantim.Open();
            OleDbCommand selectsorgu=new OleDbCommand("select * from isbasvurusu where Tcno='"+textBox3.Text+"'",baglantim);
            OleDbDataReader kayitokuma=selectsorgu.ExecuteReader();
            while (kayitokuma.Read())
            {
                kayitkontrol = true;
                break;

            }
            baglantim.Close();
            if (kayitkontrol == false)
            {
                if (textBox3.Text.Length < 11 || textBox3.Text == "")
                {
                    label10.ForeColor = Color.Red;

                }
                else
                {
                    label10.ForeColor = Color.Black;
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
                    label11.ForeColor = Color.Red;

                }
                else
                {
                    label11.ForeColor = Color.Black;
                }

                if ( textBox6.Text == "")
                {
                    label7.ForeColor = Color.Red;

                }
                else
                {
                    label7.ForeColor = Color.Black;
                }
                if (textBox11.Text == "")
                {
                    label8.ForeColor = Color.Red;

                }
                else
                {
                    label8.ForeColor = Color.Black;
                }
                if (textBox9.Text == "")
                {
                    label13.ForeColor = Color.Red;

                }
                else
                {
                    label13.ForeColor = Color.Black;
                }
                if (textBox7.Text == "")
                {
                    label16.ForeColor = Color.Red;

                }
                else
                {
                    label16.ForeColor = Color.Black;
                }
                if(comboBox1.Text=="")
                    label6.ForeColor= Color.Red;
                else
                    label6.ForeColor = Color.Black;
                if(comboBox2.Text=="")
                    label9.ForeColor= Color.Red;
                else
                    label9.ForeColor = Color.Black;
                if(comboBox3.Text==" ")
                    label14.ForeColor= Color.Red;
                else
                    label14.ForeColor = Color.Black;
                if(textBox3.Text.Length==11)
                {
                    try
                    {
                        baglantim.Open();
                        OleDbCommand eklekomutu = new OleDbCommand("insert into isbasvurusu values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + comboBox1.Text + "','" + textBox6.Text + "','" + textBox11.Text + "','" + comboBox2.Text + "','" + textBox9.Text + "','" + comboBox3.Text + "','" + textBox7.Text + "')", baglantim);
                        eklekomutu.ExecuteNonQuery();
                        baglantim.Close();
                        MessageBox.Show("Başvuru oluşturuldu!", "Personel takip programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                         toppage1_temizle();
                    }
                    catch (Exception hatamsj)
                    {
                        MessageBox.Show(hatamsj.Message);
                        baglantim.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Yazı rengi kırmızı olan alanları gözden geçirin !!!", "Personel takip", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text.Length < 11)
            {
                errorProvider1.SetError(textBox3, "TC KİMLİK NO 11 KARAKTER OLMALIDIR");

            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text.Length < 11)
            {
                errorProvider1.SetError(textBox3, "TELEFON NUMARASINI 11 KARAKTER OLACAK ŞEKİLDE GİRİNİZ");

            }
            else
            {
                errorProvider1.Clear();
            }
        }
        //HARF BASIP BASMAMA AYAR KISMI 
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
           
            if (((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57) || (int)e.KeyChar == 8)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(char.IsLetter(e.KeyChar)==true || char.IsControl(e.KeyChar)==true || char.IsSeparator(e.KeyChar)==true) {
                e.Handled = false;
            }
            else 
                e.Handled= true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }
        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57) || (int)e.KeyChar == 8)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox4_TextChanged_1(object sender, EventArgs e)
        {
            if (textBox4.Text.Length < 11)
            {
                errorProvider1.SetError(textBox4, "TEL NOYU 11 HANELİ OLACAK ŞEKİLDE GİRİNİZ");

            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void textBox4_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57) || (int)e.KeyChar == 8)
                e.Handled = false;
            else
                e.Handled = true;
        }
    }
}
