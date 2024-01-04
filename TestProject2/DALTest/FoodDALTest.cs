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
    public class FoodDALTest
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

        private static IEnumerable<TestCaseData> FoodDTOTestCases
        {
            get
            {
                yield return new TestCaseData(new FoodDTO() { TenMon = "Bánh giò", Gia = 50000, Loai = 17 }, true);
                yield return new TestCaseData(new FoodDTO() { TenMon = "Sinh tố", Gia = 50000, Loai = 17 }, false);

                // Thêm các trường hợp kiểm tra khác tại đây
            }
        }
        private static IEnumerable<TestCaseData> updateFoodDTOTestCases

        {
            get
            {
                yield return new TestCaseData(new FoodDTO() {  Id = 56, TenMon = "Bánh giò bao ngon", Gia = 50000, Loai = 18 }, true);
                yield return new TestCaseData(new FoodDTO() { Id = 56, TenMon = "Bánh giò bao ngon", Gia = 50000, Loai = 18 }, false);

                // Thêm các trường hợp kiểm tra khác tại đây
            }
        }

        #region themRow
        [Test, TestCaseSource(nameof(FoodDTOTestCases))]
        public void testThemRow(FoodDTO foodDTO, bool expected)
        {
            _mockDataProvider.Setup(m => m.excecuteQueryWithParameter(It.IsAny<SqlCommand>())).Returns(expected);

            // call action
            bool actual = FoodDAL.Instance.themRow(foodDTO);

            //compare
            Assert.AreEqual(expected, actual);
        }
        #endregion
        #region DanhSachFood    
        [Test]
        public void testDanhSachFood()
        {
            // setup method
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("name", typeof(string));

            // Thêm dữ liệu vào DataTable
            DataRow row1 = dataTable.NewRow();
            row1["name"] = "Sinh tố";
            dataTable.Rows.Add(row1);
            _mockDataProvider.Setup(m => m.excecuteQuerry(It.IsAny<string>())).Returns(dataTable);

            // call action
            List<string> actual = FoodDAL.Instance.danhSachFood();

            //compare
            Assert.AreEqual(1, actual.Count);
        }
        #endregion
        #region HienThiDanhSachFood
        [Test]
        public void testHienThiDanhSachFood()
        {
           // setup method
            DataTable dataTable = new DataTable();
            _mockDataProvider.Setup(m => m.excecuteQuerry(It.IsAny<string>())).Returns(dataTable);

            // call action
            DataTable actual = FoodDAL.Instance.hienThiDanhSachFood();

            //compare
            Assert.AreEqual(dataTable.Rows.Count, actual.Rows.Count);
        }
        #endregion


        // ko ảnh hưởng
      
        #region testGetFoodByCategory
        [Test]
        [TestCase(18, 0)]
        [TestCase(18, 2)]

        public void testGetFoodByCategory(int idCategory, int expected)
        {
            // set up method
            // setup method
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("id", typeof(int));
            dataTable.Columns.Add("name", typeof(string));
            dataTable.Columns.Add("price", typeof(double));
            dataTable.Columns.Add("idCategory", typeof(int));
            if (expected != 0)
            {
                // Thêm dữ liệu vào DataTable
                DataRow row1 = dataTable.NewRow();
                row1["id"] = 1;
                row1["name"] = "Mì xào";
                row1["price"] = 50000;
                row1["idCategory"] = idCategory;
                dataTable.Rows.Add(row1);
                DataRow row2 = dataTable.NewRow();

                row2["id"] = 2;
                row2["name"] = "Mì xào";
                row2["price"] = 50000;
                row2["idCategory"] = idCategory;
                dataTable.Rows.Add(row2);
            }
            _mockDataProvider.Setup(m => m.excecuteQuerry(It.IsAny<string>())).Returns(dataTable);

            // call action
            List<FoodDTO> actual = FoodDAL.Instance.getFoodByCateGory(idCategory);

            //compare
            Assert.AreEqual(expected, actual.Count);
        }
        #endregion
        #region ChinhSuaRow
        [Test, TestCaseSource(nameof(updateFoodDTOTestCases))]
        public void testChinhSuaRow(FoodDTO foodDTO, bool expected)
        {
            _mockDataProvider.Setup(m => m.excecuteQueryWithParameter(It.IsAny<SqlCommand>())).Returns(expected);

            // call action
            bool actual = FoodDAL.Instance.chinhSuaRow(foodDTO);

            //compare
            Assert.AreEqual(expected, actual);
        }
        #endregion
        #region TimNameFoodByIDFood
        [Test]
        [TestCase(11)]
        public void testTimNameFoodByIDFood(int id)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("name", typeof(string));

            // Thêm dữ liệu vào DataTable
            DataRow row1 = dataTable.NewRow();
            row1["name"] = "Sinh tố";
            dataTable.Rows.Add(row1);
            _mockDataProvider.Setup(m => m.excecuteQuerry(It.IsAny<string>())).Returns(dataTable);
            // call action
            string actual = FoodDAL.Instance.timNameFoodByIDFood(id);

            //compare
            Assert.AreEqual("Sinh tố", actual);
        }
        #endregion
        #region TimKiemMonAn
        [Test]
        [TestCase("Cà phê đen", 0)]
        public void testTimKiemMonAn(string name, int expected)
        {
            DataTable dataTable = new DataTable();

            _mockDataProvider.Setup(m => m.excecuteQueryWithParameter_DataTable(It.IsAny<SqlCommand>())).Returns(dataTable);

            // call action
            DataTable actual = FoodDAL.Instance.TimKiemMonAn(name);

            //compare
            Assert.AreEqual(expected, actual.Rows.Count);
        }
        #endregion
        #region XoaMonAn
        [Test]
        [TestCase(11, false)]
        [TestCase(54, true)]

        public void testXoaMonAn(int id, bool expected)
        {
            _mockDataProvider.Setup(m => m.excecuteQueryWithParameter(It.IsAny<SqlCommand>())).Returns(expected);

            // call action
            bool actual = FoodDAL.Instance.xoaMonAn(id);

            //compare
            Assert.AreEqual(expected, actual);
        }
        #endregion
    }
}
