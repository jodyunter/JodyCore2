﻿using JodyCore2.Data;
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
    public abstract class TestBaseRepository<T> where T : class, IBaseDto
    {
        public abstract IBaseRepository<T> SetupRepository();
        public abstract T SetupCreateData(JodyContext context);
        public abstract T SetupUpdateData(T originalData, JodyContext context);
        public abstract IList<T> SetupGetAllData(JodyContext context);


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

        [Test]
        public void ShouldCreate()
        {
            T createdData = null;

            using (var context = new JodyContext())
            {
                createdData = SetupCreateData(context);                

                context.SaveChanges();
            }

            using (var context = new JodyContext())
            {
                Repository.Create(createdData, context);

                context.SaveChanges();
            }

            using (var context = new JodyContext())
            {
                var compareData = Repository.GetByIdentifier(createdData.Identifier, context);
                Assert.AreEqual(createdData, compareData);
            }

        }

        [Test]
        public void ShouldUpdate()
        {
            T createdData = null;
            T updatedData = null;

            using (var context = new JodyContext())
            {
                createdData = SetupCreateData(context);
                Repository.Create(createdData, context);

                context.SaveChanges();
            }

            using (var context = new JodyContext())
            {
                updatedData = SetupUpdateData(createdData, context);
                Repository.Update(updatedData, context);

                context.SaveChanges();
            }

            using (var context = new JodyContext())
            {
                var currentData = Repository.GetByIdentifier(createdData.Identifier, context);
                Assert.AreNotEqual(currentData, createdData);
                Assert.AreEqual(currentData, updatedData);
            }
        }

        [Test]
        public void ShouldGetAll()
        {
            using (var context = new JodyContext())
            {
                SetupGetAllData(context);

                context.SaveChanges();
            }

            using (var context = new JodyContext())
            {
                var teams = Repository.GetAll(context);

                Assert.AreEqual(10, teams.Count());
                Assert.AreEqual(context.Teams.Count(), teams.Count());
            }

        }
    }
}
