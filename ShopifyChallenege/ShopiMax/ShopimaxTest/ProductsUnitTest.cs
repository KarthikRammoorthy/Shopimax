using NUnit.Framework;
using Moq;
using NUnit;
using Shopimax.API.Controllers;
using Shopimax.API.Models;
using Shopimax.API.Data;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Security.Authentication;
using Shopimax.API.Dtos;

namespace Tests
{
    [TestFixture]
    public class ProductTest
    {
        private Mock<IShopiMaxRepository> mockShopiMaxRepo = null;
        private Mock<IMapper> mockMapperRepo = null;
        private ProductController controllerObj = null;
         
        [SetUp]
        public void Setup()
        {
            mockShopiMaxRepo = new Mock<IShopiMaxRepository>();
            mockMapperRepo = new Mock<IMapper>();
            controllerObj = new ProductController(mockShopiMaxRepo.Object, mockMapperRepo.Object);
        }

        [Test]
        public async Task GetProducts_Positive()
        {
            //Arrange
            List<Product> ProductObjList = new List<Product>();
             List<ProductDisplayDto> ProductList = new List<ProductDisplayDto>();
            Product ProductObj = new Product(){
                ProductID = 1,
                ProductRate = 500,
                ProductAvailable = true,
                ProductAvailableQty = "1",
                ProductCategory = "Shoe",
                ProductName = "Adidas"

                
            };
             ProductDisplayDto Product = new ProductDisplayDto(){
                ProductID = 1,
                ProductRate = "$500",
                ProductAvailable = true,
                ProductAvailableQty = "1",
                ProductCategory = "Shoe",
                ProductName = "Adidas"
                
            };
            ProductList.Add(Product);
            ProductObjList.Add(ProductObj);
            //Mock
            mockShopiMaxRepo.Setup(x => x.GetProducts()).ReturnsAsync(ProductObjList);
            mockMapperRepo.Setup(mock => mock.Map<IEnumerable<ProductDisplayDto>>(It.IsAny<IEnumerable<Product>>())).Returns(ProductList);
            

            //Result
            IActionResult result = await controllerObj.GetProducts();
            OkObjectResult Objresult = result as OkObjectResult;
            List<ProductDisplayDto> ProductDisplay = Objresult.Value as List<ProductDisplayDto>;


            //Assert
            Assert.IsTrue(ProductDisplay[0].ProductID == Product.ProductID);
            
        }

        [Test]
        public void  GetProducts_NotAuthorized_Negative()
        {
            
                //Arrange
                List<Product> ProductObjList = new List<Product>();
                Product ProductObj = new Product(){
                ProductID = 1,
                ProductRate = 500,
                ProductAvailable = true,
                ProductAvailableQty = "1",
                ProductCategory = "Shoe",
                ProductName = "Adidas"
                };
                ProductObjList.Add(ProductObj);

                //Mock
                mockShopiMaxRepo.Setup(x => x.GetProducts()).ThrowsAsync(new AuthenticationException());
                
                //Assert
                Assert.ThrowsAsync<AuthenticationException>(() =>  controllerObj.GetProducts());
            
            
        }
        [Test]
        public async Task GetProduct_Positive()
        {
            //Arrange
            Product ProductObj = new Product(){
                 ProductID = 1,
                ProductRate = 500,
                ProductAvailable = true,
                ProductAvailableQty = "1",
                ProductCategory = "Shoe",
                ProductName = "Adidas"
            };
            ProductDisplayDto Product = new ProductDisplayDto(){
                ProductID = 1,
                ProductRate = "$500",
                ProductAvailable = true,
                ProductAvailableQty = "1",
                ProductCategory = "Shoe",
                ProductName = "Adidas"
                
            };
           
            //Mock
            mockShopiMaxRepo.Setup(x => x.GetProduct(1)).ReturnsAsync(ProductObj);
            mockMapperRepo.Setup(mock => mock.Map<ProductDisplayDto>(It.IsAny<Product>())).Returns(Product);
            

            //Result
            IActionResult result = await controllerObj.GetProduct(1);
            OkObjectResult Objresult = result as OkObjectResult;
            ProductDisplayDto ProductDisplay = Objresult.Value as ProductDisplayDto;


            //Assert
            Assert.IsTrue(ProductDisplay.ProductID == Product.ProductID);
            Assert.IsTrue(ProductDisplay.ProductAvailable == Product.ProductAvailable);
            
            
        }

        [Test]
        public async Task EditProduct_Positive()
        {
            //Arrange
            Product ProductInput = new Product(){
                 ProductID = 1,
                ProductRate = 500,
                ProductAvailable = true,
                ProductAvailableQty = "1",
                ProductCategory = "Shoe",
                ProductName = "Adidas"
            };

            Product ProductUpdated = new Product(){
                 ProductID = 1,
                ProductRate = 500,
                ProductAvailable = true,
                ProductAvailableQty = "1",
                ProductCategory = "Shoe",
                ProductName = "Adidas"
            };


           
            //Mock
            mockShopiMaxRepo.Setup(x => x.EditProduct(ProductInput)).ReturnsAsync(ProductUpdated);
            

            //Result
            IActionResult result = await controllerObj.EditProduct(ProductInput);
 
            //Assert
            Assert.AreEqual((int)((StatusCodeResult)result).StatusCode, 201);
            
        }
        [Test]
        public async Task AddProduct_Positive()
        {
            //Arrange
            Product ProductInput = new Product(){
                 ProductID = 1,
                ProductRate = 500,
                ProductAvailable = true,
                ProductAvailableQty = "1",
                ProductCategory = "Shoe",
                ProductName = "Adidas"
            };
       
            //Mock
            mockShopiMaxRepo.Setup(x => x.Add(ProductInput));
            

            //Result
            IActionResult result =  await controllerObj.AddProduct(ProductInput);
 
            //Assert
            Assert.AreEqual((int)((StatusCodeResult)result).StatusCode, 201);
            
        }
    }
}