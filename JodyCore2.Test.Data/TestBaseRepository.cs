using JodyCore2.Data;
using JodyCore2.Data.Dto;
using JodyCore2.Data.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Test.Data
{
    public abstract class TestBaseRepository<T> where T: class, IBaseDto
    {
        public abstract IBaseRepository<T> SetupRepository();        

        public IBaseRepository<T> Repository { get; set; }

        [SetUp]
        public void Setup()
        {
            using (var context = new JodyContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repository = SetupRepository();
            }
        }

        [TearDown]
        public void TearDown()
        {
            using (var context = new JodyContext())
            {
                context.Database.EnsureDeleted();
            }
        }
    }
}
