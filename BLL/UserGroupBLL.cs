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
    public class UserGroupBLL
    {
        UserGroupDAL DAL = new UserGroupDAL();
        public string Create(UserGroup ug)
        {
            return DAL.Create(ug);
        }
        public bool Read(string Name)
        {
            return DAL.Read(Name);
        }
        public List<string> ReadUGNames()
        {
            return DAL.ReadUGNames();
        }
        public UserGroup ReadN(string n)
        {
            return DAL.ReadN(n);
        }
        public DataTable Read()
        {
            return DAL.Read();
        }
    }

}
