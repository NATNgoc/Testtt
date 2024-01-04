using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using GUI.DAL.IDAL;

namespace DAL
{
    public  class Bill_InforDAL :IBill_InforDAL
    {
        public Bill_InforDAL() { }
        private static Bill_InforDAL instance= null;
        public static Bill_InforDAL Instance
        {
            get { if (instance == null) instance = new Bill_InforDAL(); return instance; }

            set {
                // Stryker disable once all
                instance = value;
            }   
        }
        public   bool  themMonAnChoBill(int maBill, int maFood,int  soLuong)
        {
          
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            // Stryker disable once all
            cmd.CommandText = "AddBill_Infor";

            // Stryker disable once all
            SqlParameter parIdB = new SqlParameter("@maBill", SqlDbType.Int);
            // Stryker disable once all
            SqlParameter parIdF = new SqlParameter("@maFood", SqlDbType.Int);
            // Stryker disable once all
            SqlParameter parCount = new SqlParameter("@count", SqlDbType.Int);
            parIdB.Value = maBill;
            parIdF.Value = maFood;
            parCount.Value = soLuong;
            // Stryker disable once all
            cmd.Parameters.Add(parIdB);
                 // Stryker disable once all
            cmd.Parameters.Add(parIdF);
            // Stryker disable once all
            cmd.Parameters.Add(parCount);

            return DataProvider.Instance.excecuteQueryWithParameter(cmd);



        }

       public  DataTable getBill_Infor(int maBill)
        {
            DataTable dt = new DataTable();
            // Stryker disable once all
            string q = "select * from bill_infor where idBill =  " + maBill;
            dt = DataProvider.Instance.excecuteQuerry(q);

            return dt;

        }
        public  bool capNhapSoLuong (int maBill, int maMon, int soLuong)
        {
          
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            // Stryker disable once all
            cmd.CommandText = "updateCount";
            // Stryker disable once all
            SqlParameter parMaBill = new SqlParameter("@maBill", SqlDbType.Int);
            parMaBill.Value = maBill;
            // Stryker disable once all
            SqlParameter parmaMon  = new SqlParameter("@maMon", SqlDbType.Int);
            parmaMon.Value = maMon;
            // Stryker disable once all
            SqlParameter parSL = new SqlParameter("@soLuong", SqlDbType.Int);
            parSL.Value = soLuong;
            // Stryker disable once all
            cmd.Parameters.Add(parMaBill);
            // Stryker disable once all
            cmd.Parameters.Add(parmaMon);
            // Stryker disable once all
            cmd.Parameters.Add(parSL);

            return DataProvider.Instance.excecuteQueryWithParameter(cmd);



        }


    }

}
