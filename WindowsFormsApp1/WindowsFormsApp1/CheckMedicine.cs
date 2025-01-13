using PHARMACY_MANAGEMENT_SYSTEM;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class CheckMedicine : Form
    {
        // Veritabanı işlemleri için yardımcı sınıf
        Function fn = new Function();
        // SQL sorgusu tutmak için değişken
        string query;

        public CheckMedicine()
        {
            InitializeComponent();
        }

        // Form yüklendiğinde çalışan metod
        private void CheckMedicine_Load(object sender, EventArgs e)
        {
            query = "select * from medic";
            setDataGridView(query);
        }


        String medicineID;
        private void gridView_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                var medicineID = gridView1.GetRowCellValue(e.RowHandle, gridView1.Columns[6]).ToString();

            }
            catch { }
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            // Pharmacist sayfasını aç
            Pharmacist pharmForm = new Pharmacist(); // pharmForm, yönlendirmek istediğiniz formun adıdır
            pharmForm.Show();

            // Mevcut formu kapat
            this.Close();
        }

  
        private void setDataGridView(string query)
        {
            DataSet ds = fn.getData(query);
            gridControl1.DataSource = ds.Tables[0];
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you Sure?", "Delete Confirmation !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                query = "delete from medic where mid = '" + medicineID + "'";
                fn.setData(query, "Medicine Record Deleated.");
                CheckMedicine_Load(this, null);
            }
        }

        private void searchControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void searchControl1_TextChanged(object sender, EventArgs e)
        {

            query = "select * from medic where mname like '" + searchControl1.Text + "'";
            setDataGridView(query);
        }
    }
}
