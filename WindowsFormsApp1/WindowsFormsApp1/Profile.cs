using DevExpress.Utils;
using PHARMACY_MANAGEMENT_SYSTEM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Profile : Form
    {
        Function fn = new Function(); // SQL bağlantısı için.
        string query;

        public Profile()
        {
            InitializeComponent();
        }
        private void Profile_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtName.Text))
            {
                LoadUserData(txtName.Text);
            }
        }
        public string ID
        {
            set { UserNameLabel.Text = value; }
        }
        
        private void LoadUserData(string  username)
        {
            query = "SELECT * FROM users WHERE username = @username";
            using (SqlConnection con = fn.getConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    try
                    {
                        con.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataSet dt = new DataSet();
                        adapter.Fill(dt);

                        if (dt.Tables[0].Rows.Count > 0)
                        {
                            txtUserRole.Text = dt.Tables[0].Rows[0]["userRole"].ToString();
                            txtName.Text = dt.Tables[0].Rows[0]["name"].ToString();
                            txtdob.Text = dt.Tables[0].Rows[0]["dob"].ToString();
                            txtMobile.Text = dt.Tables[0].Rows[0]["mobile"].ToString();
                            txtEmail.Text = dt.Tables[0].Rows[0]["email"].ToString();
                            txtPassword.Text = dt.Tables[0].Rows[0]["pass"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Kullanıcı bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Veri çekme sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
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

        

        private void Profile_Enter(object sender, EventArgs e)
        {
          
        }

        private void btnSignIn_Click_1(object sender, EventArgs e)
        {
            String role = txtUserRole.Text;
            String name = txtName.Text;
            String dob = txtdob.Text;
            Int64 mobile = Int64.Parse(txtMobile.Text);
            String email = txtEmail.Text;
            String username = UserNameLabel.Text;
            String pass = txtPassword.Text;

            string query = "UPDATE users SET userRole = @role, name = @name, dob = @dob, mobile = @mobile, email = @email, pass = @pass WHERE username = @username";
            fn.setData(query, "Profile Updation Successful.");
            using (SqlConnection con = fn.getConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@role", role);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@dob", dob);
                    cmd.Parameters.AddWithValue("@mobile", mobile);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@pass", pass);
                    cmd.Parameters.AddWithValue("@username", username);
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Profil başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Profil güncelleme sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Profile_Load(this, null);
        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {

        }
        
    }
}
      

  
