using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DTO;
using GUI.DAL.IDAL;


namespace DAL
{
    public class TableDAL :ITableDAL
    {


        public TableDAL() { }
        private static TableDAL instance = null;
        public static TableDAL Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TableDAL();
                }
                return instance;
            }
            private set
            {
                // Stryker disable once all
                TableDAL.instance = value; }
        }

        public  List<int> danhSachIDTable()
        {
            // Stryker disable once all
            string q = "select * from table_food";
            DataTable ds = DataProvider.Instance.excecuteQuerry(q);

            List<int> l = new List<int>();

            foreach (DataRow dr in ds.Rows)
            {
                // Stryker disable once all

                l.Add(int.Parse(dr["id"].ToString()));
            }



            return l;

        }
        public   List<string> danhSachBanAn ()
        {
            // Stryker disable once all
            string q = "select * from table_food";
            DataTable ds =DataProvider.Instance.excecuteQuerry(q);

            List<string> l = new List<string>();

            foreach (DataRow dr in ds.Rows)
            {
                // Stryker disable once all

                l.Add(dr["name"].ToString().Trim());
            }



            return l;

        }
        public  bool themRow(TableDTO a)
        {
           

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
            // Stryker disable once all
            cmd.CommandText = "insert into table_food values ( @name, @status)";
            // Stryker disable once all
            SqlParameter parname = new SqlParameter("@name", SqlDbType.NVarChar);
            // Stryker disable once all
            SqlParameter pars = new SqlParameter("@status", SqlDbType.NVarChar);
                parname.Value = a.Name;
                pars.Value = a.Status;
            // Stryker disable once all
            cmd.Parameters.Add(parname);
            // Stryker disable once all
            cmd.Parameters.Add(pars);

                 return DataProvider.Instance.excecuteQueryWithParameter(cmd);


        }
        public  bool xoaRow(int ma)
        {
          
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
            // Stryker disable once all
            cmd.CommandText = "delete from Table_food where id = " + ma;
                cmd.Connection = Sql_Connection.Instance.sqlCon;


               return DataProvider.Instance.excecuteQueryWithParameter(cmd);





        }
        public  bool chinhSuaRow (TableDTO a)
        {
          
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
            // Stryker disable once all
            cmd.CommandText = "update Table_Food\r\nset \r\n\tname = @name,\r\n\tstatus = @status\r\nwhere id = @id";
                cmd.Connection = Sql_Connection.Instance.sqlCon;

            // Stryker disable once all
            SqlParameter parid = new SqlParameter("@id", SqlDbType.Int);
            // Stryker disable once all
            SqlParameter parname = new SqlParameter("@name", SqlDbType.NVarChar);
            // Stryker disable once all
            SqlParameter pars = new SqlParameter("@status", SqlDbType.NVarChar);
                parid.Value = a.Id;
                parname.Value = a.Name;
                pars.Value = a.Status;
            // Stryker disable once all
            cmd.Parameters.Add(parid);
            // Stryker disable once all
            cmd.Parameters.Add(parname);
            // Stryker disable once all
            cmd.Parameters.Add(pars);



            return DataProvider.Instance.excecuteQueryWithParameter(cmd);


        }

        public  DataTable timKiemTable (string n)
        {
            DataTable dataTable= new DataTable();
           
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType= CommandType.Text;
            // Stryker disable once all
            cmd.CommandText = "select id as [Mã bàn ăn] , name as [Tên bàn ăn], status as [Trạng thái] from table_food where name like '%"+n+"%'";

            //SqlParameter p = new SqlParameter("@n", SqlDbType.NVarChar);
            //p.Value = n;
            //cmd.Parameters.Add(p);
            return DataProvider.Instance.excecuteQueryWithParameter_DataTable(cmd);

        }

        public  string timNameTableByIDTable(int idTable)
        {
            DataTable dt = new DataTable();
            // Stryker disable once all
            dt = DataProvider.Instance.excecuteQuerry("select * from Table_Food where id = " + idTable );
            DataRow dataRow = dt.Rows[0];
            return dataRow["name"].ToString().Trim();

        }
        public  DataTable HienThiTable()
        {
            DataTable dt = new DataTable();
            // Stryker disable once all
            dt = DataProvider.Instance.excecuteQuerry("select id as [Mã bàn ăn] ,name as [Tên bàn ăn] ,status as [Trạng thái]from table_food");
            return dt;

        }

       
    }
}
