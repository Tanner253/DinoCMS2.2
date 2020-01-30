using DinoCMS.Data;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace UnitTests
{
    public class UnitTest1
    {
       // [Fact]
        public void CanCreateDatabase()
        {
            DbContextOptions<DinoDbContext> options = new DbContextOptionsBuilder<DinoDbContext>().UseInMemoryDatabase("DinoDb");
        }
    }
}
