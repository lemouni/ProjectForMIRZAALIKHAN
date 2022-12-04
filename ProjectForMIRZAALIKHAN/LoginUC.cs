using BE;
using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectForMIRZAALIKHAN
{
    public partial class LoginUC : UserControl
    {
        public LoginUC()
        {
            InitializeComponent();
            if (Properties.Settings.Default.UserName != string.Empty)
            {
                textBoxX1.Text = Properties.Settings.Default.UserName;
                textBoxX2.Text = Properties.Settings.Default.Password;
            }
        }
        UserBLL Ubll = new UserBLL();
        User u = new User();

        private void label11_Click(object sender, EventArgs e)
        {
            u = Ubll.Login(textBoxX1.Text, textBoxX2.Text);
            if (u != null)
            {
                //MessageBox.Show("برای ورود به نرم افزار روی خروج کلیک کنید");
                Form1 w = ((Form1)System.Windows.Forms.Application.OpenForms["Form1"]);
                //var w = new Form1();
                //Form1 w = new Form1();
                w.LoggedInUser = u;
                w.RefreshPage();
                //w.ShowDialog();
                
                ((LoginForm)System.Windows.Forms.Application.OpenForms["LoginForm"]).Close();

            }
            else
            {
                MessageBox.Show("نام کاربری یا رمز عبور اشتباه است");
            }

            if (checkBox1.Checked)
            {
                Properties.Settings.Default.UserName = textBoxX1.Text;
                Properties.Settings.Default.Password = textBoxX2.Text;
                Properties.Settings.Default.Save();
            }
        }

        private void LoginUC_Load(object sender, EventArgs e)
        {
            AutoCompleteStringCollection HBnames = new AutoCompleteStringCollection();
            foreach (var item in Ubll.ReadNmaes())
            {
                HBnames.Add(item);
            }
            textBoxX1.AutoCompleteCustomSource = HBnames;
        }
    }
}
