using DAL;
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
    public class MenuDALTest
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
        [Test]
        #region testDoiPassword 
        public void hienThiMenuByIDBill()
        {
            DataTable dataTable = new DataTable();
            
            _mockDataProvider.Setup(m => m.excecuteQueryWithParameter_DataTable(It.IsAny<SqlCommand>())).Returns(dataTable);
            // call action
            DataTable actual = MenuDAL.Instance.hienThiMenuByIDBill(2);

            //compare
            Assert.AreEqual(0, actual.Rows.Count);
        }
        #endregion
        #region hienThiMenu
        [Test]
        [TestCase(1)]
        [TestCase(0)]

        public void hienThiMenu(int expected)
        {
            DataTable dataTable = new DataTable();
            if (expected != 0)
            {
                dataTable.Columns.Add("id", typeof(int));

                // Thêm dữ liệu vào DataTable
                DataRow row1 = dataTable.NewRow();

                row1["id"] = 1;
                dataTable.Rows.Add(row1);
            }
            _mockDataProvider.Setup(m => m.excecuteQueryWithParameter_DataTable(It.IsAny<SqlCommand>())).Returns(dataTable);

            int idBill=0;
            // call action
            DataTable actual = MenuDAL.Instance.hienThiMenu(38, ref idBill);

            //compare
            Assert.AreEqual(expected, actual.Rows.Count);
        }
        #endregion


    }
}
