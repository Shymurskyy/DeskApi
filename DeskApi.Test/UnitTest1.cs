using DeskApi.Data;
using DeskApi.Models;
using DeskApi.Services.DeskService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DeskApi.Test
{
    public class UnitTest1
    {
        [Fact]
        public void GetAllDesks_ReturnsAllDesks()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using var context = new ApplicationDbContext(options);
            context.Desks.Add(new Desk { DeskId = 1, DeskName = "Desk 1", IsAvailable = true });
            context.Desks.Add(new Desk { DeskId = 2, DeskName = "Desk 2", IsAvailable = false });
            context.SaveChanges();

            var service = new DeskService(context);

            // Act
            var desks = service.GetAllDesks();

            // Assert
            Assert.Equal(2, desks.Count());
        }
    }
}