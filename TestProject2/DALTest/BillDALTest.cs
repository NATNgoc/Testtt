using DAL;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestProject2.DALTest
{
    public  class BillDALTest
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
        #region ThemBill
        [Test]
        [TestCase(1, true)]
        [TestCase(2, false)]

        public void testThemBill(int maBan, bool expected)
        {
            // setup method
            _mockDataProvider.Setup(m => m.excecuteQueryWithParameter(It.IsAny<SqlCommand>())).Returns(expected);

            // call action
            bool actual = BillDAL.Instance.themBill(maBan);

            //compare
            Assert.AreEqual(expected, actual);
        }
        #endregion
        #region ThucHienCheckOut
        [Test]
        [TestCase(1, true)]
        [TestCase(2, false)]
        public void testThucHienCheckOut(int maBan, bool expected)
        {
            // setup method
            // mock uncheck bill 
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("id", typeof(int));


            if (expected == true)
            {

                // Thêm dữ liệu vào DataTable
                DataRow row1 = dataTable.NewRow();
                row1["id"] = expected;
                dataTable.Rows.Add(row1);
                _mockDataProvider.Setup(m => m.excecuteQueryWithParameter_DataTable(It.IsAny<SqlCommand>())).Returns(dataTable);
            }
            else
            {
                _mockDataProvider.Setup(m => m.excecuteQueryWithParameter_DataTable(It.IsAny<SqlCommand>())).Returns(dataTable);

            }
            _mockDataProvider.Setup(m => m.excecuteQueryWithParameter(It.IsAny<SqlCommand>())).Returns(expected);

            // call action
            bool actual = BillDAL.Instance.thucHienCheckOut(maBan);

            //compare
            Assert.AreEqual(expected, actual);
        }
        #endregion
        #region GetUncheckBillByTable
        [Test]
        [TestCase(68, 245)]
        [TestCase(68, -1)]

        public void testGetUncheckBillByTable(int id, int expected)
        {
            // setup method
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("id", typeof(int));
            

            if (expected != -1)
            {

                // Thêm dữ liệu vào DataTable
                DataRow row1 = dataTable.NewRow();
                row1["id"] = expected;
                dataTable.Rows.Add(row1);
                _mockDataProvider.Setup(m => m.excecuteQueryWithParameter_DataTable(It.IsAny<SqlCommand>())).Returns(dataTable);
            }
            else
            {
                _mockDataProvider.Setup(m => m.excecuteQueryWithParameter_DataTable(It.IsAny<SqlCommand>())).Returns(dataTable);

            }
            // call action
            int actual = BillDAL.Instance.getUncheckBillByTable(id);

            //compare
            Assert.AreEqual(expected, actual);
        }
        #endregion
        #region ChuyenBan
        [Test]
        [TestCase(1, 2, true)]
        [TestCase(1, 3, false)]

        public void testChuyenBan(int maBill, int maBanNew, bool expected)
        {
            // setup method
            _mockDataProvider.Setup(m => m.excecuteQueryWithParameter(It.IsAny<SqlCommand>())).Returns(expected);

            // call action
            bool actual = BillDAL.Instance.ChuyenBan(maBill, maBanNew);

            //compare
            Assert.AreEqual(expected, actual);
        }
        #endregion
        #region HienThiDoanhThu
        [Test]
        public void testHienThiDoanhThu()
        {
            // setup method

            DataTable dataTable = new DataTable();
            _mockDataProvider.Setup(m => m.excecuteQueryWithParameter_DataTable(It.IsAny<SqlCommand>())).Returns(dataTable);

            string dateString = "2023-11-11";
            DateTime startDate = DateTime.ParseExact(dateString, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime endDate = startDate;
            // call action
            DataTable actual = BillDAL.Instance.HienThiDoanhThu(1, startDate, endDate);

            //compare
            Assert.AreEqual(0, actual.Rows.Count);
        }
        #endregion
        #region CapNhatDiscount
        [Test]
        [TestCase(2, 50, true)]
        [TestCase(2, 50, false)]

        public void TestCapNhatDiscount(int maBill, decimal discount, bool expected)
        {
            // stup method 
            _mockDataProvider.Setup(m => m.excecuteQueryWithParameter(It.IsAny<SqlCommand>())).Returns(expected);

            // call action
            bool actual = BillDAL.Instance.capNhatDiscount(maBill, discount);

            // compare
            Assert.AreEqual(expected, actual);
        }
        #endregion
        #region GetDiscount
        [Test]
        public void TestGetDiscount_ReturnsDiscount_WhenExecuteScalarReturnsDiscount()
        {
            // setup method

            int maBill = 2;
            int expectedDiscount = 50;
            _mockDataProvider.Setup(m => m.ExecuteScalar(It.IsAny<SqlCommand>())).Returns(expectedDiscount);



            // call action
            int actual = BillDAL.Instance.getDiscount(maBill);

            // compare
            Assert.AreEqual(expectedDiscount, actual);
        }
        #endregion
        #region GetSizeOfBill
        [Test]
        public void TestGetSizeOfBill_ReturnsCountOfBills_WhenExecuteScalarReturnsCount()
        {
            int expectedCount = 50;

            // setup method
            _mockDataProvider.Setup(m => m.ExecuteScalar(It.IsAny<SqlCommand>())).Returns(expectedCount);


            string dateString = "2023-11-11";
            DateTime startDate = DateTime.ParseExact(dateString, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime endDate = startDate;

            // call action
            int actual = BillDAL.Instance.getSizeOfBill(startDate, endDate);

            // compare
            Assert.AreEqual(expectedCount, actual);
        }
        #endregion
        #region HienThiTongDanhThu
        [Test]
        [TestCase(0)]
        [TestCase(575500)]
        public void testHienThiTongDanhThu(int expected)
        { // setup method
            DataTable dataTable = new DataTable();
            if (expected != 0)
            {
                dataTable.Columns.Add("Tổng", typeof(int));

                    // Thêm dữ liệu vào DataTable
                    DataRow row1 = dataTable.NewRow();
                    row1["Tổng"] = expected;
                    dataTable.Rows.Add(row1);
               
            }
            else
            {
                dataTable.Columns.Add("Tổng", typeof(int));

                // Thêm dữ liệu vào DataTable
                DataRow row1 = dataTable.NewRow();
                row1["Tổng"] = 0;
                dataTable.Rows.Add(row1);
            }    
            _mockDataProvider.Setup(m => m.excecuteQueryWithParameter_DataTable(It.IsAny<SqlCommand>())).Returns(dataTable);

            string dateString = "2023-11-11";
            DateTime startDate = DateTime.ParseExact(dateString, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime endDate = startDate;

            // call action
            int actual = BillDAL.Instance.hienThiTongDanhThu(startDate, endDate);

            //compare
            Assert.AreEqual(expected, actual);
        }
        #endregion
        #region xoaBillInfor
        [Test]
        [TestCase(1,  true)]
        [TestCase(2, false)]

        public void TestXoaBillInfor(int maBill, bool expected)
        {
            // setup method
            _mockDataProvider.Setup(m => m.excecuteQueryWithParameter(It.IsAny<SqlCommand>())).Returns(expected);

            // call action
            bool actual = BillDAL.Instance.xoaBill_Infor(maBill);

            // compare 
            Assert.AreEqual(expected,actual);
        }
        #endregion
    }
}
