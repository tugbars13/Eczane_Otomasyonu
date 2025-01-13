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
    public partial class Administrator : Form
    {
        public Administrator()
        {
            InitializeComponent();  // Form bileşenlerini başlatır
        }

        private void Administrator_Load(object sender, EventArgs e)
        {
            // Form yüklendiğinde yapılacak işlemler
        }

        // "Kullanıcı Ekle" butonuna tıklanmasıyla AddUser formunun açılması
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            AddUser kullaniciEkleForm = new AddUser();  // AddUser formunu oluştur
            kullaniciEkleForm.Show();  // Yeni formu aç
            this.Hide();  // Mevcut formu gizle
        }

        // "Kullanıcı Ara" butonuna tıklanmasıyla KullanıcıAra formunun açılması
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            KullanıcıAra kullanıcıAraForm = new KullanıcıAra();  // KullanıcıAra formunu oluştur
            kullanıcıAraForm.Show();  // Yeni formu aç
            this.Hide();  // Mevcut formu gizle
        }

        // "Profil" butonuna tıklanmasıyla Profile formunun açılması
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            Profile profilForm = new Profile();  // Profile formunu oluştur
            profilForm.Show();  // Yeni formu aç
            this.Hide();  // Mevcut formu gizle
        }

        // "Çıkış" butonuna tıklanmasıyla uygulamayı kapatma

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Form1 fm = new Form1();  // Form1 oluştur
            fm.Show();  // Form1'i aç
            this.Hide();  // Mevcut formu gizle
        }

        private void panelControl5_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
