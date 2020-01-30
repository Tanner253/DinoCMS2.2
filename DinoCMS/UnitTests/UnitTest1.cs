using DinoCMS.Data;
using DinoCMS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace UnitTests
{
    public class UnitTest1
    {
        // [Fact]
        //public void CanCreateDatabase()
        //{
        //    DbContextOptions<DinoDbContext> options = new DbContextOptionsBuilder<DinoDbContext>().UseInMemoryDatabase("DinoDb");
        //}



        [Fact]
        public void DinosaurPropID()
        {
            Dinosaur dino = new Dinosaur();
            dino.ID = 556;
            var result = dino.ID;

            Assert.Equal(556, result);
        }

        [Fact]
        public void DinosaurPropName()
        {
            Dinosaur dino = new Dinosaur();
            dino.Name = "test";
            var result = dino.Name;

            Assert.Equal("test", result);
        }
    }
}
