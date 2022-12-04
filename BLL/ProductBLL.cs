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
    public class ProductBLL
    {
        ProductDAL dal = new ProductDAL();
        public string Create(Product p)
        {
            return dal.Create(p);
        }
        public bool Read(Product p)
        {
            return dal.Read(p);
        }
        public List<ProductViewModel> ReadViewModel()
        {
            return dal.ReadViewModel();
        }
        public Product Read(int id)
        {
            return dal.Read(id);
        }
        public DataTable Read()
        {
            return dal.Read();
        }
        public DataTable Read(string s, int index)
        {
            return dal.Read(s,index);
        }
        public string Delete(int id)
        {
            return dal.Delete(id);
        }
        public string Update(Product p, int id)
        {
            return dal.Update(p, id);
        }
    }
}
