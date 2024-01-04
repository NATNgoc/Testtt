using GUI.DAL.IDAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public  class MenuDAL :IMenuDAL
    {
        private static MenuDAL  instance = null;
        public static MenuDAL Instance
        { get { if (instance == null) instance = new MenuDAL(); return instance; }
            // Stryker disable once all
            private set { instance = value; }
        }   
        public MenuDAL() { }

        public  DataTable hienThiMenu (int idT, ref int idBill)
        {
            // lay ma bill
            int maBill ;

            int m = idT;


            DataTable t = new DataTable();
            SqlCommand cmd1 = new SqlCommand();
            cmd1.CommandType = CommandType.StoredProcedure;
            // Stryker disable once all
            cmd1.CommandText = "getUncheckBillByTable";
            cmd1.Connection = Sql_Connection.Instance.sqlCon;
            // Stryker disable once all
            SqlParameter p = new SqlParameter("@ma", SqlDbType.Int);
            p.Value = idT;
            // Stryker disable once all
            cmd1.Parameters.Add(p);
            t = DataProvider.Instance.excecuteQueryWithParameter_DataTable(cmd1);


            // kiểm tra liệu có null 

            if (t.Rows.Count > 0)
            {
                DataRow dr = t.Rows[0];
                maBill = (int)dr["id"];
                
            }
            // Stryker disable once all
            maBill = - 1;
            // lay ma bill

            DataTable table = new DataTable(); //mở từ lúc được tạo 
          
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            // Stryker disable once all

            cmd.CommandText = "HienThiMenu";
            // Stryker disable once all
            SqlParameter pa = new SqlParameter("@ma", SqlDbType.Int);
            pa.Value = maBill;
            // Stryker disable once all

            cmd.Parameters.Add(pa); 



            idBill = maBill;
            return DataProvider.Instance.excecuteQueryWithParameter_DataTable(cmd);
        }

        public  DataTable hienThiMenuByIDBill(int maBill)
        {
          

            DataTable table = new DataTable(); //mở từ lúc được tạo 
            
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            // Stryker disable once all
            cmd.CommandText = "HienThiMenu";
            // Stryker disable once all
            SqlParameter pa = new SqlParameter("@ma", SqlDbType.Int);
            pa.Value = maBill;
            // Stryker disable once all
            cmd.Parameters.Add(pa);
            return DataProvider.Instance.excecuteQueryWithParameter_DataTable(cmd);


        }






    }
}
