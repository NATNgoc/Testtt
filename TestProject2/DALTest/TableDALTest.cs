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
    [TestFixture]
    public class TableDALTest
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
        #region themRow
        [Test]
        [TestCase("Bàn 200", "Trống", true)]
        [TestCase("Bàn 105", "Trống", false)]
        public void testThemRow(string name, string status, bool expected)
        {

            //setup method
            _mockDataProvider.Setup(m => m.excecuteQueryWithParameter(It.IsAny<SqlCommand>())).Returns(expected);

            TableDTO tableDTO = new TableDTO() { Name = name, Status = status };
           //call action
            bool actual = TableDAL.Instance.themRow(tableDTO);
           // compare
            Assert.AreEqual(expected, actual);

        }

        #endregion
        #region chinhSuaRow
        [Test]
        [TestCase(1, "Bàn 200", "Trống", true)]
        [TestCase(53, "Bàn 107", "Trống", false)]
        public void testchinhSuaRow(int id, string name, string status, bool expected)
        {

            //setup method
            _mockDataProvider.Setup(m => m.excecuteQueryWithParameter(It.IsAny<SqlCommand>())).Returns(expected);

            TableDTO tableDTO = new TableDTO() { Name = name, Status = status };

            // call action
            bool actual = TableDAL.Instance.chinhSuaRow(tableDTO);
            // compare
            Assert.AreEqual(expected, actual);

        }
        #endregion
        #region danhsachbanan
        [Test]
        public void testDanhSachBanAn()
        {
            //setup method
            DataTable dt = new DataTable();
            dt.Columns.Add("name", typeof(string));

            // Thêm dữ liệu vào DataTable
            DataRow row1 = dt.NewRow();

            row1["name"] = "Bàn 1";
            dt.Rows.Add(row1);
            _mockDataProvider.Setup(m => m.excecuteQuerry(It.IsAny<string>())).Returns(dt);


          //  call action
            List<string> listTable = TableDAL.Instance.danhSachBanAn();
           // compare
            Assert.AreEqual(1, listTable.Count);
        }
        #endregion
        #region DanhSachIDTable
        [Test]
        public void testDanhSachIDTable()
        {
            //setup method
            DataTable table = new DataTable();
            table.Columns.Add("id", typeof(int));

            // Thêm dữ liệu vào DataTable
            DataRow row1 = table.NewRow();

            row1["id"] = 1;
            table.Rows.Add(row1);
            _mockDataProvider.Setup(m => m.excecuteQuerry(It.IsAny<string>())).Returns(table);

           // call action
            List<int> kq = TableDAL.Instance.danhSachIDTable();
           // compare
            Assert.AreEqual(1, kq.Count);
        }
        #endregion
        #region xoaRow
        [Test]
        [TestCase(73, true)]
        [TestCase(1, false)]

        public void testXoaRow(int ma, bool expected)
        {
            // setup method
            _mockDataProvider.Setup(m => m.excecuteQueryWithParameter(It.IsAny<SqlCommand>())).Returns(expected);

            // call action
            bool actual = TableDAL.Instance.xoaRow(ma);
           // compare
            Assert.AreEqual(expected, actual);

        }
        #endregion
        #region timKiemTable
        [Test]
        [TestCase("Bàn VIP100")]
        public void testTimKiemTable(string n)
        {

            // setup method
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Mã bàn ăn", typeof(int));
            dataTable.Columns.Add("Tên bàn ăn", typeof(string));
            dataTable.Columns.Add("Trạng thái", typeof(string));

            // Thêm dữ liệu vào DataTable
            DataRow row1 = dataTable.NewRow();
            row1["Mã bàn ăn"] = 1;
            row1["Tên bàn ăn"] = "Bàn 200";
            row1["Trạng thái"] = "Trống";
            dataTable.Rows.Add(row1);
            _mockDataProvider.Setup(m => m.excecuteQueryWithParameter_DataTable(It.IsAny<SqlCommand>())).Returns(dataTable);


            // call action
            DataTable actual = TableDAL.Instance.timKiemTable(n);
            //compare
            Assert.AreEqual(1, actual.Rows.Count);


        }
        #endregion
        #region timNameTableByIDTable
        [Test]
        [TestCase("1")]

        public void testimNameTableByIDTable(int idTable)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("name", typeof(string));

            // Thêm dữ liệu vào DataTable
            DataRow row1 = dataTable.NewRow();
            row1["name"] = "Bàn 200";
            dataTable.Rows.Add(row1);
            _mockDataProvider.Setup(m => m.excecuteQuerry(It.IsAny<string>())).Returns(dataTable);

            // call action
            string actual = TableDAL.Instance.timNameTableByIDTable(idTable);
            //compare
            Assert.AreEqual("Bàn 200", actual);


        }
        #endregion
        #region HienThiTable
        [Test]
        public void testHienThiTable()
        {
            DataTable dataTable = new DataTable();
            _mockDataProvider.Setup(m => m.excecuteQuerry(It.IsAny<string>())).Returns(dataTable);

            // call action
            DataTable actual = TableDAL.Instance.HienThiTable();
            //compare
            Assert.AreEqual(0, actual.Rows.Count);

        }
        #endregion
    }

}
