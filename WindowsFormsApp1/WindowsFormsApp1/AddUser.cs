using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class AddUser : Form
    {
        public AddUser()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            // Administrator sayfasını aç
            Administrator adminForm = new Administrator(); 
            adminForm.Show();

            //Mevcut formu kapat
            this.Close();
        }
        
        private void btnSignIn_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Kullanıcıdan alınan verileri değişkenlere ata
                string role = txtUserRole.Text;
                if (string.IsNullOrWhiteSpace(role))
                {
                    MessageBox.Show("Lütfen bir rol seçin!");
                    return;
                }

                string name = txtName.Text;                       // Kullanıcı adı
                string dob = txtdob.Text;                         // Doğum tarihi
                Int64 mobile = Int64.Parse(txtMobile.Text);       // Telefon numarası
                string email = txtEmail.Text;                     // E-posta adresi
                string username = txtUsername.Text;               // Kullanıcı adı
                string pass = txtPassword.Text;                   // Şifre

                // SQL sorgusunu oluştur
                string query = "INSERT INTO users (userRole, name, dob, mobile, email, username, pass) VALUES (@role, @name, @dob, @mobile, @email, @username, @pass)";

                // Veritabanı bağlantı dizgesi
                string connectionString = "Server=DESKTOP-E23C3JA\\SQLEXPRESS01;Database=pharmacy;Integrated Security=True;";

                // SQL bağlantısını başlat
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open(); // Bağlantıyı aç
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Parametreleri ekle (SQL enjeksiyonuna karşı koruma)
                        command.Parameters.AddWithValue("@role", role);
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@dob", dob);
                        command.Parameters.AddWithValue("@mobile", mobile);
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@pass", pass);

                        // Sorguyu çalıştır ve etkilenen satır sayısını al
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0) // Eğer en az bir satır etkilendiyse
                        {
                            MessageBox.Show("Başarıyla eklendi."); // Başarılı mesajı
                            ClearAll(); // Alanları temizle
                        }
                        else
                        {
                            MessageBox.Show("An error occurred while adding the user."); // Hata mesajı
                        }
                    }
                }
            }
            catch (FormatException) // Telefon numarası yanlış formatta girilirse
            {
                MessageBox.Show("Lütfen geçerli bir telefon numarası girin.");
            }
            catch (Exception ex) // Genel bir hata oluşursa
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
        // Tüm giriş alanlarını temizleyen metot
        public void ClearAll()
        {
            txtName.Text = string.Empty;         // İsim alanını temizle
            txtdob.Text = string.Empty;         // Doğum tarihi alanını temizle
            txtEmail.Text = string.Empty;       // E-posta alanını temizle
            txtMobile.Text = string.Empty;      // Telefon alanını temizle
            txtUsername.Text = string.Empty;    // Kullanıcı adı alanını temizle
            txtPassword.Text = string.Empty;    // Şifre alanını temizle
            txtUserRole.Text = string.Empty; // Alanı temizle
        }
    }
}
