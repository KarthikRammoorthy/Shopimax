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
    public class OrderTest
    {
        private Mock<IShopiMaxRepository> mockShopiMaxRepo = null;
        private Mock<IMapper> mockMapperRepo = null;
        private OrderController controllerObj = null;
         
        [SetUp]
        public void Setup()
        {
            mockShopiMaxRepo = new Mock<IShopiMaxRepository>();
            mockMapperRepo = new Mock<IMapper>();
            controllerObj = new OrderController(mockShopiMaxRepo.Object, mockMapperRepo.Object);
        }

        [Test]
        public async Task GetOrders_Positive()
        {
            //Arrange
            List<Order> OrderObjList = new List<Order>();
             List<OrderDisplayDto> OrderList = new List<OrderDisplayDto>();
            Order OrderObj = new Order(){
                OrderID = 1,
                OrderCancelled = false,
                OrderDate = DateTime.Now,
                UserID = 1
                
            };
             OrderDisplayDto Order = new OrderDisplayDto(){
                OrderID = 1,
                OrderCancelled = false,
                OrderDate = DateTime.Now,
                OrderTotal = "$3000",
                UserID = 1
                
            };
            OrderList.Add(Order);
            OrderObjList.Add(OrderObj);
            //Mock
            mockShopiMaxRepo.Setup(x => x.GetOrders()).ReturnsAsync(OrderObjList);
            mockMapperRepo.Setup(mock => mock.Map<IEnumerable<OrderDisplayDto>>(It.IsAny<IEnumerable<Order>>())).Returns(OrderList);
            

            //Result
            IActionResult result = await controllerObj.GetOrders();
            OkObjectResult Objresult = result as OkObjectResult;
            List<OrderDisplayDto> orderDisplay = Objresult.Value as List<OrderDisplayDto>;


            //Assert
            Assert.IsTrue(orderDisplay[0].OrderID == Order.OrderID);
            
        }

        [Test]
        public void  GetOrders_NotAuthorized_Negative()
        {
            
                //Arrange
                List<Order> OrderObjList = new List<Order>();
                Order OrderObj = new Order(){
                    OrderID = 1,
                    OrderCancelled = false,
                    OrderDate = DateTime.Now,
                    UserID = 1
                };
                OrderObjList.Add(OrderObj);

                //Mock
                mockShopiMaxRepo.Setup(x => x.GetOrders()).ThrowsAsync(new AuthenticationException());
                
                //Assert
                Assert.ThrowsAsync<AuthenticationException>(() =>  controllerObj.GetOrders());
            
            
        }
        [Test]
        public async Task GetOrder_Positive()
        {
            //Arrange
            Order OrderObj = new Order(){
                OrderID = 1,
                OrderCancelled = false,
                OrderDate = DateTime.Now,
                UserID = 1
            };
            OrderDisplayDto Order = new OrderDisplayDto(){
                OrderID = 1,
                OrderCancelled = false,
                OrderDate = DateTime.Now,
                OrderTotal = "$3000",
                UserID = 1
                
            };
           
            //Mock
            mockShopiMaxRepo.Setup(x => x.GetOrder(1)).ReturnsAsync(OrderObj);
            mockMapperRepo.Setup(mock => mock.Map<OrderDisplayDto>(It.IsAny<Order>())).Returns(Order);
            

            //Result
            IActionResult result = await controllerObj.GetOrder(1);
            OkObjectResult Objresult = result as OkObjectResult;
            OrderDisplayDto orderDisplay = Objresult.Value as OrderDisplayDto;


            //Assert
            Assert.IsTrue(orderDisplay.OrderID == Order.OrderID);
            Assert.IsTrue(orderDisplay.OrderCancelled == Order.OrderCancelled);
            
            
        }

        [Test]
        public async Task EditOrder_Positive()
        {
            //Arrange
            Order OrderInput = new Order(){
                OrderID = 1,
                OrderCancelled = false,
                OrderDate = DateTime.Now,
                UserID = 1
            };

            Order OrderUpdated = new Order(){
                OrderID = 1,
                OrderCancelled = true,
                OrderDate = DateTime.Now,
                UserID = 1
            };


           
            //Mock
            mockShopiMaxRepo.Setup(x => x.EditOrder(OrderInput)).ReturnsAsync(OrderUpdated);
            

            //Result
            IActionResult result = await controllerObj.EditOrder(OrderInput);
 
            //Assert
            Assert.AreEqual((int)((StatusCodeResult)result).StatusCode, 201);
            
        }
        [Test]
        public void AddOrder_Positive()
        {
            //Arrange
            Order OrderInput = new Order(){
                OrderID = 1,
                OrderCancelled = false,
                OrderDate = DateTime.Now,
                UserID = 1
            };
       
            //Mock
            mockShopiMaxRepo.Setup(x => x.Add(OrderInput));
            

            //Result
            IActionResult result =  controllerObj.AddOrder(OrderInput);
 
            //Assert
            Assert.AreEqual((int)((StatusCodeResult)result).StatusCode, 201);
            
        }
    }
}