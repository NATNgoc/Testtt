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
    public  class Bill_InforDALTest
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
        [TestCase("69", "11", 1, true)] 
        [TestCase("69", "-1", 1, false)]

        public void themMonAnChoBill(int maBill, int maFood, int soLuong, bool expectedResult)
        {
            //setup method
            _mockDataProvider.Setup(m => m.excecuteQueryWithParameter(It.IsAny<SqlCommand>())).Returns(expectedResult);

            // call action
            bool actual = Bill_InforDAL.Instance.themMonAnChoBill(maBill, maFood, soLuong);

            //compare
            Assert.AreEqual(expectedResult, actual);
        }
        #region getBill_Infor 
        [Test]
        [TestCase(100, 0)]

        public void getBill_Infor(int maBill, int expectedResult)
        {
            // setup method
            DataTable dt = new DataTable();
            _mockDataProvider.Setup(m => m.excecuteQuerry(It.IsAny<string>())).Returns(dt);

            // call action
            DataTable actual = Bill_InforDAL.Instance.getBill_Infor(maBill);

            //compare
            Assert.AreEqual(expectedResult, actual.Rows.Count);
        }
        #endregion
        #region capNhatSoLuong
        [Test]
        [TestCase("69", "11",2, true)]
        [TestCase("69", "11", 2, false)]

        public void capNhapSoLuong(int maBill, int maMon, int soLuong, bool expectedResult)
        {
            // setup method
            _mockDataProvider.Setup(m => m.excecuteQueryWithParameter(It.IsAny<SqlCommand>())).Returns(expectedResult);

            // call action
            bool actual = Bill_InforDAL.Instance.capNhapSoLuong( maBill,  maMon,  soLuong);

            //compare
            Assert.AreEqual(expectedResult, actual);
        }
        #endregion
    }
}
