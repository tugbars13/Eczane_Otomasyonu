using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    static class Program
    {
        /// <summary>
        /// Uygulamanın ana girdi noktası.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new Form1()); // Ana formunuzun doğru ismini yazdığınızdan emin olun
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Uygulama Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
