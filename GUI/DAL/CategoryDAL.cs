using DTO;
using GUI.DAL.IDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL
{
    public  class CategoryDAL :ICategoryDAL
    {
        public CategoryDAL() { }
        private static CategoryDAL instance = null;
        public static CategoryDAL Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CategoryDAL();
                }
                return instance;
            }
            private set
            {
                // Stryker disable once all
                CategoryDAL.instance = value;
            }
        }
        public  DataTable hienThiDanhSachFoodCategory()
        {
            // Stryker disable once all
            string s = "select id [Mã danh mục], name [Tên danh mục] from food_category";
            return DataProvider.Instance.excecuteQuerry(s);
        }
        public  List<string> danhSachCategory()
        {
            // Stryker disable once all
            string q = "select * from food_category";
            DataTable ds = DataProvider.Instance.excecuteQuerry(q);

            List<string> l = new List<string>();



            foreach (DataRow dr in ds.Rows)
            {
                l.Add(dr["name"].ToString().Trim());
            }



            return l;

        }

       public  bool themRow(string name)
        {
            

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                // Stryker disable once all
                cmd.CommandText = "insert into food_category values ( @name)";
            // Stryker disable once all
            SqlParameter parname = new SqlParameter("@name", SqlDbType.NVarChar);
                parname.Value = name;
                // Stryker disable once all
                cmd.Parameters.Add(parname);
                return DataProvider.Instance.excecuteQueryWithParameter(cmd);


              
           

        }
       public string timNameCateGory_FoodByID(int id)
        {
            DataTable dt = new DataTable();
            // Stryker disable once all
            dt = DataProvider.Instance.excecuteQuerry("select * from food_category where id = " + id);
            DataRow dataRow = dt.Rows[0];
            return dataRow["name"].ToString().Trim();


        }
        public  bool chinhSuaRow(CategoryDTO a)
        {
       

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                // Stryker disable once all
                cmd.CommandText = "UPDATE food_category " +
                  // Stryker disable once all
                  "SET name = @name " +
                  // Stryker disable once all

                  "WHERE id = @id";
            // Stryker disable once all
            SqlParameter parid = new SqlParameter("@id", SqlDbType.Int);
            // Stryker disable once all
            SqlParameter parname = new SqlParameter("@name", SqlDbType.NVarChar);

                parid.Value = a.Id;
                parname.Value = a.Name;
                // Stryker disable once all
                cmd.Parameters.Add(parid);
                // Stryker disable once all
                cmd.Parameters.Add(parname);

            return DataProvider.Instance.excecuteQueryWithParameter(cmd);

                
            

        }
        public  DataTable TimKiemLoaiMonAn(string n)
        {
            DataTable dataTable = new DataTable();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            // Stryker disable once all
            cmd.CommandText = "TimKiemLoaiMonAn";
            // Stryker disable once all
            SqlParameter p = new SqlParameter("@name", SqlDbType.NVarChar);
            p.Value = n;
            // Stryker disable once all

            cmd.Parameters.Add(p);

           return DataProvider.Instance.excecuteQueryWithParameter_DataTable(cmd);




        }
        public   bool xoaLoaiMonAn(int id)
        {
         
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                // Stryker disable once all
                cmd.CommandText = "delete from food_category where id = " + id;
                 return DataProvider.Instance.excecuteQueryWithParameter(cmd);


          



        }
    }
}
