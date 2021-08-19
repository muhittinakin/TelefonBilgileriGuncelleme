using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TelefonBilgiGuncelleme
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection cnn = new SqlConnection();
        SqlDataAdapter adapter;
        DataSet dataSet = new DataSet();
        int SiraID;
        private void Form1_Load(object sender, EventArgs e)
        {
            TabloDoldur();
        }
        void TabloDoldur()
        {
            cnn.ConnectionString = "Server=???;Database=??;User Id=???;Password=??";
            adapter = new SqlDataAdapter("SELECT * FROM IDR_TELEFONREHBERI", cnn);
            cnn.Open();
            dataSet.Clear();
            adapter.Fill(dataSet, "xxx");
            dataGridView1.DataSource = dataSet.Tables["xxx"];
            cnn.Close();
        }
        void EkranTemizleme()
        {
            //Ekleme
            txtBirimAd.Text = "";
            txtAd.Text = "";
            txtGorev.Text = "";
            txtDahili.Text = "";
            txtKisaKod.Text = "";
            txtCepNo.Text = "";
            txtMail.Text = "";

            //Güncelleme
            txtGBirimAd.Text = "";
            txtGAd.Text = "";
            txtGGorev.Text = "";
            txtGDahili.Text = "";
            txtGKisaKod.Text = "";
            txtGCepNo.Text = "";
            txtGMail.Text = "";
        }
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SiraID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            txtGBirimAd.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtGAd.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtGGorev.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtGDahili.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtGKisaKod.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txtGCepNo.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            txtGMail.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO xxx (BirimAdi,AdSoyad,Gorevi,Dahili,KisaKodu,CepTelefonNo,EPosta) VALUES(@BirimAdi,@AdSoyad,@Gorevi,@Dahili,@KisaKodu,@CepTelefonNo,@EPosta)", cnn);

                cmd.Parameters.AddWithValue("@BirimAdi", txtBirimAd.Text);
                cmd.Parameters.AddWithValue("@AdSoyad", txtAd.Text);
                cmd.Parameters.AddWithValue("@Gorevi", txtGorev.Text);
                cmd.Parameters.AddWithValue("@Dahili", txtDahili.Text);
                cmd.Parameters.AddWithValue("@KisaKodu",txtKisaKod.Text);
                cmd.Parameters.AddWithValue("@CepTelefonNo", txtCepNo.Text);
                cmd.Parameters.AddWithValue("@EPosta", txtMail.Text);

                cnn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Kullanıcı Eklendi!!!");
                cnn.Close();

                EkranTemizleme();
                TabloDoldur();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("UPDATE xxx SET BirimAdi=@BirimAdi, AdSoyad=@AdSoyad, Gorevi=@Gorevi, Dahili=@Dahili, KisaKodu=@KisaKodu, CepTelefonNo=@CepTelefonNo, EPosta=@EPosta WHERE Id=" + SiraID, cnn);
                cmd.Parameters.AddWithValue("@BirimAdi", txtGBirimAd.Text);
                cmd.Parameters.AddWithValue("@AdSoyad", txtGAd.Text);
                cmd.Parameters.AddWithValue("@Gorevi", txtGGorev.Text);
                cmd.Parameters.AddWithValue("@Dahili", txtGDahili.Text);
                cmd.Parameters.AddWithValue("@KisaKodu", txtGKisaKod.Text);
                cmd.Parameters.AddWithValue("@CepTelefonNo", txtGCepNo.Text);
                cmd.Parameters.AddWithValue("@EPosta", txtGMail.Text);

                cnn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Kullanıcı Güncellendi!!!");
                cnn.Close();

                EkranTemizleme();
                TabloDoldur();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM xxx WHERE Id=" + SiraID, cnn);

                cnn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Kullanıcı Silindi!!");
                cnn.Close();

                EkranTemizleme();
                TabloDoldur();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cnn.Open();
            SqlCommand cmd = new SqlCommand("Select * from xxx where AdSoyad like '%" + textBox1.Text + "%'", cnn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            cnn.Close();
        }
    }
}
