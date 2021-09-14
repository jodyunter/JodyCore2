using JodyCore2.Data;
using JodyCore2.Data.Repositories;
using JodyCore2.Domain.Bo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JodyCore2.Test.Xunit.Data.Repositories
{
    public abstract class BaseRepositoryTests<T> where T: class, IBO
    {
        public abstract IBaseRepository<T> SetupRepository();
        public abstract T SetupCreateData(JodyContext context);
        public abstract T SetupUpdateData(T originalData, JodyContext context);
        public abstract IList<T> SetupGetAllData(JodyContext context);
        public abstract IList<T> SetupDeleteData(JodyContext context);

        public IBaseRepository<T> Repository { get; set; }

        protected BaseRepositoryTests()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Integration");
            Environment.SetEnvironmentVariable("CONNECTION_STRING", "DefaultConnectionString");

            using (var context = new JodyContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Repository = SetupRepository();
            }
        }

        protected void Dispose()
        {
            // Do "global" teardown here; Called after every test method.
            using (var context = new JodyContext())
            {
                context.Database.EnsureDeleted();
            }
        }

        [Fact]
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
                var compareData = Repository.WithAllObjects(Repository.GetByIdentifier(createdData.Identifier, context)).FirstOrDefault();
                Assert.Equal(createdData, compareData);
            }

        }

        [Fact]
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
                var currentData = Repository.WithAllObjects(Repository.GetByIdentifier(createdData.Identifier, context)).FirstOrDefault();
                Assert.NotEqual(currentData, createdData);
                Assert.Equal(currentData, updatedData);
            }
        }

        [Fact]
        public void ShouldGetAll()
        {
            var setupData = new List<T>();

            using (var context = new JodyContext())
            {
                setupData = SetupGetAllData(context).ToList();

                context.SaveChanges();
            }

            using (var context = new JodyContext())
            {
                var values = Repository.WithAllObjects(Repository.GetAll(context));

                Assert.StrictEqual(10, values.Count());
                Assert.StrictEqual(context.Set<T>().Count(), values.Count());

                setupData.ForEach(d =>
                {
                    Assert.True(values.Contains(d));
                });

            }

        }

        [Fact]
        public void ShouldDelete()
        {
            IList<T> createdData = null;

            using (var context = new JodyContext())
            {
                createdData = SetupDeleteData(context);

                context.SaveChanges();
            }

            using (var context = new JodyContext())
            {

                Repository.Delete(createdData[0], context);

                context.SaveChanges();
            }

            using (var context = new JodyContext())
            {
                var currentData = Repository.GetAll(context);

                Assert.StrictEqual(createdData.Count - 1, currentData.Count());

                Assert.Null(currentData.Where(g => g.Identifier == createdData[0].Identifier).FirstOrDefault());
            }
        }
    }
}

