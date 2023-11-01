using DataAccessLayer.Abstract;
using EntityLayer.Concrete.ReportSql;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace AssessmentProject.Report.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public ReportController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        [HttpGet]
        public IActionResult GetAllReportData()
        {
            var report = _repositoryWrapper.ReportRepository.FindAll();
            if (report != null)
            {
                foreach (var item in report)
                {

                }
            }
            return Ok();
        }
    }
}
