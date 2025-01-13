using PHARMACY_MANAGEMENT_SYSTEM; // Sistemde tanımlı bir Function sınıfını içeren namespace
using System;
using System.Data; // DataSet gibi veri sınıfları için gerekli namespace
using System.Data.SqlClient;
using System.Windows.Forms; // Form ve diğer kontrol bileşenleri için gerekli namespace

namespace WindowsFormsApp1
{
    public partial class KullanıcıAra : Form
    {
        Function fn = new Function();
        string query;
        string currentUser = ""; // Geçerli kullanıcı adı
        string userName = ""; // Seçilen kullanıcı adı
        
        public KullanıcıAra()
        {
            InitializeComponent();
        }

        // Bu property, başka bir formdan "currentUser" bilgisini alır
        public string ID
        {
            set { currentUser = value; }
        }
        
        private void KullanıcıAra_Load(object sender, EventArgs e)
        {
            // Kullanıcı bilgilerini veritabanından çeken sorgu
            query = "SELECT id, userRole, name, dob, mobile, email, username FROM users";

            // Sorgu sonucu alınır ve grid kontrolüne aktarılır
            DataSet ds = fn.getData(query);
            gridControl1.DataSource = ds.Tables[0]; 
        }

        // Yenileme butonuna tıklandığında çalışan metod
        private void btnSync_Click(object sender, EventArgs e)
        {
            // Kullanıcı bilgilerini yeniden yükler
            KullanıcıAra_Load(this, null);
        }

        //Reset butonuna tıklandığında çalışan metod
        private void btnReset_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Delete Confirmation!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (!currentUser.Equals(userName, StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        // Parametreli sorgu ile kullanıcıyı siler
                        query = "DELETE FROM users WHERE username = @username";
                        using (SqlConnection connection = new SqlConnection())
                        {
                            connection.Open();
                            using (SqlCommand cmd = new SqlCommand(query, connection))
                            {
                                cmd.Parameters.AddWithValue("@username", userName);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        MessageBox.Show("User record deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Güncel kullanıcı listesini yeniden yükler
                        KullanıcıAra_Load(this, null);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred while deleting the user: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("You are trying to delete your own profile.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }



        // Grid'deki bir satıra tıklandığında çalışan metod
        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                // Seçilen satırdaki "username" hücresindeki veriyi alır
                userName = gridView1.GetRowCellValue(e.RowHandle, gridView1.Columns[6]).ToString();
            }
            catch (Exception ex)
            {
                // Hata oluşursa bir mesaj gösterir
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
        
        private void btnExit_Click(object sender, EventArgs e)
        {
            // Administrator sayfasını aç
            Administrator adminForm = new Administrator(); // AdminForm, yönlendirmek istediğiniz formun adıdır
            adminForm.Show();

            // Mevcut formu kapat
            this.Close();
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            query = "select * from users where username like '" + txtUsername.Text + "'";
            DataSet ds = fn.getData(query);
            gridControl1.DataSource = ds.Tables[0];
        }

        private void txtUsername_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
