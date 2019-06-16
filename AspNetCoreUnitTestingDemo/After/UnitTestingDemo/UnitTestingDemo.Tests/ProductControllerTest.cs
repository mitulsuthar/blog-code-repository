using AutoFixture;
using System;
using UnitTestingDemo.Controllers;
using UnitTestingDemo.Services;
using Xunit;
using FakeItEasy;
using UnitTestingDemo.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace UnitTestingDemo.Tests
{
    public class ProductControllerTest
    {
        //Fakes
        private readonly IProductService _productService;
        private readonly IMyLogger _logger;

        //Dummy Data Generator
        private readonly Fixture _fixture;
        
        //System under test
        private readonly ProductsController _sut;
        public ProductControllerTest()
        {
            _productService = A.Fake<IProductService>();
            _logger = A.Fake<IMyLogger>();
            _sut = new ProductsController(_productService,_logger);

            _fixture = new Fixture();
        }

        [Fact]
        public void Get_WhenThereAreProducts_ShouldReturnActionResultOfProductsWith200StatusCode()
        {
            //Arrange
            var products = _fixture.CreateMany<Product>(3).ToList();
            A.CallTo(() => _productService.GetProducts()).Returns(products);

            //Act
            var result = _sut.Get() as OkObjectResult;

            //Assert
            A.CallTo(() => _productService.GetProducts()).MustHaveHappenedOnceExactly();
            Assert.NotNull(result);
            var returnValue = Assert.IsType<List<Product>>(result.Value);
            Assert.Equal(products.Count, returnValue.Count());
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }

        [Fact]
        public void Get_WhenThereIsUnhandledException_ShouldReturn500StatusCodeAndLogAnException()
        {
            //Arrange
            A.CallTo(() => _productService.GetProducts()).Throws<Exception>();
            A.CallTo(() => _logger.Log(A<string>._, A<Exception>._)).DoesNothing();

            //Act
            var result = _sut.Get() as StatusCodeResult;

            //Assert
            A.CallTo(() => _productService.GetProducts()).MustHaveHappenedOnceExactly();
            A.CallTo(() => _logger.Log(A<string>._, A<Exception>._)).MustHaveHappenedOnceExactly();
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        [Fact]
        public void Get_WhenThereAreNoProductsFound_ShouldReturn404NotFoundResult()
        {
            //Arrange
            var products = new List<Product>();
            A.CallTo(() => _productService.GetProducts()).Returns(products);

            //Act
            var result = _sut.Get() as NotFoundResult;

            //Assert
            A.CallTo(() => _productService.GetProducts()).MustHaveHappenedOnceExactly();
            Assert.NotNull(result);                        
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);

        }
    }
}
