using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL 
{
    [ExcludeFromCodeCoverage]
    // Stryker disable all
    public class DataProvider 
    {
        protected DataProvider() { }
        private static DataProvider instance = null;
        public  static DataProvider Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataProvider();
                }
                return instance;
            }
            private set
            {
                DataProvider.instance = value;
            }
        }
        public virtual DataTable excecuteQuerry(string q)
        {
            try
            {
                Sql_Connection.Instance.connect();
                Sql_Connection.Instance.openCon();

                SqlCommand sqlCommand = new SqlCommand(q, Sql_Connection.Instance.sqlCon);
                DataTable data = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                adapter.Fill(data);


                return data;
            }
            catch
            {
                throw new Exception();
            }

        }
      
        public virtual bool excecuteQueryWithParameter(SqlCommand cmd)
        {
            Sql_Connection.Instance.connect();
            Sql_Connection.Instance.openCon();
            cmd.Connection = Sql_Connection.Instance.sqlCon;

            try
            {
                if (cmd.ExecuteNonQuery() < 0)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }

        }
        public virtual DataTable excecuteQueryWithParameter_DataTable(SqlCommand cmd)
        {
            DataTable t = new DataTable();

            try
            {

                Sql_Connection.Instance.connect();
                Sql_Connection.Instance.openCon();
                cmd.Connection = Sql_Connection.Instance.sqlCon;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(t);
                return t;
            }
            catch {
                return t;
               }

        }
        public virtual int ExecuteScalar(SqlCommand cmd)
        {
            Sql_Connection.Instance.connect();
            Sql_Connection.Instance.openCon();
            cmd.Connection = Sql_Connection.Instance.sqlCon;

            int kq = (int)cmd.ExecuteScalar();
            return kq;

        }
    }
}
