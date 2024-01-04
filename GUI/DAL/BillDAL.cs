using GUI.DAL.IDAL;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace DAL
{
    public  class BillDAL : IBillDAL
    {
        private static BillDAL instance = null;
        public BillDAL() { }

        public static BillDAL Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BillDAL();
                }
                return instance;
            }
            private set
            {
                // Stryker disable once all
                BillDAL.instance = value;
            }
        }


        // tìm mã bill chưa thanh toán khi biết bàn 

        public   int  getUncheckBillByTable (int id)
        {
            int m = id;
            int ma;
            

            DataTable t = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            // Stryker disable once all
            cmd.CommandText = "getUncheckBillByTable";
            cmd.Connection = Sql_Connection.Instance.sqlCon;
            // Stryker disable once all
            SqlParameter p = new SqlParameter("@ma", SqlDbType.Int);
            p.Value = id;
            // Stryker disable once all
            cmd.Parameters.Add(p);
            t = DataProvider.Instance.excecuteQueryWithParameter_DataTable(cmd);


            // kiểm tra liệu có null 


            if (t.Rows.Count > 0)
            {
                DataRow dr = t.Rows[0];
                ma = (int)dr["id"];
                return ma;
            }

            return -1;
        }
       public  bool  thucHienCheckOut(int maBan)
        {
            // thực hiện getUncheckBillByTable
            int m = maBan;
            int maBill;


            DataTable t = new DataTable();
            SqlCommand cmd1 = new SqlCommand();
            cmd1.CommandType = CommandType.StoredProcedure;
            // Stryker disable once all
            cmd1.CommandText = "getUncheckBillByTable";
            cmd1.Connection = Sql_Connection.Instance.sqlCon;
            // Stryker disable once all
            SqlParameter p = new SqlParameter("@ma", SqlDbType.Int);
            p.Value = maBan;
            // Stryker disable once all
            cmd1.Parameters.Add(p);
            t = DataProvider.Instance.excecuteQueryWithParameter_DataTable(cmd1);


            // kiểm tra liệu có null 


            if (t.Rows.Count > 0)
            {
                DataRow dr = t.Rows[0];
                // Stryker disable once all
                maBill = (int)dr["id"];
                
            }
            else
                maBill =  -1;

            // end thwujc hiện check out 
            if (maBill ==-1) { return false; }
            // Stryker disable once all
            Sql_Connection.Instance.connect();
            // Stryker disable once all
            Sql_Connection.Instance.openCon();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            // Stryker disable once all
            cmd.CommandText = "thucHienCheckOut";
            // Stryker disable once all
            SqlParameter par = new SqlParameter("@maBill", SqlDbType.Int);
            par.Value = maBill;
            // Stryker disable once all
            cmd.Parameters.Add(par);

            return DataProvider.Instance.excecuteQueryWithParameter(cmd);


        }
       public  bool themBill(int maBan)
        {
          


                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                // Stryker disable once all
                cmd.CommandText = "themBill";
            // Stryker disable once all
                SqlParameter pa = new SqlParameter("@idTable", SqlDbType.Int);
                pa.Value = maBan;
                // Stryker disable once all
                cmd.Parameters.Add(pa);

                return DataProvider.Instance.excecuteQueryWithParameter(cmd);



        }
        public  bool ChuyenBan(int maBill, int maBanNew )
        {
           

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            // Stryker disable once all
            cmd.CommandText = "chuyenBan";
            // Stryker disable once all
            SqlParameter paBill = new SqlParameter("@idBill", SqlDbType.Int);
            // Stryker disable once all
            SqlParameter paTable = new SqlParameter("@idNew", SqlDbType.Int);
            paTable.Value = maBanNew;
            paBill.Value = maBill;
            // Stryker disable once all
            cmd.Parameters.Add(paBill);
            // Stryker disable once all
            cmd.Parameters.Add(paTable);


            return DataProvider.Instance.excecuteQueryWithParameter(cmd);



        }

        public  DataTable HienThiDoanhThu(int page, DateTime dateStart, DateTime dateEnd)
        {
            DataTable dataTable = new DataTable();
         
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            // Stryker disable once all
            cmd.CommandText = "HienThiDoanhThuPhanTrang";
            // Stryker disable once all
            SqlParameter parD1 = new SqlParameter("@dateStart", SqlDbType.VarChar);
            // Stryker disable once all
            SqlParameter parD2 = new SqlParameter("@dateEnd", SqlDbType.VarChar);
            // Stryker disable once all
            SqlParameter parD3 = new SqlParameter("@page_num", SqlDbType.Int);
            // Stryker disable once all
            parD1.Value = dateStart.ToString("yyyy-MM-dd");
            // Stryker disable once all
            parD2.Value = dateEnd.ToString("yyyy-MM-dd")+" 23:59:59";
            parD3.Value = page;
            // Stryker disable once all
            cmd.Parameters.Add(parD1);
            // Stryker disable once all
            cmd.Parameters.Add(parD2);
            // Stryker disable once all
            cmd.Parameters.Add(parD3);

            return DataProvider.Instance.excecuteQueryWithParameter_DataTable(cmd);


        }

        public  int  hienThiTongDanhThu(DateTime dateStart,DateTime dateEnd)
        {
            DataTable dataTable = new DataTable();
            // Stryker disable once all

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            // Stryker disable once all
            cmd.CommandText = "HienThiTongDoanhThu";
            // Stryker disable once all
            SqlParameter parD1 = new SqlParameter("@dateStart", SqlDbType.Date);
            // Stryker disable once all
            SqlParameter parD2 = new SqlParameter("@dateEnd", SqlDbType.Date);

            parD1.Value = dateStart;
            parD2.Value = dateEnd;
            // Stryker disable once all
            cmd.Parameters.Add(parD1);
            // Stryker disable once all
            cmd.Parameters.Add(parD2);

            DataTable d = new DataTable();
            d = DataProvider.Instance.excecuteQueryWithParameter_DataTable(cmd);

            DataRow dr = d.Rows[0];
            if ( dr.IsNull("Tổng"))
            {
                return 0;
            }

            return int.Parse(dr["Tổng"].ToString());



        }




        public  bool capNhatDiscount(int maBill,decimal  discount)
        {
            // Stryker disable once all

            
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            // Stryker disable once all
            cmd.CommandText = "capNhatDiscount";
            // Stryker disable once all
            SqlParameter parid = new SqlParameter("@id", SqlDbType.Int);
            // Stryker disable once all
            SqlParameter parDis = new SqlParameter("@discount", SqlDbType.Int);
            parid.Value = maBill;
            parDis.Value =(int) discount;
            // Stryker disable once all
            cmd.Parameters.Add(parid);
             // Stryker disable once all
            cmd.Parameters.Add(parDis);
            return DataProvider.Instance.excecuteQueryWithParameter(cmd);


        }
        public  int getSizeOfBill(DateTime dateStart, DateTime dateEnd)
        {
           
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            // Stryker disable once all
            String dateStartString = dateStart.ToString("yyyy-MM-dd");
            // Stryker disable once all
            String dateEndString = dateEnd.ToString("yyyy-MM-dd")+" 23:59:59";
            //string query = string.Format("select count(id) from bill where dateCheckIn between {0} and {1}", dateStart, dateEnd);
            // Stryker disable once all
            cmd.CommandText = "select count(id) from bill where (dateCheckIn between '" + dateStartString  + "' and '" + dateEndString+"') and status = 1";
            
           return DataProvider.Instance.ExecuteScalar(cmd);

        }
        public  int getDiscount (int maBill)
        {
          
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            // Stryker disable once all
            cmd.CommandText = "select discount from bill where id = " + maBill;

            return DataProvider.Instance.ExecuteScalar(cmd);    


        }
    
        public  bool xoaBill_Infor(int maBill)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            // Stryker disable once all
            cmd.CommandText = "delete from Bill_infor where idBill = " + maBill;
            return DataProvider.Instance.excecuteQueryWithParameter(cmd);

        }

    }
}
