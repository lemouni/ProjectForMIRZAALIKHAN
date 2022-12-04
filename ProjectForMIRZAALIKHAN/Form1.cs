using BE;
using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Effects;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ProjectForMIRZAALIKHAN
{
    public partial class Form1 : Form 
    {

        public Form1()
        {

            InitializeComponent();
            

        }
        UserBLL Ubll = new UserBLL();
        public User LoggedInUser = new User();
        public static int userName;
        
        public void RefreshPage()
        {
            label2.Text = LoggedInUser.name;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (Ubll.Access(LoggedInUser, "مدیریت یوزر", 1))
            {
                UserForm f = new UserForm(LoggedInUser);
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("شما اجازه ورود به این قسمت را ندارید");
            }
            RefreshPage();

        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (Ubll.Access(LoggedInUser, "مدیریت کالا", 1))
            {
                ProductForm f = new ProductForm(LoggedInUser);
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("شما اجازه ورود به این قسمت را ندارید");
            }
            RefreshPage();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoginForm f = new LoginForm();
            f.ShowDialog();

        }
    }
}
