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
    public  class CategoryDALTest
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
        [TestCase("Kem F", true)] // thay A,B,C 
        [TestCase("Mì cay", false)]
        public void testThemRow(string name, bool expected)
        {
            // set up method
            _mockDataProvider.Setup(m => m.excecuteQueryWithParameter(It.IsAny<SqlCommand>())).Returns(expected);

            // call action

            bool actual = CategoryDAL.Instance.themRow(name);
            //compare
            Assert.AreEqual(expected, actual);

        }
        #endregion

        #region hienThiDanhSachFoodCategory
        [Test]
        public void testHienThiDanhSachFoodCategory()
        {
            // setup method
            DataTable dataTable = new DataTable();
            _mockDataProvider.Setup(m => m.excecuteQuerry(It.IsAny<string>())).Returns(dataTable);
            // call action
            DataTable actual = CategoryDAL.Instance.hienThiDanhSachFoodCategory();
            //compare
            Assert.AreEqual(0, actual.Rows.Count);
        }
        #endregion

        #region danhSachCategory
        [Test]
        [TestCase(0)]
        [TestCase(1)]

        public void testdanhSachCategory(int expectResult)
        {
            // setup method

            DataTable dataTable = new DataTable();
            if(expectResult==0)
            _mockDataProvider.Setup(m => m.excecuteQuerry(It.IsAny<string>())).Returns(dataTable);
            else
            {
                dataTable.Columns.Add("name", typeof(string));

                // Thêm dữ liệu vào DataTable
                DataRow row1 = dataTable.NewRow();
                row1["name"] = "Sinh tố";
                dataTable.Rows.Add(row1);
                _mockDataProvider.Setup(m => m.excecuteQuerry(It.IsAny<string>())).Returns(dataTable);

            }
            // call action
            List<string> listTable = CategoryDAL.Instance.danhSachCategory();
            //compare
            Assert.AreEqual(expectResult, listTable.Count);
        }
        #endregion

        // ko ảnh hưởng
        #region chinhSuaRow
        [Test]
        [TestCase(53, "Kem 1", true)] // thay 1,2,3
        [TestCase(53, "Sinh tố", false)]

        public void testchinhSuaRow(int id, string name, bool expected)
        {

            // setup method
            _mockDataProvider.Setup(m => m.excecuteQueryWithParameter(It.IsAny<SqlCommand>())).Returns(expected);

            CategoryDTO categoryDTO = new CategoryDTO() { Id = id, Name = name };

            // call action
            bool actual = CategoryDAL.Instance.chinhSuaRow(categoryDTO);
            //compare
            Assert.AreEqual(expected, actual);

        }
        #endregion
        #region xoaLoaiMonAn
        [Test]
        [TestCase(18, false)]
        [TestCase(34, true)]

        public void testxoaLoaiMonAn(int ma, bool expected)
        {
            // setup method
            _mockDataProvider.Setup(m => m.excecuteQueryWithParameter(It.IsAny<SqlCommand>())).Returns(expected);

            // call action 
            bool actual = CategoryDAL.Instance.xoaLoaiMonAn(ma);
            // compare
            Assert.AreEqual(expected, actual);

        }
        #endregion
     
        #region TimNameCateGory_FoodByID
        [Test]
        [TestCase(18, "Sinh tố")]


        public void testTimNameCateGory_FoodByID(int id, string expected)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("name", typeof(string));

            // Thêm dữ liệu vào DataTable
            DataRow row1 = dataTable.NewRow();
            row1["name"] = expected;
            dataTable.Rows.Add(row1);
            _mockDataProvider.Setup(m => m.excecuteQuerry(It.IsAny<string>())).Returns(dataTable);

            // call action
            string actual = CategoryDAL.Instance.timNameCateGory_FoodByID(id);

            //compare
            Assert.AreEqual(expected, actual);
        }
        #endregion
        #region TimKiemLoaiMonAn
        [Test]
        [TestCase("Trà sữa")]
        public void testTimKiemLoaiMonAn(string name)
        {
            DataTable dataTable = new DataTable();
            // setup method
            _mockDataProvider.Setup(m => m.excecuteQueryWithParameter_DataTable(It.IsAny<SqlCommand>())).Returns(dataTable);

            // call action
            DataTable actual = CategoryDAL.Instance.TimKiemLoaiMonAn(name);
            //compare
            Assert.AreEqual(0, actual.Rows.Count);
        }
        #endregion
    }
}
