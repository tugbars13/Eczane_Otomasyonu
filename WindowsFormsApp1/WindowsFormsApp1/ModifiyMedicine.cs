using DevExpress.Utils; // DevExpress bileşenlerini kullanmak için gerekli namespace
using PHARMACY_MANAGEMENT_SYSTEM; // Özel sınıfların olduğu namespace
using System;
using System.Data; // DataSet gibi veri işlemleri için gerekli namespace
using System.Data.SqlClient; // SQL işlemleri için gerekli namespace
using System.Windows.Forms; // Windows Forms uygulamaları için gerekli namespace

namespace WindowsFormsApp1
{
    public partial class ModifiyMedicine : Form
    {
        // Veritabanı işlemleri için kullanılan bir yardımcı sınıfın örneği
        Function fn = new Function();
        string query; // SQL sorgularını tutmak için kullanılan string değişken

        // Formun constructor metodu
        public ModifiyMedicine()
        {
            InitializeComponent();
        }
        
        private void btnExit_Click(object sender, EventArgs e)
        {
            // Pharmacist sayfasını aç
            Pharmacist pharmForm = new Pharmacist(); // pharmForm, yönlendirmek istediğiniz formun adıdır
            pharmForm.Show();

            // Mevcut formu kapat
            this.Close();
        }

        Int64 totalQuantity;
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            String mname = txtMedicName.Text;
            String mnumber = txtMedicNo.Text;
            String mDate = txtMDate.Text;
            String eDate = txtEDate.Text;
            Int64 quantity = Int64.Parse(txtAvailableQuantity.Text);
            Int64 addquantity = Int64.Parse(txtAddQuantity.Text);
            Int64 perUnit = Int64.Parse(txtPricePerUnit.Text);

            totalQuantity = addquantity + quantity;

            query = "update medic set mname = '" + mname + "',mnumber = '" + mnumber + "',mDate = '" + mDate + "',eDate = '" + eDate + "',quantity = '" + totalQuantity + "',perUnit = '" + perUnit + "' where mid = '" + txtMedicID.Text + "'";
            fn.setData(query, " Medicine Details Updated.");
        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            // Kullanıcı bir ilaç ID'si girdi mi kontrol edilir
            if (txtMedicID.Text != "")
            {
                // SQL sorgusu, güvenli parametrelerle
                query = "SELECT * FROM medic WHERE mid = @mid";

                try
                {
                    // Veritabanı bağlantısını açar
                    using (SqlConnection connection = fn.getConnection())
                    {
                        connection.Open(); // Bağlantıyı aç

                        // SQL komutu oluşturulur ve parametre atanır
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@mid", txtMedicID.Text);

                            // Veriyi almak için SqlDataAdapter kullanılır
                            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                            {
                                DataSet ds = new DataSet(); // Veri seti oluştur
                                adapter.Fill(ds); // Veri setini doldur

                                // Eğer sonuç varsa ilgili alanlara doldurulur
                                if (ds.Tables[0].Rows.Count != 0)
                                {
                                    txtMedicName.Text = ds.Tables[0].Rows[0]["mname"].ToString();
                                    txtMedicNo.Text = ds.Tables[0].Rows[0]["mnumber"].ToString();
                                    txtMDate.Text = ds.Tables[0].Rows[0]["mDate"].ToString();
                                    txtEDate.Text = ds.Tables[0].Rows[0]["eDate"].ToString();
                                    txtAddQuantity.Text = ds.Tables[0].Rows[0]["quantity"].ToString();
                                    txtPricePerUnit.Text = ds.Tables[0].Rows[0]["perUnit"].ToString();
                                }
                                else
                                {
                                    // Eğer sonuç yoksa kullanıcıya bilgi mesajı gösterilir
                                    MessageBox.Show("No Medicine with ID : " + txtMedicID.Text + " exists.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Hata oluşursa kullanıcıya hata mesajı gösterilir
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Eğer hiçbir ID girilmediyse tüm alanlar temizlenir
                clearAll();
            }
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            // Kullanıcıdan alınan veriler değişkenlere atanır
            string mname = txtMedicName.Text;
            string mnumber = txtMedicNo.Text;
            string mDate = txtMDate.Text;
            string eDate = txtEDate.Text;
            Int64 quantity = Int64.Parse(txtAvailableQuantity.Text);
            Int64 addquantity = Int64.Parse(txtAddQuantity.Text);
            Int64 perUnit = Int64.Parse(txtPricePerUnit.Text);

            // Toplam miktar hesaplanır
            Int64 totalQuantity = addquantity + quantity;

            // Güncelleme sorgusu
            query = "UPDATE medic SET mname = @mname, mnumber = @mnumber, mDate = @mDate, eDate = @eDate, quantity = @quantity, perUnit = @perUnit WHERE mid = @mid";

            try
            {
                // Veritabanı bağlantısını açar
                using (SqlConnection connection = fn.getConnection())
                {
                    connection.Open();

                    // SQL komutu oluşturulur ve parametreler atanır
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@mname", mname);
                        command.Parameters.AddWithValue("@mnumber", mnumber);
                        command.Parameters.AddWithValue("@mDate", mDate);
                        command.Parameters.AddWithValue("@eDate", eDate);
                        command.Parameters.AddWithValue("@quantity", totalQuantity);
                        command.Parameters.AddWithValue("@perUnit", perUnit);
                        command.Parameters.AddWithValue("@mid", txtMedicID.Text);

                        // Sorgu çalıştırılır
                        command.ExecuteNonQuery();
                        MessageBox.Show("Medicine Details Updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                // Hata oluşursa kullanıcıya hata mesajı gösterilir
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReset_Click_1(object sender, EventArgs e)
        {
            // Tüm giriş alanlarını temizler
            clearAll();
        }
        // Tüm alanları temizleyen yardımcı metod
        public void clearAll()
        {
            txtMedicID.Text = string.Empty;
            txtMedicName.Text = string.Empty;
            txtMedicNo.Text = string.Empty;
            txtMDate.Text = string.Empty;
            txtEDate.Text = string.Empty;
            txtAvailableQuantity.Text = string.Empty;
            txtPricePerUnit.Text = string.Empty;
            txtAddQuantity.Text = "0"; // Varsayılan olarak 0
        }
    }
}
