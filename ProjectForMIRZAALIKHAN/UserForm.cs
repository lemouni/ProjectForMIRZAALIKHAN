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
    public partial class UserForm : Form
    {
        public UserForm()
        {
            InitializeComponent();
        }
        public UserForm(User u)
        {
            InitializeComponent();
            Lu=u;
        }
        int id;
        UserBLL bll = new UserBLL();
        UserGroupBLL UGbll = new UserGroupBLL();
        UserAccessRoleBLL UAbll = new UserAccessRoleBLL();
        OpenFileDialog ofd = new OpenFileDialog();
        void FillDataGrid2()
        {
            dataGridViewX2.DataSource = null;
            dataGridViewX2.DataSource = UGbll.Read();
            dataGridViewX2.Columns["id"].Visible = false;
        }
        void FillDataGrid()
        {
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = bll.Read();
            dataGridViewX1.Columns["id"].Visible = false;
        }
        void ClearTxtBoxs()
        {

            textBoxX1.Text = "";
            textBoxX2.Text = "";
            textBoxX3.Text = "";

        }
        bool CHEKed()
        {
            bool isvalid = true;
            if (textBoxX1.Text == "")
            {
                MessageBox.Show("لطفا ابتدا نام و نام خانوادگی کاربر را وارد کنید");
                isvalid = false;
                textBoxX1.Focus();
            }
            if (textBoxX2.Text == "")
            {
                MessageBox.Show("لطفا نام کاربری را وارد کنید");
                isvalid = false;
                textBoxX2.Focus();
            }
            else if (textBoxX3.Text == "")
            {
                MessageBox.Show("لطفا کلمه عبور را وارد کنید");
                isvalid = false;
                textBoxX3.Focus();
            }

            return isvalid;
        }
        bool CHEKed1()
        {
            bool isvalid = true;
            if (textBoxX10.Text == "")
            {
                MessageBox.Show("لطفا ابتدا نام گروه کاربری را وارد کنید");
                isvalid = false;
                textBoxX10.Focus();
            }
            return isvalid;
        }
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

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        User Lu = new User();

        private void label1_Click(object sender, EventArgs e)
        {

            if (CHEKed())
            {
                User u = new User();
                u.name = textBoxX1.Text;
                UserGroup ug = UGbll.ReadN(comboBoxEx1.Text);

                if (textBoxX2.Text == textBoxX3.Text)
                {
                    u.family = textBoxX2.Text;
                }
                else
                {
                    MessageBox.Show("تکرار کلمه عبور درست وارد نشده است");
                }
                if (label1.Text == "ثبت اطلاعات")
                {


                        if (bll.Access(Lu, "مدیریت یوزر", 2))
                        {
                            MessageBox.Show(bll.Create(u, ug));
                        }
                        else
                        {
                        MessageBox.Show("شما اجازه ثبت یوزر را ندارید");
                        }


                }
                else
                {

                    MessageBox.Show(bll.Update(u, ug, id));
                    label1.Text = "ثبت اطلاعات";
                }
                FillDataGrid();
                ClearTxtBoxs();
            }

        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            FillDataGrid2();
            FillDataGrid();
            foreach (var item in UGbll.ReadUGNames())
            {
                comboBoxEx1.Items.Add(item);
            }

        }

        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
                id = Convert.ToInt32(dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells["id"].Value);


            }
            catch (Exception)
            {

                MessageBox.Show("این سطر خالی می باشد");
            }
        }

        private void ویرایشToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (bll.Access(Lu, "مدیریت یوزر", 3))
            {
                User u = bll.Read(id);
                textBoxX1.Text = u.name;
                textBoxX2.Text = u.family;




                label1.Text = "ویرایش اطلاعات";
            }
            else
            {
                MessageBox.Show("شما اجازه آپدیت یوزر را ندارید");
            }
        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (bll.Access(Lu, "مدیریت یوزر", 4))
            {
                DialogResult dr = MessageBox.Show("آیا از حذف مشتری اطمینان دارید؟", "اخطار", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    bll.Delete(id);
                }
                FillDataGrid();
            }
            else
            {
                MessageBox.Show("شما اجازه حذف یوزر را ندارید");
            }
        }

        private void UserForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if
                (
                    MessageBox.Show
                    (
                        "آیا میخواهید فرم را ببندید؟",
                        "بستن فرم",
                        MessageBoxButtons.YesNo,

                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button1 // hit Enter == No !
                    )
                    == DialogResult.Yes
                )
                {
                    this.Close();
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox8.Checked = true;
                checkBox12.Checked = true;


            }
            else
            {
                checkBox8.Checked = false;
                checkBox12.Checked = false;


            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox7.Checked = true;
                checkBox11.Checked = true;


            }
            else
            {
                checkBox7.Checked = false;
                checkBox11.Checked = false;


            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                checkBox6.Checked = true;
                checkBox10.Checked = true;


            }
            else
            {
                checkBox6.Checked = false;
                checkBox10.Checked = false;


            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                checkBox5.Checked = true;
                checkBox9.Checked = true;


            }
            else
            {
                checkBox5.Checked = false;
                checkBox9.Checked = false;


            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

            if (CHEKed1())
            {
                UserGroup ug = new UserGroup();
                ug.Title = textBoxX10.Text;
                ug.UserAccessRoles.Add(FillAccessRole(label6.Text, checkBox8.Checked, checkBox7.Checked, checkBox6.Checked, checkBox5.Checked));
                ug.UserAccessRoles.Add(FillAccessRole(label7.Text, checkBox12.Checked, checkBox11.Checked, checkBox10.Checked, checkBox9.Checked));


                if (bll.Access(Lu, "مدیریت یوزر", 2))
                {
                    MessageBox.Show(UGbll.Create(ug));
                }
                else
                {
                   MessageBox.Show("شما اجازه ثبت سطح کاربری را ندارید");
                }
                FillDataGrid2();
            }
        }

        private void dataGridViewX2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                contextMenuStrip2.Show(Cursor.Position.X, Cursor.Position.Y);
                id = Convert.ToInt32(dataGridViewX2.Rows[dataGridViewX2.CurrentRow.Index].Cells["id"].Value);


            }
            catch (Exception)
            {

                MessageBox.Show("این سطر خالی می باشد");
            }
        }

        private void ورودفعالToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (bll.Access(Lu, "مدیریت یوزر", 3))
            {
                DialogResult dr =MessageBox.Show("در صورت انجام این رویداد تیک دسترسی ورود فعال می شود\nآیا این دسترسی را آزاد میکنید؟");
                if (dr == DialogResult.Yes)
                {
                    UAbll.DoneEnter(id);
                }
                FillDataGrid2();
            }
            else
            {
                MessageBox.Show("شما اجازه آپدیت سطح کاربری را ندارید");
            }
        }

        private void ورودغیرفعالToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (bll.Access(Lu, "مدیریت یوزر", 3))
            {
                DialogResult dr = MessageBox.Show("در صورت انجام این رویداد تیک دسترسی ورود فعال می شود\nآیا این دسترسی را آزاد میکنید؟");
                if (dr == DialogResult.Yes)
                {
                    UAbll.NotDoneEnter(id);
                }
                FillDataGrid2();
            }
            else
            {
                MessageBox.Show("شما اجازه آپدیت سطح کاربری را ندارید");
            }
        }

        private void ویرایشفعالToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (bll.Access(Lu, "مدیریت یوزر", 3))
            {
                DialogResult dr = MessageBox.Show("در صورت انجام این رویداد تیک دسترسی ورود فعال می شود\nآیا این دسترسی را آزاد میکنید؟");
                if (dr == DialogResult.Yes)
                {
                    UAbll.DoneUpdate(id);
                }
                FillDataGrid2();
            }
            else
            {
                MessageBox.Show("شما اجازه آپدیت سطح کاربری را ندارید");
            }
        }

        private void ویرایشغیرفعالToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (bll.Access(Lu, "مدیریت یوزر", 3))
            {
                DialogResult dr = MessageBox.Show("در صورت انجام این رویداد تیک دسترسی ورود فعال می شود\nآیا این دسترسی را آزاد میکنید؟");
                if (dr == DialogResult.Yes)
                {
                    UAbll.NotDoneUpdate(id);
                }
                FillDataGrid2();
            }
            else
            {
                MessageBox.Show("شما اجازه آپدیت سطح کاربری را ندارید");
            }
        }

        private void افزودنفعالToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (bll.Access(Lu, "مدیریت یوزر", 3))
            {
                DialogResult dr = MessageBox.Show("در صورت انجام این رویداد تیک دسترسی ورود فعال می شود\nآیا این دسترسی را آزاد میکنید؟");
                if (dr == DialogResult.Yes)
                {
                    UAbll.DoneCreate(id);
                }
                FillDataGrid2();
            }
            else
            {
                MessageBox.Show("شما اجازه آپدیت سطح کاربری را ندارید");
            }
        }

        private void افزودنغیرفعالToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (bll.Access(Lu, "مدیریت یوزر", 3))
            {
                DialogResult dr = MessageBox.Show("در صورت انجام این رویداد تیک دسترسی ورود فعال می شود\nآیا این دسترسی را آزاد میکنید؟");
                if (dr == DialogResult.Yes)
                {
                    UAbll.NotDoneCreate(id);
                }
                FillDataGrid2();
            }

            else
            {
                MessageBox.Show("شما اجازه آپدیت سطح کاربری را ندارید");
            }
        }

        private void حذففعالToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (bll.Access(Lu, "مدیریت یوزر", 3))
            {
                DialogResult dr = MessageBox.Show("در صورت انجام این رویداد تیک دسترسی ورود فعال می شود\nآیا این دسترسی را آزاد میکنید؟");
                if (dr == DialogResult.Yes)
                {
                    UAbll.DoneDelete(id);
                }
                FillDataGrid2();
            }
            else
            {
                MessageBox.Show("شما اجازه آپدیت سطح کاربری را ندارید");
            }
        }

        private void حذفغیرفعالToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (bll.Access(Lu, "مدیریت یوزر", 3))
            {
                DialogResult dr = MessageBox.Show("در صورت انجام این رویداد تیک دسترسی ورود فعال می شود\nآیا این دسترسی را آزاد میکنید؟");
                if (dr == DialogResult.Yes)
                {
                    UAbll.NotDoneDelete(id);
                }
                FillDataGrid2();
            }
            else
            {
                MessageBox.Show("شما اجازه آپدیت سطح کاربری را ندارید");
            }
        }
    }
}
