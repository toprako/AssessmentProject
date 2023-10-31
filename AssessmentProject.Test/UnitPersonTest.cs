using AssessmentProject.Persons.Controllers;
using DataAccessLayer.Abstract;
using EntityLayer.Request;
using EntityLayer.Response.Common;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AssessmentProject.Test
{
    public class UnitPersonTest
    {
        private readonly PersonController _personController;

        public UnitPersonTest()
        {
            var service = A.Fake<IRepositoryWrapper>();
            _personController = new PersonController(service);
        }

        [Fact]
        public void GetAllPerson_ReturnOK()
        {
            //Act
            var okResult = _personController.GetAll();

            //Assert
            Assert.IsType<ObjectResult>(okResult);
        }


        [Fact]
        public void AddPersonTest_ReturnCreatedResponse()
        {
            //Arrange 
            var person = new AddPersonRequest()
            {
                Name = "Test",
                SurName = "Test"
            };

            //Act
            var _createdResponse = _personController.AddPerson(person);

            //Assert
            Assert.IsType<BaseResponse>(_createdResponse);
        }

        [Fact]
        public void AddPersonTest_ReturnErrorResponse()
        {
            //Arrange 
            var person = new AddPersonRequest()
            {
                Name = null,
                SurName = "Test"
            };

            //Act
            var _createdResponse = _personController.AddPerson(person);

            //Assert
            Assert.NotNull(_createdResponse.Message);
        }


    }
}