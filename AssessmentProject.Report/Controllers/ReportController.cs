using DataAccessLayer.Abstract;
using EntityLayer.Concrete.ReportSql;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using EntityLayer.Response;

namespace AssessmentProject.Report.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ReportController : ControllerBase
    {

        #region Varaible
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILogger<ReportController> _logger;
        #endregion

        public ReportController(IRepositoryWrapper repositoryWrapper, ILogger<ReportController> logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        /// <summary>
        /// Tüm Raporları Çekmek İçin Oluşturulan Fonksiyon
        /// </summary>
        [HttpGet]
        public GetAllReportResponse GetAllReportData()
        {
            GetAllReportResponse response = new()
            {
                Reports = new()
            };
            try
            {
                var reports = _repositoryWrapper.ReportRepository.FindAll();
                if (reports != null)
                {
                    foreach (var item in reports)
                    {
                        response.Reports.Add(new()
                        {
                            Id = item.Id,
                            ReportStatus = item.ReportStatus,
                            RequestDate = item.RequestDate,
                            ReportDetail = JsonSerializer.Deserialize<List<ReportSqlModelDetail>>(item.ReportJson)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return response;
        }

        /// <summary>
        /// İlgili Rapor ID sine Göre Raporu ve Rapor Detayını Getirmektedir.
        /// </summary>
        [HttpGet]
        public GetReportResponse GetReport(Guid? Id)
        {
            GetReportResponse response = new()
            {
                Message = string.Empty,
                Status = true
            };
            try
            {
                if (Id is null || Id == Guid.Empty)
                {
                    _logger.LogInformation("Id Alanı Boş");
                    throw new ArgumentNullException("Id Alanı Boş");
                }
                var report = _repositoryWrapper.ReportRepository.GetById(Id ?? Guid.Empty);
                if (report is null)
                {
                    throw new ArgumentNullException("Rapor Bulunamadı");
                }
                response.Id = report.Id;
                response.ReportStatus = report.ReportStatus;
                response.RequestDate = report.RequestDate;
                response.ReportDetail = JsonSerializer.Deserialize<List<ReportSqlModelDetail>>(report.ReportJson);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                _logger.LogError(ex, ex.Message);
            }
            return response;
        }

    }
}
