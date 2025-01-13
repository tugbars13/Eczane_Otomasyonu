using DevExpress.Utils;
using PHARMACY_MANAGEMENT_SYSTEM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class viewMedicine : Form
    {
        Function fn = new Function();
        String query;

        public viewMedicine()
        {
            InitializeComponent();
        }

        private void viewMedicine_Load(object sender, EventArgs e)
        {
            query = "select * from medic";
            setDataGridView(query);

        }
        private void setDataGridView(string query)
        {
            DataSet ds = fn.getData(query);
            gridControl1.DataSource = ds.Tables[0];
        }


        private void txtMedicname_EditValueChanged(object sender, EventArgs e)
        {
            query = "select * from medic where mname like '" + txtMedicname.Text + "%'";
            setDataGridView(query);

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            DataSet ds = fn.getData(query);
            gridControl1.DataSource = ds.Tables[0];
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

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you Sure?", "Delete Confirmation !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                query = "delete from medic where mid = '" + medicineID + "'";
                fn.setData(query, "Medicine Record Deleated.");
                viewMedicine_Load(this, null);
            }

        }
        

        private void btnExit_Click(object sender, EventArgs e)
        {
            // Pharmacist sayfasını aç
            Pharmacist pharmForm = new Pharmacist(); // pharmForm, yönlendirmek istediğiniz formun adıdır
            pharmForm.Show();

            // Mevcut formu kapat
            this.Close();
        }
    }
}
