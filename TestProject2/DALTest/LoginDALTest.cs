using DAL;
using DTO;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestProject2.DALTest
{
    public class LoginDALTest
    {
        private Mock<DataProvider> _mockDataProvider;
        [SetUp]
        public void Init()
        {
            _mockDataProvider = new Mock<DataProvider>();
            mockSingleTon();
        }

        private void mockSingleTon()
        {
            var instanceField = typeof(DataProvider).GetField("instance", BindingFlags.Static | BindingFlags.NonPublic);
            instanceField.SetValue(null, _mockDataProvider.Object);
        }

        #region Login
        [Test]
        [TestCase("trinhxinhdep", "trinh2003", true)]
        [TestCase("trinhxinhdep", "trinh2002", false)]
        [TestCase("username2", "password2", false)]
        public void testLogin(string username, string password, bool expected)
        {
            DataTable dt = new DataTable(); 
            if(expected || password == "trinh2002")
            {
                dt.Columns.Add("id", typeof(int));
                dt.Columns.Add("userName", typeof(string));
                dt.Columns.Add("displayName", typeof(string));
                dt.Columns.Add("typeAccount", typeof(int));
                dt.Columns.Add("passWord", typeof(string));

                // Thêm dữ liệu vào DataTable
                DataRow row1 = dt.NewRow();
                row1["id"] = 1;
                row1["userName"] = "trinhxinhdep";
                row1["displayName"] = "Sinh tố";
                row1["typeAccount"] =1;
                row1["passWord"] = "1515115221938221101234415591195149231127154";

                dt.Rows.Add(row1);
            }
            _mockDataProvider.Setup(m => m.excecuteQueryWithParameter_DataTable(It.IsAny<SqlCommand>())).Returns(dt);

            // call action
            Account a = new Account();
            bool actual = LoginDAL.Instance.Login(username, password, ref a);

            //compare
            Assert.AreEqual(expected, actual);
        }
        #endregion

        #region KtCurPass
        [Test]
        [TestCase("trinhxinhdep", "trinh2002", true)]
        [TestCase("username2", "password2", false)]
        public void testKtCurPass(string username, string password, bool expected)
        {
            // setup method
            DataTable dt = new DataTable();
            if (expected)
            {
                dt.Columns.Add("id", typeof(int));
                dt.Columns.Add("userName", typeof(string));
                dt.Columns.Add("displayName", typeof(string));
                dt.Columns.Add("typeAccount", typeof(int));
                dt.Columns.Add("passWord", typeof(string));

                // Thêm dữ liệu vào DataTable
                DataRow row1 = dt.NewRow();
                row1["id"] = 1;
                row1["userName"] = "trinhxinhdep";
                row1["displayName"] = "Sinh tố";
                row1["typeAccount"] = 1;
                row1["passWord"] = "1515115221938221101234415591195149231127154";

                dt.Rows.Add(row1);
            }
             _mockDataProvider.Setup(m => m.excecuteQueryWithParameter_DataTable(It.IsAny<SqlCommand>())).Returns(dt);


            // call action
            bool actual = LoginDAL.Instance.ktCurPass(username, password);

            //compare
            Assert.AreEqual(expected, actual);
        }
        #endregion
        #region HienThiDanhSachTaiKhoan
        [Test]
        public void testHienThiDanhSachTaiKhoan()
        {
            DataTable dataTable = new DataTable();
            _mockDataProvider.Setup(m => m.excecuteQuerry(It.IsAny<string>())).Returns(dataTable);

            // call action
            DataTable actual = LoginDAL.Instance.hienThiDanhSachTaiKhoan();

            //compare
            Assert.AreEqual(dataTable.Rows.Count, actual.Rows.Count);
        }
        #endregion

        #region DanhSachUserName
        [Test]
        public void testDanhSachUserName()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("userName", typeof(string));
            dt.Columns.Add("displayName", typeof(string));
            dt.Columns.Add("typeAccount", typeof(int));
            dt.Columns.Add("passWord", typeof(string));

            // Thêm dữ liệu vào DataTable
            DataRow row1 = dt.NewRow();
            row1["id"] = 1;
            row1["userName"] = "trinhxinhdep";
            row1["displayName"] = "Sinh tố";
            row1["typeAccount"] = 1;
            row1["passWord"] = "1515115221938221101234415591195149231127154";

            dt.Rows.Add(row1);
            _mockDataProvider.Setup(m => m.excecuteQuerry(It.IsAny<string>())).Returns(dt);
            // call action
            List<string> actual = LoginDAL.Instance.danhSachUserName();

            //compare
            Assert.AreEqual(1, actual.Count);
        }
        #endregion

        #region ThemRow
        [Test]
        public void testThemRow()
        {
            _mockDataProvider.Setup(m => m.excecuteQueryWithParameter(It.IsAny<SqlCommand>())).Returns(true);

            // setup method
            Account account = new Account() { UserName = "username1", DisplayName = "Người dùng 1", Password = "password1", TypeAccount = 1 };

            // call action
            bool actual = LoginDAL.Instance.themRow(account);

            //compare
            Assert.AreEqual(true, actual);
        }
        #endregion

        #region XoaRow
        [Test]
        public void testXoaRow()
        {
            // setup method
            _mockDataProvider.Setup(m => m.excecuteQueryWithParameter(It.IsAny<SqlCommand>())).Returns(true);

            // call action
            bool actual = LoginDAL.Instance.xoaRow(1);

            //compare
            Assert.AreEqual(true, actual);
        }
        #endregion

        #region TimUserNameByIDAccount
        [Test]
        public void testTimUserNameByIDAccount()
        {
            // setup method
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("username", typeof(string));

            // Thêm dữ liệu vào DataTable
            DataRow row1 = dataTable.NewRow();
          
            row1["username"] = "trinhxinhdep";
            dataTable.Rows.Add(row1);

            _mockDataProvider.Setup(m => m.excecuteQuerry(It.IsAny<string>())).Returns(dataTable);

            // call action
            string actual = LoginDAL.Instance.timUserNameByIDAccount(7);

            //compare
            Assert.AreEqual("trinhxinhdep", actual);
        }
        #endregion

        #region ChinhSuaRow
        [Test]
        public void testChinhSuaRow()
        {
            // setup method
            _mockDataProvider.Setup(m => m.excecuteQueryWithParameter(It.IsAny<SqlCommand>())).Returns(true);

            Account account = new Account() { ID = 2, UserName = "username1", DisplayName = "Người dùng 1", Password = "password1", TypeAccount = 1 };

            // call action
            bool actual = LoginDAL.Instance.chinhSuaRow(account);

            //compare
            Assert.AreEqual(true, actual);
        }
        #endregion

        #region TimKiemTaiKhoan
        [Test]
        public void testTimKiemTaiKhoan()
        {
            DataTable dt = new DataTable();
            _mockDataProvider.Setup(m => m.excecuteQueryWithParameter_DataTable(It.IsAny<SqlCommand>())).Returns(dt);

            // call action
            DataTable actual = LoginDAL.Instance.timKiemTaiKhoan("Thùy Trinh");

            //compare
            Assert.AreEqual(0, actual.Rows.Count);
        }
        #endregion

        #region DoiPassword
        [Test]
        public void testDoiPassword()
        {

            _mockDataProvider.Setup(m => m.excecuteQueryWithParameter(It.IsAny<SqlCommand>())).Returns(true);

            // call action
            bool actual = LoginDAL.Instance.DoiPassword("username1", "password1");

            //compare
            Assert.AreEqual(true, actual);
        }
        #endregion
    }
}
