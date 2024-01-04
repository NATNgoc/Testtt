using DTO;
using GUI.DAL.IDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class LoginDAL :ILoginDAL
    {
        public LoginDAL() { }
        private static LoginDAL instance = null;
        public static LoginDAL Instance
        {


            get { if (instance == null)
                    instance = new LoginDAL();
                return instance; }
            private set
            {
                // Stryker disable once all
                instance = value;
            }
        }
        public string maHoaMatKhau(string password)
        {
            byte[] temp = ASCIIEncoding.ASCII.GetBytes(password);
            byte[] hashData = new MD5CryptoServiceProvider().ComputeHash(temp);
            // Stryker disable once all
            string hasPass = "";
            foreach (byte b in hashData)
            {
                // Stryker disable once all
                hasPass += b;
            }
            return hasPass;
        }
      
           public   bool Login(string username,  string password, ref Account a)
        {
            #region ma hoa mat khau
            // Stryker disable once all
            string hasPass =maHoaMatKhau(password);

            #endregion
            DataTable t = new DataTable();
          
            // Stryker disable once all
            Sql_Connection.Instance.openCon();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            // Stryker disable once all
            cmd.CommandText = "select * from account  where userName = @user and passWord = @pass";
            // Stryker disable once all
            SqlParameter paruser = new SqlParameter("@user", SqlDbType.VarChar);
            // Stryker disable once all
            SqlParameter parpassword = new SqlParameter("@pass", SqlDbType.VarChar);
            paruser.Value = username;
            
            parpassword.Value = hasPass;
            //
            password = hasPass;
            // Stryker disable once all
            cmd.Parameters.Add(paruser);
            // Stryker disable once all
            cmd.Parameters.Add(parpassword);

           t = DataProvider.Instance.excecuteQueryWithParameter_DataTable(cmd); 

            if (t.Rows.Count == 0) {
                // Stryker disable once all
                return false;
                

            }
            DataRow dr = t.Rows[0];
            // Stryker disable once all
            if (dr["userName"].ToString().Trim() == username && dr["passWord"].ToString().Trim() == password) {
                a.ID = (int)dr["id"];
                a.UserName = dr["userName"].ToString();
                a.DisplayName = dr["displayName"].ToString();
                a.TypeAccount = (int)dr["typeAccount"];


                return true;
            }
            // Stryker disable once all
            return false;


        }
        public  bool ktCurPass(string username, string password)
        {
            string hasPass = maHoaMatKhau(password);

            DataTable t = new DataTable();
           
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            // Stryker disable once all
            cmd.CommandText = "select * from account  where userName = @user and passWord = @pass";
            // Stryker disable once all
            SqlParameter paruser = new SqlParameter("@user", SqlDbType.VarChar);
            // Stryker disable once all
            SqlParameter parpassword = new SqlParameter("@pass", SqlDbType.VarChar);
            // Stryker disable once all
            paruser.Value = username;
            // Stryker disable once all
            parpassword.Value = hasPass;
            // Stryker disable once all
            password = hasPass;

            // Stryker disable once all
            cmd.Parameters.Add(paruser);
            // Stryker disable once all
            cmd.Parameters.Add(parpassword);

            t = DataProvider.Instance.excecuteQueryWithParameter_DataTable(cmd);

            if (t.Rows.Count == 0)
            {

                return false;


            }
            return true;

        }
        public   DataTable hienThiDanhSachTaiKhoan()
        {
            DataTable dt = new DataTable();
            // Stryker disable once all
            string q = "select id [Mã tài khoản], displayName [Tên hiển thị], userName [Username] ,typeAccount [Loại tài khoản] from account";
            dt = DataProvider.Instance.excecuteQuerry(q);
            return dt;


        }
        public  List <string> danhSachUserName()
        {
            // Stryker disable once all
            string q = "select * from account";
        DataTable ds = DataProvider.Instance.excecuteQuerry(q);

        List<string> l = new List<string>();



            foreach (DataRow dr in ds.Rows)
            {
                // Stryker disable once all
                l.Add(dr["username"].ToString().Trim());
            }



            return l;

        }

        public  bool themRow(Account d)
        {
            // Stryker disable once all
            string pass = maHoaMatKhau("0");
               

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                // Stryker disable once all
                cmd.CommandText = "insert into account values (@user,@display,@pass,@type)";

                // Stryker disable once all
                SqlParameter paruser = new SqlParameter("@user", SqlDbType.VarChar);
                // Stryker disable once all
                SqlParameter pardisplay = new SqlParameter("@display", SqlDbType.NVarChar);
                // Stryker disable once all
                SqlParameter parpass = new SqlParameter("@pass", SqlDbType.VarChar);
                // Stryker disable once all
                SqlParameter partype = new SqlParameter("@type", SqlDbType.Int);

                paruser.Value = d.UserName;
                pardisplay.Value = d.DisplayName;
                parpass.Value = pass;
                partype.Value = d.TypeAccount;
                // Stryker disable once all
                cmd.Parameters.Add(paruser);
                // Stryker disable once all
                cmd.Parameters.Add(pardisplay);
                // Stryker disable once all
                cmd.Parameters.Add(parpass);
                // Stryker disable once all
                cmd.Parameters.Add(partype);


            return DataProvider.Instance.excecuteQueryWithParameter(cmd);


        }
        public  bool xoaRow(int ma)
        {
           
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                // Stryker disable once all
                cmd.CommandText = "delete from account where id = " + ma;
                return DataProvider.Instance.excecuteQueryWithParameter(cmd);




        }
        public   string timUserNameByIDAccount(int id)
        {
            DataTable dt = new DataTable();
            // Stryker disable once all
            dt = DataProvider.Instance.excecuteQuerry("select * from account where id = " + id);
            DataRow dataRow = dt.Rows[0];
            return dataRow["username"].ToString().Trim();

        }
        public  bool chinhSuaRow(Account d)
        {
        
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
            // Stryker disable once all
            cmd.CommandText = "update account \r\nset \r\n\tusername = @name,\r\n\tdisplayname = @dis ,\r\n\ttypeAccount = @type \r\nwhere id = @id";
                cmd.Connection = Sql_Connection.Instance.sqlCon;

            // Stryker disable once all
            SqlParameter parid = new SqlParameter("@id", SqlDbType.Int);
            // Stryker disable once all
            SqlParameter parUsername = new SqlParameter("@name", SqlDbType.VarChar);
            // Stryker disable once all
            SqlParameter parDisplay = new SqlParameter("@dis", SqlDbType.NVarChar);
            // Stryker disable once all
            SqlParameter parType = new SqlParameter("@type", SqlDbType.Int);

                parid.Value = d.ID;
                parUsername.Value = d.UserName;
                parDisplay.Value = d.DisplayName;
                parType.Value = d.TypeAccount;
            // Stryker disable once all
            cmd.Parameters.Add(parid);
            // Stryker disable once all
            cmd.Parameters.Add(parUsername);
            // Stryker disable once all
            cmd.Parameters.Add(parDisplay);
            // Stryker disable once all
            cmd.Parameters.Add(parType);


               return DataProvider.Instance.excecuteQueryWithParameter(cmd);



        }
        public   DataTable timKiemTaiKhoan(string displayName)
        {
            DataTable dataTable = new DataTable();
           
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            // Stryker disable once all
            cmd.CommandText = "select id [Mã tài khoản], displayName [Tên hiển thị], userName [Username] ,typeAccount [Loại tài khoản] from account where displayName like '%" + displayName + "%'";

            //SqlParameter p = new SqlParameter("@n", SqlDbType.NVarChar);
            //p.Value = n;
            //cmd.Parameters.Add(p);

            return DataProvider.Instance.excecuteQueryWithParameter_DataTable(cmd);

        }
        public  bool DoiPassword(string username, string password)
        {
            string hasPass = maHoaMatKhau(password);
            
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                // Stryker disable once all
                cmd.CommandText = "update account \r\nset \r\n\tpassword = @pass\r\nwhere username = @username";

                // Stryker disable once all
                SqlParameter parpass = new SqlParameter("@pass", SqlDbType.VarChar);
                // Stryker disable once all
                SqlParameter parUsername = new SqlParameter("@username", SqlDbType.VarChar);


                parpass.Value = hasPass;
                parUsername.Value = username;
                // Stryker disable once all
                cmd.Parameters.Add(parUsername);
                // Stryker disable once all
                cmd.Parameters.Add(parpass);

                return DataProvider.Instance.excecuteQueryWithParameter(cmd);







        }

    }
}
