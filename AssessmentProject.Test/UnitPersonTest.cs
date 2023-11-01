using AssessmentProject.Persons.Controllers;
using Castle.Core.Logging;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete.Enum;
using EntityLayer.Request;
using EntityLayer.Response.Common;
using FakeItEasy;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace AssessmentProject.Test
{
    public class UnitPersonTest
    {
        #region Variable
        private readonly PersonController _personController;
        #endregion

        public UnitPersonTest()
        {
            var service = A.Fake<IRepositoryWrapper>();
            var logger = A.Fake<ILogger<PersonController>>();
            var publish = A.Fake<IPublishEndpoint>();
            _personController = new PersonController(service, logger, publish);
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

        [Fact]
        public void DeletePersonTest_ReturnErrorResponse()
        {
            //Arrange
            var person = new DeletePersonRequest()
            {
                Id = null
            };

            //Act
            var deleteResponse = _personController.DeletePerson(person);

            //Assert
            Assert.NotNull(deleteResponse.Message);
        }

        [Fact]
        public void DeletePersonTest_ReturnOkResponse()
        {
            //Arrange
            var person = new DeletePersonRequest()
            {
                Id = Guid.NewGuid()
            };

            //Act
            var deleteResponse = _personController.DeletePerson(person);

            //Assert
            Assert.True(deleteResponse.Status);
        }

        [Fact]
        public void AddPersonCommunication_ReturnErrorResponse()
        {
            //Arrange
            var communication = new AddPersonCommunicationRequest()
            {
                Id = null,
                InformationContent = "Ýstanbul",
                InformationType = (byte)CommunicationEnum.Localion
            };

            //Act
            var addResponse = _personController.AddPersonCommunication(communication);

            //Assert
            Assert.False(addResponse.Status);
        }

        [Fact]
        public void AddPersonCommunication_ReturnOkResponse()
        {
            //Arrange
            var communication = new AddPersonCommunicationRequest()
            {
                Id = Guid.NewGuid(),
                InformationContent = "Ýstanbul",
                InformationType = (byte)CommunicationEnum.Localion
            };

            //Act
            var addResponse = _personController.AddPersonCommunication(communication);

            //Assert
            Assert.True(addResponse.Status);
        }

        [Fact]
        public void DeletePersonCommunication_ReturnErrorResponse()
        {
            //Arrange
            var communication = new DeletePersonCommunicationRequest()
            {
                Id = Guid.NewGuid(),
                CommunicationId = null
            };

            //Act
            var deleteResponse = _personController.DeletePersonCommunication(communication);

            //Assert
            Assert.False(deleteResponse.Status);
            Assert.NotNull(deleteResponse.Message);
        }

        [Fact]
        public void DeletePersonCommunication_ReturnOkResponse()
        {
            //Arrange
            var communication = new DeletePersonCommunicationRequest()
            {
                Id = Guid.NewGuid(),
                CommunicationId = Guid.NewGuid()
            };

            //Act
            var deleteResponse = _personController.DeletePersonCommunication(communication);

            //Assert
            Assert.True(deleteResponse.Status);
            Assert.Equal(string.Empty, deleteResponse.Message);
        }

        [Fact]
        public void GetByPerson_ReturnOkResponse()
        {
            //Arrange 
            string id = Guid.NewGuid().ToString();

            //Act
            var GetByPersonResponse = _personController.GetByPerson(id);

            //Assert
            Assert.True(GetByPersonResponse.Status);
            Assert.Equal(string.Empty, GetByPersonResponse.Message);
        }


        [Fact]
        public void GetByPerson_ReturnErrorResponse()
        {
            //Arrange 
            string id = "";

            //Act
            var GetByPersonResponse = _personController.GetByPerson(id);

            //Assert
            Assert.False(GetByPersonResponse.Status);
            Assert.NotEqual(string.Empty, GetByPersonResponse.Message);
        }

    }
}