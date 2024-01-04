using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DTO;
using GUI.DAL.IDAL;

namespace DAL
{
    public  class FoodDAL :IFoodDAL
    {
        public FoodDAL() { }   
        private static FoodDAL instance= null;
        public static FoodDAL Instance
        {  get { if (instance == null) instance = new FoodDAL(); return instance; }
            // Stryker disable once all
            set { instance = value; } 
        }   

        public  List <FoodDTO> getFoodByCateGory (int ma)
        {
            List <FoodDTO> l = new List <FoodDTO>();
            DataTable dataTable = new DataTable();
            // Stryker disable once all
            string q = "select * from food where idCategory = " + ma;
            dataTable =  DataProvider.Instance.excecuteQuerry(q); ;
            foreach (DataRow row in dataTable.Rows)
            {
                DataRow dr = row;
                FoodDTO f = new FoodDTO(row);
                l.Add(f);

            }

            return l;

        }
        public  DataTable hienThiDanhSachFood()
        {
            // Stryker disable once all
            string q = "select f.id [Mã món ăn], f.name [Tên món ăn], f.price [Giá], c.name [Loại] from food f\r\ninner join FOOD_CATEGORY c\r\non c.id = f.idCategory";
            return DataProvider.Instance.excecuteQuerry(q);



        }
        public  bool themRow(FoodDTO a)
        {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                // Stryker disable once all
                cmd.CommandText = "insert into food values ( @name, @price, @loai)";
                // Stryker disable once all
                SqlParameter parname = new SqlParameter("@name", SqlDbType.NVarChar);
                // Stryker disable once all
                SqlParameter parprice = new SqlParameter("@price", SqlDbType.Float);
                // Stryker disable once all
                SqlParameter parLoai = new SqlParameter("@loai", SqlDbType.Int);
                parname.Value = a.TenMon;
                parprice.Value =(float) a.Gia;
                parLoai.Value = a.Loai;
                // Stryker disable once all
                cmd.Parameters.Add(parname);
                // Stryker disable once all
                cmd.Parameters.Add(parprice);
                // Stryker disable once all
                cmd.Parameters.Add(parLoai);

            return DataProvider.Instance.excecuteQueryWithParameter(cmd);

        }
       public  List <string> danhSachFood()
        {
            // Stryker disable once all
            string q = "select * from food";
            DataTable ds = DataProvider.Instance.excecuteQuerry(q);

            List<string> l = new List<string>();



            foreach (DataRow dr in ds.Rows)
            {
                l.Add(dr["name"].ToString().Trim());
            }

            return l;


        }
        public  bool chinhSuaRow(FoodDTO a)
        {
           
                
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                // Stryker disable once all
                cmd.CommandText = "update Food\r\nset \r\n\tname = @name,\r\n\tprice = @price ,\r\n\tidCategory = @idLoai \r\nwhere id = @id";

                // Stryker disable once all
                SqlParameter parid = new SqlParameter("@id", SqlDbType.Int);
                // Stryker disable once all
                SqlParameter parname = new SqlParameter("@name", SqlDbType.NVarChar);
                // Stryker disable once all
                SqlParameter parprice  = new SqlParameter("@price", SqlDbType.Float);
                // Stryker disable once all
                SqlParameter parLoai =  new SqlParameter("@idLoai", SqlDbType.Int);
                parid.Value = a.Id;
                parname.Value = a.TenMon;
                parprice.Value =(float) a.Gia;
                parLoai.Value = a.Loai;
                // Stryker disable once all
                cmd.Parameters.Add(parid);
                // Stryker disable once all
                cmd.Parameters.Add(parname);
                // Stryker disable once all
                cmd.Parameters.Add(parprice);
                // Stryker disable once all
                cmd.Parameters.Add(parLoai);

            return DataProvider.Instance.excecuteQueryWithParameter(cmd);
                


        }

        public  string timNameFoodByIDFood (int idFood) {
            DataTable dt = new DataTable();
            // Stryker disable once all
            dt =  DataProvider.Instance.excecuteQuerry("select * from Food where id = "+ idFood);
            DataRow dataRow = dt.Rows[0];
            return dataRow["name"].ToString().Trim();   
        
        }
        public  DataTable TimKiemMonAn(string n)
        {
            DataTable dataTable = new DataTable();
   
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            // Stryker disable once all
            cmd.CommandText = "TimKiemMonAn";
            // Stryker disable once all
            SqlParameter p = new SqlParameter("@name", SqlDbType.NVarChar);
            p.Value = n;
            // Stryker disable once all
            cmd.Parameters.Add(p);
            return DataProvider.Instance.excecuteQueryWithParameter_DataTable(cmd);


        }
        public  bool xoaMonAn( int id)
        {
             
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                // Stryker disable once all
                cmd.CommandText = "delete from food where id = " + id;
                return DataProvider.Instance.excecuteQueryWithParameter(cmd);




        }
    }
}
