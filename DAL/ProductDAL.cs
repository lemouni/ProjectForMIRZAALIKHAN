using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ProductDAL
    {
        DB db = new DB();
        public string Create(Product p)
        {
            if (Read(p))
            {
                db.Products.Add(p);
                db.SaveChanges();
                return "ثبت اطلاعات با موفقیت انجام شد";
            }
            else
            {
                return "یوزر با این نام وجود دارد";
            }

        }
        public bool Read(Product p)
        {
            var q = db.Products.Where(i => i.nameP == p.nameP);
            if (q.Count() == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<ProductViewModel> ReadViewModel()
        {
            List<ProductViewModel> lst = new List<ProductViewModel>();
            IQueryable<Product> query = db.Products;
            //query = query.Include("User");
            foreach (var item in query.ToList())
            {
                lst.Add(new ProductViewModel()
                {
                    id = item.id,
                    nameP = item.nameP,
                    Code = item.Code,
                    IsActive = item.IsActive,

                });
            }
            return lst;
        }
        public DataTable Read()
        {
            string cmd = "SELECT id, nameP AS [نام محصول], Code AS [کد محصول], IsActive AS [وضعیت فعالی] FROM dbo.Product";
            SqlConnection con = new SqlConnection("data source=.;initial catalog=x;integrated security=True");
            var sqladapter = new SqlDataAdapter(cmd, con);
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];
        }
        public Product Read(int id)
        {
            return db.Products.Where(i => i.id == id).FirstOrDefault();
        }
        public DataTable Read(string s, int index)
        {
            SqlCommand cmd = new SqlCommand();
            if (index == 0)
            {
                cmd.CommandText = "dbo.SearchProduct1";

            }
            else if (index == 1)
            {
                cmd.CommandText = "dbo.SearchProductname";
            }
            else if (index == 2)
            {
                cmd.CommandText = "dbo.SearchProductcode";
            }
            SqlConnection con = new SqlConnection("data source=.;initial catalog=x;integrated security=True");
            cmd.Parameters.AddWithValue("@search", s);
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            //string cmd = "exec dbo.SearchCustomer";
            var sqladapter = new SqlDataAdapter();
            sqladapter.SelectCommand = cmd;
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];
        }
        public string Delete(int id)
        {
            try
            {
                var q = db.Products.Where(i => i.id == id).SingleOrDefault();
                if (q != null)
                {

                     db.Products.Remove(q);
                     db.SaveChanges();
                     return "حذف فاکتور با موفقیت انجام شد";


                }
                else
                {
                    return "فاکتور مورد نظر یافت نشد";
                }
            }
            catch (Exception e)
            {

                return "حذف فاکتور با مشکلی مواجه شد:\n" + e.Message;
            }

        }
        public string Update(Product p, int id)
        {
            try
            {
                var q = db.Products.Where(i => i.id == id).FirstOrDefault();
                if (q != null)
                {
                    q.nameP = p.nameP;
                    p.Code = p.Code;
                    q.IsActive = p.IsActive;
                    db.SaveChanges();
                    return "ویرایش اطلاعات با موفقیت انجام شد";
                }
                else
                {
                    return "کاربر مورد نظر یافت نشد";
                }
            }
            catch (Exception e)
            {
                return "ویرایش اطلاعات با مشکلی مواجه شد\n" + e.Message;

            }

        }

    }
}
