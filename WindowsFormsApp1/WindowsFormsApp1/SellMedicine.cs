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
    public partial class SellMedicine : Form
    {
        Function fn = new Function();
        String query;
        DataSet ds;
        Form1 form = new Form1();
        

        public SellMedicine()
        {
            InitializeComponent(); // Form bileşenlerini başlatma
        }

        // Form yüklendiğinde ilaç listesini yükleyen metot
        private void SellMedicine_Load(object sender, EventArgs e)
        {
            listBoxControl1.Items.Clear(); // Listeyi temizle
            query = "select mname from medic where eDate >= mDate and quantity >0"; // Geçerli ilaçları çekmek için sorgu
            ds = fn.getData(query); // Veritabanından verileri al
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                listBoxControl1.Items.Add(ds.Tables[0].Rows[i][0].ToString()); // İlaç adlarını listeye ekle
            }
        }

        // Arama kutusunda değişiklik yapıldığında çağrılır
        private void txtArama_EditValueChanged(object sender, EventArgs e)
        {
            listBoxControl1.Items.Clear(); // Listeyi temizle
            query = "select mname from medic where mname like '" + txtArama.Text + "%' and eDate >= getData() and quantity >0'"; // Arama kriterine göre sorgu
            ds = fn.getData(query); // Veritabanından verileri al
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                listBoxControl1.Items.Add(ds.Tables[0].Rows[i][0].ToString()); // Arama sonucu listeyi doldur
            }
        }

        // Listeden bir ilaç seçildiğinde çağrılır
        private void listBoxControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtQuantity.Text = string.Empty; // Miktar alanını sıfırla

            if (listBoxControl1.SelectedItem != null)
            {
                string name = listBoxControl1.SelectedItem.ToString(); // Seçilen ilaç adı
                txtMedicName.Text = name; // İlaç adı textbox'ına yaz

                // SQL sorgusu oluşturuluyor
                query = "select mid, eDate, perUnit from medic where mname = '" + name + "'";
                ds = fn.getData(query);

                // Veri varsa, ilacın bilgilerini textbox'lara yaz
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtMedicID.Text = ds.Tables[0].Rows[0][0].ToString();
                    txtEDate.Text = ds.Tables[0].Rows[0][1].ToString();
                    txtTotalPrice.Text = ds.Tables[0].Rows[0][2].ToString();
                }
                else
                {
                    MessageBox.Show("Seçilen ilaç için veri bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Bir ilaç seçilmedi. Lütfen listeden bir öğe seçiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Miktar kutusundaki metin değiştiğinde çağrılır
        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            if (txtQuantity.Text != string.Empty)
            {
                Int64 unitPrice = Int64.Parse(txtTotalPrice.Text); // Birim fiyatı al
                Int64 noOfUnit = Int64.Parse(txtQuantity.Text); // Satın alınan birim sayısını al
                Int64 totalAmount = unitPrice * noOfUnit; // Toplam fiyatı hesapla
                txtTotalPrice.Text = totalAmount.ToString(); // Toplam fiyatı göster
            }
            else
            {
                txtTotalPrice.Text = string.Empty; // Miktar boşsa toplam fiyatı temizle
            }
        }

        protected int n, totalAmount = 0;
        protected Int64 quantity, newQuantity;
        int valueAmount;
        string valueID;

        // Satın alma işlemini gerçekleştiren metot
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (txtMedicID.Text != string.Empty)
            {
                query = "select quantity from medic where mid = '" + txtMedicID.Text + "'";
                ds = fn.getData(query);

                quantity = Int64.Parse(ds.Tables[0].Rows[0][0].ToString());
                newQuantity = quantity - Int64.Parse(txtQuantity.Text); // Yeni miktar hesapla

                if (newQuantity >= 0)
                {
                    gridView1.AddNewRow(); // Yeni bir satır ekle
                    int n = gridView1.FocusedRowHandle; // Satırın handle'ını al

                    // Satıra veri ekle
                    gridView1.SetRowCellValue(n, "Column1", txtMedicID.Text);
                    gridView1.SetRowCellValue(n, "Column2", txtMedicName.Text);
                    gridView1.SetRowCellValue(n, "Column3", txtEDate.Text);
                    gridView1.SetRowCellValue(n, "Column4", txtPricePerUnit.Text);
                    gridView1.SetRowCellValue(n, "Column5", txtQuantity.Text);
                    gridView1.SetRowCellValue(n, "Column6", txtTotalPrice.Text);

                    // Satırda yapılan değişiklikleri kaydet
                    gridView1.UpdateCurrentRow();

                    totalAmount = totalAmount + int.Parse(txtTotalPrice.Text); // Toplam miktarı güncelle
                    totalLabel.Text = "Rs. " + totalAmount.ToString(); // Toplam fiyatı göster

                    query = "update medic set quantity = '" + newQuantity + "' where mid = '" + txtMedicID.Text + "'";
                    fn.setData(query, "Medicine Added."); // İlaç stoğu güncelle
                    gridView1.UpdateCurrentRow();
                }
                else
                {
                    MessageBox.Show("Medicine is Out of Stock.\n Only " + quantity + " Left", "Warning !!!", MessageBoxButtons.OK, MessageBoxIcon.Warning); // Yeterli stok yoksa uyarı göster
                }
                clearAll(); // Alanları temizle
                SellMedicine_Load(this, null); // Formu yenile
            }
            else
            {
                MessageBox.Show("Select Medicine First.", "Information !!", MessageBoxButtons.OK, MessageBoxIcon.Information); // İlk olarak ilaç seçilmesi gerektiğini bildir
            }
        }

        // Formu sıfırlamak için buton
        private void btnReset_Click(object sender, EventArgs e)
        {
            totalAmount = 0; // Toplam tutarı sıfırla
            totalLabel.Text = "Rs. 00"; // Toplam fiyatı sıfırla
            gridView1.RefreshData(); // Grid'i yenile
        }

        // Grid üzerinde satıra tıklama işlemi
        private void gridView_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                valueAmount = Convert.ToInt32(gridView1.GetRowCellValue(e.RowHandle, gridView1.Columns[5])); // Satırın toplam fiyatını al
                valueID = gridView1.GetRowCellValue(e.RowHandle, gridView1.Columns[0]).ToString(); // İlaç ID'sini al
                noOfUnit = Convert.ToInt64(gridView1.GetRowCellValue(e.RowHandle, gridView1.Columns[4])); // Birim sayısını al
            }
            catch (Exception)
            {
                // Hata oluşursa herhangi bir işlem yapılmaz
            }
        }

        // Formdaki tüm alanları temizleyen metot
        private void clearAll()
        {
            txtMedicID.Text = string.Empty;
            txtMedicName.Text = string.Empty;
            txtEDate.ResetText();
            txtPricePerUnit.Text = string.Empty;
            txtQuantity.Text = string.Empty;
        }
        

        private void btnExit_Click(object sender, EventArgs e)
        {
            // Pharmacist sayfasını aç
            Pharmacist pharmForm = new Pharmacist(); // pharmForm, yönlendirmek istediğiniz formun adıdır
            pharmForm.Show();

            // Mevcut formu kapat
            this.Close();
        }

        private void panelControl2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void labelControl3_Click(object sender, EventArgs e)
        {

        }

        private void labelControl1_Click(object sender, EventArgs e)
        {

        }

        private void labelControl2_Click(object sender, EventArgs e)
        {

        }

        private void labelControl7_Click(object sender, EventArgs e)
        {

        }

        private void labelControl5_Click(object sender, EventArgs e)
        {

        }

        private void labelControl4_Click(object sender, EventArgs e)
        {

        }

        private void labelControl8_Click(object sender, EventArgs e)
        {

        }

        private void labelControl6_Click(object sender, EventArgs e)
        {

        }

        protected Int64 noOfUnit;

        // Satırı kaldırmak için buton
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (valueID != null)
            {
                try
                {
                    // Seçilen satırın index'ini alıyoruz
                    int selectedRowHandle = gridView1.GetSelectedRows()[0];

                    // Satırı kaldırıyoruz
                    gridView1.DeleteRow(selectedRowHandle);
                }
                catch
                {
                    // Hata durumunda herhangi bir işlem yapılmaz
                }
                finally
                {
                    // Satır silindiğinde, ilgili ilaç stoğunu güncelliyoruz
                    query = "selected quantity from medic where mid = '" + valueID ;
                    ds = fn.getData(query);
                    quantity = Int64.Parse(ds.Tables[0].Rows[0][0].ToString());
                    newQuantity = quantity + noOfUnit;

                    query = "update medic set quantity = '" + newQuantity + "' where mid = '" + valueID + "'";
                    fn.setData(query, "Medicine Removed from Cart."); // İlaç stoğu güncelleniyor
                    totalAmount = totalAmount - valueAmount; // Toplam tutar güncelleniyor
                    totalLabel.Text = "Rs. " + totalAmount.ToString(); // Güncel toplam fiyat gösteriliyor
                }
                SellMedicine_Load(this, null); // Formu yenile
            }
        }
    }
}
