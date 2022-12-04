using BE;
using BLL;
using DevComponents.AdvTree;
using DevComponents.DotNetBar.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ProjectForMIRZAALIKHAN
{
    public partial class ProductForm : Form
    {
        public ProductForm()
        {
            InitializeComponent();
        }
        public ProductForm(User u)
        {
            InitializeComponent();
            Lu=u;
        }
        int id;
        ProductBLL bll = new ProductBLL();
        UserBLL Ubll = new UserBLL();
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
            
        }
        bool CHEKed()
        {
            bool isvalid = true;
            if (textBoxX1.Text == "")
            {
                MessageBox.Show("لطفا ابتدا نام محصول را وارد کنید");
                isvalid = false;
                textBoxX1.Focus();
            }
            if (textBoxX2.Text == "")
            {
                MessageBox.Show("لطفا کد محصول را وارد کنید");
                isvalid = false;
                textBoxX2.Focus();
            }
            return isvalid;
        }

        private void label11_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        User Lu = new User();

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (CHEKed())
            {

                Product p = new Product();
                p.nameP = textBoxX1.Text;
                p.Code = textBoxX2.Text;
                if (checkBox5.Checked)
                {
                    p.IsActive = true;
                }
                else
                {
                    p.IsActive = false;
                }
                if (buttonX1.Text == "ثبت اطلاعات")
                {
                    if (Ubll.Access(Lu, "مدیریت کالا", 2))
                    {

                        MessageBox.Show(bll.Create(p));

                    }
                    else
                    {
                        MessageBox.Show("شما اجازه ثبت کالا را ندارید");
                    }
                }
                else
                {

                        MessageBox.Show(bll.Update(p, id));
                        buttonX1.Text = "ثبت اطلاعات";

                }
            }
            FillDataGrid();
            ClearTxtBoxs();

        }
        int index;

        private void textBoxX3_TextChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked && checkBox3.Checked || (!checkBox3.Checked && !checkBox4.Checked))
            {
                index = 0;
            }
            else if (checkBox4.Checked && !checkBox3.Checked)
            {
                index = 1;
            }
            else if (checkBox3.Checked && !checkBox4.Checked)
            {
                index = 2;
            }
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = bll.Read(textBoxX3.Text, index);
            dataGridViewX1.Columns["id"].Visible = false;
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            FillDataGrid();

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

            if (Ubll.Access(Lu, "مدیریت کالا", 3))
            {
                Product p = bll.Read(id);
                textBoxX1.Text = p.nameP;
                textBoxX2.Text = p.Code;
                if (p.IsActive==true)
                {
                    checkBox5.Checked=true;
                }
                else
                {
                    checkBox5.Checked = false;
                }
                buttonX1.Text = "ویرایش اطلاعات";
            }
            else
            {
                MessageBox.Show("شما اجازه ویرایش کالا را ندارید");
            }
        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (Ubll.Access(Lu, "مدیریت کالا", 4))
            {
                DialogResult dr = MessageBox.Show("آیا از حذف کالا اطمینان دارید؟", "اخطار", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    MessageBox.Show(bll.Delete(id));
                }
                FillDataGrid();
            }
            else
            {
                MessageBox.Show("شما اجازه حذف کالا را ندارید");
            }
        }

        private void ProductForm_KeyUp(object sender, KeyEventArgs e)
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

        private void textBoxX2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\b')
            {
                e.Handled = false;
                return;
            }
            if ((e.KeyChar < '0') || (e.KeyChar > '9'))
                e.Handled = true;
        }

    }
}
