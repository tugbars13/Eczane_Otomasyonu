using DevExpress.Utils;
using PHARMACY_MANAGEMENT_SYSTEM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Function fn = new Function();  // 'Function' sınıfı, veri tabanı işlemleri için kullanılıyor
        String query;  // SQL sorguları için değişken
        DataSet ds;  // Veritabanı sonucu için veri kümesi


        public Form1()
        {
            try
            {
                InitializeComponent(); // Form bileşenlerini başlatır
            }
            catch (Exception ex)
            {
                File.AppendAllText("error.log", $"{DateTime.Now}: {ex.Message}{Environment.NewLine}{ex.StackTrace}{Environment.NewLine}");
                MessageBox.Show($"Form yüklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                /*using (SqlConnection connection = fn.getConnection())
                {
                    connection.Open();
                    MessageBox.Show("Bağlantı başarılı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }*/
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veritabanı bağlantı hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Çıkış butonuna tıklanmasıyla uygulamayı kapatma
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();  // Uygulamayı kapatır
        }

        // Reset butonuna tıklanmasıyla kullanıcı adı ve şifreyi sıfırlama
        private void btnReset_Click(object sender, EventArgs e)
        {
            txtUsername.Text = string.Empty;  // Kullanıcı adı inputunu temizler
            txtPassword.Text = string.Empty;  // Şifre inputunu temizler
        }

        // Giriş yap butonuna tıklanmasıyla kullanıcı doğrulama işlemi
        private void btnSignIn_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;  // Kullanıcı adını al
            string pass = txtPassword.Text;  // Şifreyi al

            string query = "SELECT * FROM users WHERE username = @username AND pass = @pass";  // SQL sorgusu

            using (SqlConnection connection = fn.getConnection())  // Veritabanı bağlantısını açar
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))  // Sorgu ve bağlantı ile komut oluşturur
                {
                    command.Parameters.AddWithValue("@username", username);  // Parametre olarak kullanıcı adı
                    command.Parameters.AddWithValue("@pass", pass);  // Parametre olarak şifre

                    DataSet ds = new DataSet();  // Veritabanı sonucu için veri kümesi
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))  // Komutu veri setine doldurur
                    {
                        adapter.Fill(ds);  // Verileri veri setine ekler
                    }

                    if (ds.Tables[0].Rows.Count != 0)  // Eğer veri varsa, kullanıcı doğrulaması yapılır
                    {
                        String role = ds.Tables[0].Rows[0][1].ToString();  // Kullanıcının rolü
                        if (role == "Administrator")  // Eğer rol "Administrator" ise
                        {
                            Administrator admin = new Administrator();  // Admin ekranını göster
                            admin.Show();
                            this.Hide();  // Şu anki formu gizle
                        }
                        else if (role == "Pharmacist")  // Eğer rol "Pharmacist" ise
                        {
                            Pharmacist pharm = new Pharmacist();  // Eczacı ekranını göster
                            pharm.Show();
                            this.Hide();  // Şu anki formu gizle
                        }
                        else
                        {
                            // Hatalı giriş durumunda mesaj göster
                            DevExpress.XtraEditors.XtraMessageBox.Show(
                               "Yanlış Kullanıcı Adı veya Şifre",
                                "Hata",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        // Hatalı giriş durumunda mesaj göster
                        DevExpress.XtraEditors.XtraMessageBox.Show(
                                "Yanlış Kullanıcı Adı veya Şifre",
                                 "Hata",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Error);
                    }
                }
            }
        }
        public SqlConnection getConnection()
        {
            try
            {
                string connectionString = "data source = DESKTOP-E23C3JA\\SQLEXPRESS01; database = pharmacy; integrated security = True";
                SqlConnection connection = new SqlConnection(connectionString);
                return connection;
            }
            catch (Exception ex)
            {
                throw new Exception($"Veritabanı bağlantı hatası: {ex.Message}");
            }
        }
        public void bilgiall()
        {
            string kullanıcı = txtUsername.Text;
            string şifre = txtPassword.Text;

        }

        private void txtUsername_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
