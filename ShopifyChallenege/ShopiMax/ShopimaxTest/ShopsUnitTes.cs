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
    public class ShopTest
    {
        private Mock<IShopiMaxRepository> mockShopiMaxRepo = null;
        private Mock<IMapper> mockMapperRepo = null;
        private ShopsController controllerObj = null;
         
        [SetUp]
        public void Setup()
        {
            mockShopiMaxRepo = new Mock<IShopiMaxRepository>();
            mockMapperRepo = new Mock<IMapper>();
            controllerObj = new ShopsController(mockShopiMaxRepo.Object, mockMapperRepo.Object);
        }

        [Test]
        public async Task GetShops_Positive()
        {
            //Arrange
            List<Shop> ShopObjList = new List<Shop>();
             List<ShopDto> ShopList = new List<ShopDto>();
            Shop ShopObj = new Shop(){
                ShopID = 1,
                ShopName = "Nike",
                ShopCity = "Halifax",
                ShopCountry = "Canada",
                ShopState = "NS",
                ShopStreet = "2001 Brunswick",
                ShopZip = "B3J3J7"            
            };
             ShopDto Shop = new ShopDto(){
                ShopID = 1,
                ShopName = "Nike",
                ShopCity = "Halifax",
                ShopCountry = "Canada",
                ShopState = "NS",
                ShopStreet = "2001 Brunswick",
                ShopZip = "B3J3J7"    
                
            };
            ShopList.Add(Shop);
            ShopObjList.Add(ShopObj);
            //Mock
            mockShopiMaxRepo.Setup(x => x.GetShops()).ReturnsAsync(ShopObjList);
            mockMapperRepo.Setup(mock => mock.Map<IEnumerable<ShopDto>>(It.IsAny<IEnumerable<Shop>>())).Returns(ShopList);
            

            //Result
            IActionResult result = await controllerObj.GetShops();
            OkObjectResult Objresult = result as OkObjectResult;
            List<ShopDto> ShopDisplay = Objresult.Value as List<ShopDto>;


            //Assert
            Assert.IsTrue(ShopDisplay[0].ShopID == Shop.ShopID);
            
        }

        [Test]
        public void  GetShops_NotAuthorized_Negative()
        {
            
                //Arrange
                List<Shop> ShopObjList = new List<Shop>();
                Shop ShopObj = new Shop(){
                ShopID = 1,
                ShopName = "Nike",
                ShopCity = "Halifax",
                ShopCountry = "Canada",
                ShopState = "NS",
                ShopStreet = "2001 Brunswick",
                ShopZip = "B3J3J7" 
                };
                ShopObjList.Add(ShopObj);

                //Mock
                mockShopiMaxRepo.Setup(x => x.GetShops()).ThrowsAsync(new AuthenticationException());
                
                //Assert
                Assert.ThrowsAsync<AuthenticationException>(() =>  controllerObj.GetShops());
            
            
        }
        [Test]
        public async Task GetShop_Positive()
        {
            //Arrange
            Shop ShopObj = new Shop(){
                 ShopID = 1,
                ShopName = "Nike",
                ShopCity = "Halifax",
                ShopCountry = "Canada",
                ShopState = "NS",
                ShopStreet = "2001 Brunswick",
                ShopZip = "B3J3J7" 
            };
            ShopDto Shop = new ShopDto(){
               ShopID = 1,
                ShopName = "Nike",
                ShopCity = "Halifax",
                ShopCountry = "Canada",
                ShopState = "NS",
                ShopStreet = "2001 Brunswick",
                ShopZip = "B3J3J7" 
                
            };
           
            //Mock
            mockShopiMaxRepo.Setup(x => x.GetShop(1)).ReturnsAsync(ShopObj);
            mockMapperRepo.Setup(mock => mock.Map<ShopDto>(It.IsAny<Shop>())).Returns(Shop);
            

            //Result
            IActionResult result = await controllerObj.GetShop(1);
            OkObjectResult Objresult = result as OkObjectResult;
            ShopDto ShopDisplay = Objresult.Value as ShopDto;


            //Assert
            Assert.IsTrue(ShopDisplay.ShopID == Shop.ShopID);
            Assert.IsTrue(ShopDisplay.ShopCountry == Shop.ShopCountry);
            
            
        }

        [Test]
        public async Task EditShop_Positive()
        {
            //Arrange
            Shop ShopInput = new Shop(){
                 ShopID = 1,
                ShopName = "Nike",
                ShopCity = "Halifax",
                ShopCountry = "Canada",
                ShopState = "NS",
                ShopStreet = "2001 Brunswick",
                ShopZip = "B3J3J7" 
            };

            Shop ShopUpdated = new Shop(){
              ShopID = 1,
                ShopName = "Adidas",
                ShopCity = "Halifax",
                ShopCountry = "Canada",
                ShopState = "NS",
                ShopStreet = "2001 Brunswick",
                ShopZip = "B3J3J7" 
            };


           
            //Mock
            mockShopiMaxRepo.Setup(x => x.EditShop(ShopInput)).ReturnsAsync(ShopUpdated);
            

            //Result
            IActionResult result = await controllerObj.EditShop(ShopInput);
 
            //Assert
            Assert.AreEqual((int)((StatusCodeResult)result).StatusCode, 201);
            
        }
        [Test]
        public async Task AddShop_Positive()
        {
            //Arrange
            Shop ShopInput = new Shop(){
                 ShopID = 1,
                ShopName = "Nike",
                ShopCity = "Halifax",
                ShopCountry = "Canada",
                ShopState = "NS",
                ShopStreet = "2001 Brunswick",
                ShopZip = "B3J3J7" 
            };
       
            //Mock
            mockShopiMaxRepo.Setup(x => x.Add(ShopInput));
            

            //Result
            IActionResult result =  await controllerObj.Addshop(ShopInput);
 
            //Assert
            Assert.AreEqual((int)((StatusCodeResult)result).StatusCode, 201);
            
        }
    }
}