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
    public partial class AddMedicine : Form
    {
        Function fn = new Function();
        String query;

        public AddMedicine()
        {
            InitializeComponent(); // Form bileşenlerini başlatma
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            // Pharmacist sayfasını aç
            Pharmacist pharmForm = new Pharmacist(); // pharmForm, yönlendirmek istediğiniz formun adıdır
            pharmForm.Show();

            // Mevcut formu kapat
            this.Close();
        }

        private void btnSignIn_Click_1(object sender, EventArgs e)
        {
            // Kullanıcıdan alınan tüm verilerin boş olmadığını kontrol etme
            if (txtMedicID.Text != "" && txtMedicName.Text != "" && txtMedicNo.Text != "" && txtQuantity.Text != "" && txtPricePerUnit.Text != "")
            {
                // Kullanıcıdan alınan verileri değişkenlere atama
                String mid = txtMedicID.Text;
                String mname = txtMedicName.Text;
                String mnumber = txtMedicNo.Text;
                String mDate = txtMDate.Text;
                String eDate = txtEDate.Text;
                Int64 quantity = Int64.Parse(txtQuantity.Text); // Miktarı tam sayıya dönüştürme
                Int64 perUnit = Int64.Parse(txtPricePerUnit.Text); // Birim fiyatı tam sayıya dönüştürme

                // Veritabanına veri eklemek için SQL sorgusunu hazırlama
                query = "INSERT INTO medic (mid, mname, mnumber, mDate, eDate, quantity, perUnit) VALUES (@mid, @mname, @mnumber, @mDate, @eDate, @quantity, @perUnit)";

                // SQL komutunu çalıştıracak SqlCommand nesnesi oluşturma
                SqlCommand cmd = new SqlCommand(query);

                // Parametreleri ekleyip değerlerini atama
                cmd.Parameters.AddWithValue("@mid", mid);
                cmd.Parameters.AddWithValue("@mname", mname);
                cmd.Parameters.AddWithValue("@mnumber", mnumber);
                cmd.Parameters.AddWithValue("@mDate", mDate);
                cmd.Parameters.AddWithValue("@eDate", eDate);
                cmd.Parameters.AddWithValue("@quantity", quantity);
                cmd.Parameters.AddWithValue("@perUnit", perUnit);

                // Veritabanına veri eklemek için sorguyu çalıştırma
                fn.setData(query, "Medic Added to Database.");
            }
            else
            {
                // Kullanıcı gerekli verileri girmemişse uyarı gösterme
                MessageBox.Show("Tüm alanları doldurunuz.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnReset_Click_1(object sender, EventArgs e)
        {
            clearAll();
        }

        // Formdaki tüm metin kutularını temizleme fonksiyonu
        public void clearAll()
        {
            txtMedicID.Text = string.Empty;
            txtMedicName.Text = string.Empty;
            txtMedicNo.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            txtPricePerUnit.Text = string.Empty;
            txtMDate.Text = string.Empty; 
            txtEDate.Text = string.Empty;
        }
    }
}
