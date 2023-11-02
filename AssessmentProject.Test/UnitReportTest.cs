using AssessmentProject.Persons.Controllers;
using AssessmentProject.Report.Controllers;
using DataAccessLayer.Abstract;
using EntityLayer.Response;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentProject.Test
{
    public class UnitReportTest
    {
        #region Variable
        private readonly ReportController _controller;
        #endregion

        public UnitReportTest()
        {
            var service = A.Fake<IRepositoryWrapper>();
            var logger = A.Fake<ILogger<ReportController>>();
            _controller = new ReportController(service, logger);
        }

        [Fact]
        public void GetAllReportData_ReturnOkResponse()
        {
            //Act
            var okResult = _controller.GetAllReportData();

            //Assert
            Assert.IsType<GetAllReportResponse>(okResult);
        }

        [Fact]
        public void GetAllReportData_ReturnEmptyListResponse()
        {
            //Act
            var okResult = _controller.GetAllReportData();

            //Assert
            Assert.Empty(okResult.Reports);
        }

        [Fact]
        public void GetReportData_ReturnOkResponse()
        {
            //Arrange 
            string id = Guid.NewGuid().ToString();

            //Act
            var GetReportResponse = _controller.GetReport(id);

            //Assert
            Assert.True(GetReportResponse.Status);
            Assert.Equal(string.Empty, GetReportResponse.Message);
        }

        [Fact]
        public void GetReportData_ReturnErrorResponse()
        {
            //Arrange 
            string id = "";

            //Act
            var GetReportResponse = _controller.GetReport(id);

            //Assert
            Assert.False(GetReportResponse.Status);
            Assert.NotEqual(string.Empty, GetReportResponse.Message);
        }

        [Fact]
        public void GetReportData_ReturnNotGuidIdResponse()
        {
            //Arrange 
            string id = "test";

            //Act
            var GetReportResponse = _controller.GetReport(id);

            //Assert
            Assert.False(GetReportResponse.Status);
            Assert.NotEqual(string.Empty, GetReportResponse.Message);
        }
    }
}
