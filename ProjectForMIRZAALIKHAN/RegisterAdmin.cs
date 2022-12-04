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
    public partial class RegisterAdmin : UserControl
    {
        public RegisterAdmin()
        {
            InitializeComponent();
        }
        Timer t1 = new Timer();

        UserGroupBLL UGbll = new UserGroupBLL();
        UserAccessRole FillAccessRole(string Section, bool CanEnter, bool CanCreate, bool CanUpdate, bool CanDelete)
        {
            UserAccessRole uar = new UserAccessRole();
            uar.Section = Section;
            uar.CanEnter = CanEnter;
            uar.CanCreate = CanCreate;
            uar.CanUpdate = CanUpdate;
            uar.CanDelete = CanDelete;
            return uar;
        }
        void CreateAdminGroup()
        {
            UserGroup ug = new UserGroup();
            ug.Title = "مدیریت";
            ug.UserAccessRoles.Add(FillAccessRole("مدیریت یوزر", true, true, true, true));
            ug.UserAccessRoles.Add(FillAccessRole("مدیریت کالا", true, true, true, true));

            UGbll.Create(ug);
        }
        OpenFileDialog ofd = new OpenFileDialog();
        UserBLL Ubll = new UserBLL();

        int y = 153;



        private void label11_Click(object sender, EventArgs e)
        {
            User u = new User();
            CreateAdminGroup();
            u.name = textBoxX2.Text;
            if (textBoxX1.Text == textBoxX3.Text)
            {
                u.family = textBoxX1.Text;
            }
            else
            {
                MessageBox.Show("کلمه عبور و تکرار آن با یکدیگر همخوانی ندارند");
            }
            MessageBox.Show(Ubll.Create(u, UGbll.ReadN("مدیریت")));
            this.Visible = false;
            ((LoginForm)Application.OpenForms["LoginForm"]).LoadLoginForm();
        }
    }
}
