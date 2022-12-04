using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BLL
{
    public class UserBLL
    {
        public string Create(User u, UserGroup ug)
        {
            UserDAL dal = new UserDAL();
            return dal.Create(u,ug);
        }
        public bool IsRegistered()
        {
            UserDAL dal = new UserDAL();
            return dal.IsRegistered();
        }
        public bool Read(User u)
        {
            UserDAL dal = new UserDAL();
            return dal.Read(u);    
        }
        public DataTable Read()
        {
            UserDAL dal = new UserDAL();
            return dal.Read();
        }
        public User Read(int id)
        {
            UserDAL dal = new UserDAL();
            return dal.Read(id);
        }
        public User ReadU(string s)
        {
            UserDAL dal = new UserDAL();
            return dal.ReadU(s);
        }
        public List<string> ReadUserNames()
        {
            UserDAL dal = new UserDAL();
            return dal.ReadUserNames();
        }
        public string Update(User u, UserGroup ug, int id)
        {
            UserDAL dal = new UserDAL();
            return dal.Update(u, ug, id);
        }
        public string Delete(int id)
        {
            UserDAL dal = new UserDAL();
            return dal.Delete(id);
        }
        public User Login(string u, string p)
        {
            UserDAL dal = new UserDAL();
            return dal.Login(u, p);
        }
        public bool Access(User u, string s, int a)
        {
            UserDAL dal = new UserDAL();
            return dal.Access(u, s, a);
        }
        public List<string> ReadNmaes()
        {
            UserDAL dal = new UserDAL();
            return dal.ReadNmaes();
        }

    }
}
