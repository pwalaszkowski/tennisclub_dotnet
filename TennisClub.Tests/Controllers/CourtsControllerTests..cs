using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using tennisclub.Controllers;
using tennisclub.Controllers.Data;
using tennisclub.Models;
using Xunit;

namespace tennisclub.Tests
{
    public class CourtsControllerTests
    {
        private readonly Mock<ApplicationDbContext> _mockContext;
        private readonly CourtsController _controller;

        public CourtsControllerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var context = new ApplicationDbContext(options);
            _mockContext = new Mock<ApplicationDbContext>(options);
            _controller = new CourtsController(context);
        }

        // TODO: Something is not working here correctly
        //[Fact]
        //public async Task Index_ReturnsViewWithCourts()
        //{
        //    // Arrange
        //    var courts = new List<Court>
        //    {
        //        new Court { Name = "Court 1", Location = "Location A", Surface = SurfaceType.Clay },
        //        new Court { Name = "Court 2", Location = "Location B", Surface = SurfaceType.Grass }
        //    };
        //    _mockContext.Setup(c => c.Courts).Returns(GetMockDbSet(courts).Object);

        //    // Act
        //    var result = await _controller.Index();

        //    // Assert
        //    var viewResult = Assert.IsType<ViewResult>(result);
        //    var model = Assert.IsAssignableFrom<IEnumerable<Court>>(viewResult.ViewData.Model);
        //    Assert.Equal(2, model.Count());
        //}

        [Fact]
        public void Create_Get_ReturnsView()
        {
            // Act
            var result = _controller.Create();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Create_Post_ValidModel_RedirectsToIndex()
        {
            // Arrange
            var court = new Court { Name = "Court 3", Location = "Location C", Surface = SurfaceType.Hard };

            // Act
            var result = await _controller.Create(court);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Create_Post_InvalidModel_ReturnsViewWithModel()
        {
            // Arrange
            _controller.ModelState.AddModelError("Name", "Required");
            var court = new Court { Location = "Location D", Surface = SurfaceType.Clay }; // Missing Name

            // Act
            var result = await _controller.Create(court);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(court, viewResult.Model);
        }

        private Mock<DbSet<T>> GetMockDbSet<T>(IEnumerable<T> data) where T : class
        {
            var queryableData = data.AsQueryable();
            var mockSet = new Mock<DbSet<T>>();

            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryableData.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryableData.GetEnumerator());

            mockSet.As<IAsyncEnumerable<T>>()
                   .Setup(m => m.GetAsyncEnumerator(It.IsAny<System.Threading.CancellationToken>()))
                   .Returns(new TestAsyncEnumerator<T>(queryableData.GetEnumerator()));

            return mockSet;
        }

        private class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
        {
            private readonly IEnumerator<T> _inner;

            public TestAsyncEnumerator(IEnumerator<T> inner)
            {
                _inner = inner;
            }

            public ValueTask DisposeAsync() => new ValueTask();

            public ValueTask<bool> MoveNextAsync()
            {
                return new ValueTask<bool>(_inner.MoveNext());
            }

            public T Current => _inner.Current;
        }
    }
}
