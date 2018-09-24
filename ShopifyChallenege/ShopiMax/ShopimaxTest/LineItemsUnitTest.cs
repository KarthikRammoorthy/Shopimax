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
    public class LineItemTest
        {
        private Mock<IShopiMaxRepository> mockShopiMaxRepo = null;
        private Mock<IMapper> mockMapperRepo = null;
        private LineItemController controllerObj = null;
         
        [SetUp]
        public void Setup()
        {
            mockShopiMaxRepo = new Mock<IShopiMaxRepository>();
            mockMapperRepo = new Mock<IMapper>();
            controllerObj = new LineItemController(mockShopiMaxRepo.Object, mockMapperRepo.Object);
        }

        [Test]
        public async Task GetLineItems_Positive()
        {
            //Arrange
            List<LineItem> lineItemObjList = new List<LineItem>();
            List<LineItemDisplayDto> lineItemList = new List<LineItemDisplayDto>();
            LineItem lineItemObj = new LineItem(){
                LineItemID = 1,
                LineItemTotalPrice = 100,
                LineItemQuantity = 2,
                ProductID = 12,
                OrderID = 11
            };
            LineItemDisplayDto lineItemDisplay = new LineItemDisplayDto(){
                LineItemID = 1,
                LineItemTotalPrice = "$100",
                LineItemQuantity = 2,
                ProductID = 12,
                OrderID = 11
            };
            lineItemObjList.Add(lineItemObj);
            lineItemList.Add(lineItemDisplay);
            //Mock
            mockShopiMaxRepo.Setup(x => x.GetLineItems()).ReturnsAsync(lineItemObjList);
            mockMapperRepo.Setup(mock => mock.Map<IEnumerable<LineItemDisplayDto>>(It.IsAny<IEnumerable<LineItem>>())).Returns(lineItemList);
            
            

            //Result
            IActionResult result = await controllerObj.GetLineItems();
            OkObjectResult Objresult = result as OkObjectResult;
            List<LineItemDisplayDto> lineItem = Objresult.Value as List<LineItemDisplayDto>;


            //Assert
            Assert.IsTrue(lineItem[0].OrderID == 11);
            
        }

        [Test]
        public void  GetLineItems_NotAuthorized_Negative()
        {
            
                //Arrange
                List<LineItem> lineItemObjList = new List<LineItem>();
                LineItem lineItemObj = new LineItem(){
                    LineItemID = 1,
                    LineItemTotalPrice = 100,
                    LineItemQuantity = 2,
                    ProductID = 12,
                    OrderID = 11
                };
                lineItemObjList.Add(lineItemObj);

                //Mock
                mockShopiMaxRepo.Setup(x => x.GetLineItems()).ThrowsAsync(new AuthenticationException());
                
                //Assert
                Assert.ThrowsAsync<AuthenticationException>(() =>  controllerObj.GetLineItems());
            
            
        }
        [Test]
        public async Task GetLineItem_Positive()
        {
            //Arrange
            LineItem lineItemObj = new LineItem(){
                LineItemID = 1,
                LineItemTotalPrice = 100,
                LineItemQuantity = 2,
                ProductID = 12,
                OrderID = 11
            };
            LineItemDisplayDto lineItemDisplay = new LineItemDisplayDto(){
                LineItemID = 1,
                LineItemTotalPrice = "$100",
                LineItemQuantity = 2,
                ProductID = 12,
                OrderID = 11
            };
           
            //Mock
            mockShopiMaxRepo.Setup(x => x.GetLineItem(1)).ReturnsAsync(lineItemObj);
            mockMapperRepo.Setup(mock => mock.Map<LineItemDisplayDto>(It.IsAny<LineItem>())).Returns(lineItemDisplay);
            

            //Result
            IActionResult result = await controllerObj.GetLineItem(1);
            OkObjectResult Objresult = result as OkObjectResult;
            LineItemDisplayDto lineItem = Objresult.Value as LineItemDisplayDto;


            //Assert
            Assert.IsTrue(lineItem.OrderID == 11);
            Assert.IsTrue(lineItem.ProductID == 12);
            
        }

        [Test]
        public async Task EditLineItem_Positive()
        {
            //Arrange
            LineItem lineItemInput = new LineItem(){
                LineItemID = 1,
                LineItemTotalPrice = 100,
                LineItemQuantity = 2,
                ProductID = 12,
                OrderID = 11
            };

            LineItem lineItemUpdated = new LineItem(){
                LineItemID = 1,
                LineItemTotalPrice = 120,
                LineItemQuantity = 2,
                ProductID = 12,
                OrderID = 11
            };


           
            //Mock
            mockShopiMaxRepo.Setup(x => x.EditLineItem(lineItemInput)).ReturnsAsync(lineItemUpdated);
            

            //Result
            IActionResult result = await controllerObj.EditLineItem(lineItemInput);
 
            //Assert
            Assert.AreEqual((int)((StatusCodeResult)result).StatusCode, 201);
            
        }
        [Test]
        public void AddLineItem_Positive()
        {
            //Arrange
            LineItem lineItemInput = new LineItem(){
                LineItemID = 1,
                LineItemTotalPrice = 100,
                LineItemQuantity = 2,
                ProductID = 12,
                OrderID = 11
            };
       
            //Mock
            mockShopiMaxRepo.Setup(x => x.Add(lineItemInput));
            

            //Result
            IActionResult result =  controllerObj.AddLineItem(lineItemInput);
 
            //Assert
            Assert.AreEqual((int)((StatusCodeResult)result).StatusCode, 201);
            
        }
    }
}