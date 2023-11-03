using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentProject.Test
{
    public class UnitPersonGenericRepositoryTest
    {
        #region Variable
        private readonly GenericRepository<Person> genericRepository;
        private readonly RepositoryWrapper repositoryWrapper;
        #endregion

        public UnitPersonGenericRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<RepositoryContext>()
            .UseInMemoryDatabase(databaseName: "FakeDatabase")
            .Options;
            RepositoryContext context = new RepositoryContext(options);
            genericRepository = new GenericRepository<Person>(context);
            repositoryWrapper = new RepositoryWrapper(context);
        }


        [Fact]
        public void Delete_ReturnEmptyMessage()
        { 
            //Arrange 
            Person person = new Person()
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                SurName = "Test",
            };
            var responseSave = genericRepository.Insert(person);
            repositoryWrapper.Save();

            //Act
            var response = genericRepository.Delete(person);
            repositoryWrapper.Save();

            //Assert
            Assert.Equal(string.Empty, response);
        }

        [Fact]
        public void Insert_ReturnErrorMessage()
        { 
            //Arrange 
            Person person = new Person()
            {
                Id = Guid.NewGuid(),
                SurName = "b"
            };

            //Act
            var response = genericRepository.Insert(person);

            //Assert
            Assert.Throws<DbUpdateException>(() => repositoryWrapper.Save());
        }

        [Fact]
        public void Insert_ReturnOkMessage()
        {
            //Arrange 
            Person person = new Person()
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                SurName = "Test",
            };

            //Act
            var response = genericRepository.Insert(person);
            repositoryWrapper.Save();

            //Assert
            Assert.Equal(string.Empty, response);
        }

        [Fact]
        public void Update_ReturnOkMessage()
        { 
            //Arrange 
            Person person = new Person()
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                SurName = "Test",
            };
            var responseSave = genericRepository.Insert(person);
            repositoryWrapper.Save();
            person.SurName = "AABB";

            //Act
            var response = genericRepository.Update(person);
            repositoryWrapper.Save();

            //Assert
            Assert.Equal(string.Empty, response);
        }

        [Fact]
        public void GetById_ReturnOkMessage()
        {
            //Arrange 
            Person person = new Person()
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                SurName = "Test",
            };
            var responseSave = genericRepository.Insert(person);
            repositoryWrapper.Save();

            //Act
            var response = genericRepository.GetById(person.Id);

            //Assert
            Assert.NotNull(response);
        }
    }
}
