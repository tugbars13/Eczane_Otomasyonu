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
    public partial class Pharmacist : Form
    {
        public Pharmacist()
        {
            InitializeComponent();
        }

        

        private void simpleButton3_Click_1(object sender, EventArgs e)
        {
            AddMedicine İacEklemeFormu = new AddMedicine();
            İacEklemeFormu.Show(); // Yeni formu açar
            this.Hide(); // Mevcut formu gizler
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            viewMedicine ilacBulForm = new viewMedicine();
            ilacBulForm.Show(); // Yeni formu açar
            this.Hide(); // Mevcut formu gizler
        }

        private void simpleButton7_Click_1(object sender, EventArgs e)
        {
            CheckMedicine kontrolForm = new CheckMedicine();
            kontrolForm.Show();
            this.Hide();
        }

        private void simpleButton6_Click_1(object sender, EventArgs e)
        {
            SellMedicine paraForm = new SellMedicine();
            paraForm.Show();
            this.Hide();
        }

        private void simpleButton4_Click_1(object sender, EventArgs e)
        {
            ModifiyMedicine ilacGunForm = new ModifiyMedicine();
            ilacGunForm.Show();
            this.Hide();
        }

        private void simpleButton5_Click_1(object sender, EventArgs e)
        {
            Form1 fm = new Form1();
            fm.Show();
            this.Hide();
        }
    }
}
