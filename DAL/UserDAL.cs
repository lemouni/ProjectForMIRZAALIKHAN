using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;


namespace DAL
{
    public class UserDAL
    {
        DB db = new DB();
        public string Create(User u, UserGroup ug)
        {
            try
            {
                if (Read(u))
                {
                    ///
                    //u.UserGroup = db.UserGroups.Find(ug.id);
                    u.UserGroup = db.UserGroups.Where(i => i.Title == ug.Title).SingleOrDefault();

                    ///
                    db.Users.Add(u);
                    db.SaveChanges();
                    return "ثبت اطلاعات با موفقیت انجام شد";
                }
                else
                {
                    return "نام کاربری وارد شده تکراری می باشد";
                }
            }
            catch (Exception e)
            {

                return "ثبت اطلاعات با مشکلی مواجه شد\n" + e.Message;
            }

        }
        public bool IsRegistered()
        {
            return db.Users.Count() > 0;
        }
        public bool Read(User u)
        {
            var q = db.Users.Where(i => i.name == u.name);
            if (q.Count() == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public DataTable Read()
        {
            string cmd = @"SELECT dbo.[User].id, dbo.[User].name AS [نام کاربری], dbo.[User].family AS فامیلی, dbo.UserGroups.Title AS [سطح کاربری] FROM dbo.[User] INNER JOIN dbo.UserGroups ON dbo.[User].UserGroup_id = dbo.UserGroups.id";
            SqlConnection con = new SqlConnection("data source=.;initial catalog=x;integrated security=True");
            var sqladapter = new SqlDataAdapter(cmd, con);
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];
        }
        public User Read(int id)
        {
            return db.Users.Where(i => i.id == id).FirstOrDefault();

        }
        public User ReadU(string s)
        {
            return db.Users.Where(i => i.name == s).SingleOrDefault();
        }
        public List<string> ReadUserNames()
        {
            return db.Users.Select(i => i.name).ToList();
        }
        public string Update(User u, UserGroup ug, int id)
        {
            try
            {
                var q = db.Users.Where(i => i.id == id).FirstOrDefault();
                if (q != null)
                {
                    q.name = u.name;
                    q.family = u.family;
                    q.UserGroup = db.UserGroups.Find(ug.id);
                    //q.Pic = u.Pic;
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
        public string Delete(int id)
        {
            var q = db.Users.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    db.Users.Remove(q);
                    db.SaveChanges();
                    return "حذف اطلاعات با موفقیت انجام شد";
                }
                else
                {
                    return "کاربر مورد نظر یافت نشد!";
                }
            }
            catch (Exception e)
            {

                return "حذف اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }
        }
        public User Login(string u, string p)
        {
            return db.Users.Include("UserGroup").Where(i => i.name == u && i.family == p).SingleOrDefault();
        }
        public bool Access(User u, string s, int a)
        {
            UserGroup ug = db.UserGroups.Include("UserAccessRoles").Where(i => i.id == u.UserGroup.id).FirstOrDefault();
            UserAccessRole uar = ug.UserAccessRoles.Where(z => z.Section == s).FirstOrDefault();


            if (a == 1)
            {
                return uar.CanEnter;
            }
            else if (a == 2)
            {
                return uar.CanCreate;
            }
            else if (a == 3)
            {
                return uar.CanUpdate;
            }
            else
            {
                return uar.CanDelete;
            }
        }
        public List<string> ReadNmaes()
        {
            return db.Users.Select(i => i.name).ToList();
        }
    }
}

