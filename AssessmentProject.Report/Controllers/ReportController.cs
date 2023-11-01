using DataAccessLayer.Abstract;
using EntityLayer.Concrete.ReportSql;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using EntityLayer.Response;
using System.Linq.Expressions;

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
                            ReportStatus = item.ReportStatus ?? string.Empty,
                            RequestDate = item.RequestDate,
                            ReportDetail = !string.IsNullOrEmpty(item.ReportJson) ? JsonSerializer.Deserialize<List<ReportSqlModelDetail>>(item.ReportJson) ?? new List<ReportSqlModelDetail>() : new List<ReportSqlModelDetail>()
                        }); ;
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
        public GetReportResponse GetReport(string Id)
        {
            GetReportResponse response = new()
            {
                Message = string.Empty,
                Status = true
            };
            try
            {
                Guid id = Guid.Empty;
                if (string.IsNullOrEmpty(Id) || !Guid.TryParse(Id, out id))
                {
                    _logger.LogInformation("Id Alanı Boş");
                    throw new ArgumentNullException("Id Alanı Boş");
                }
                var report = _repositoryWrapper.ReportRepository.GetById(id);
                if (report is null)
                {
                    throw new ArgumentNullException("Rapor Bulunamadı");
                }
                response.Id = report.Id;
                response.ReportStatus = report.ReportStatus ?? string.Empty;
                response.RequestDate = report.RequestDate;
                response.ReportDetail = !string.IsNullOrEmpty(report.ReportJson) ? JsonSerializer.Deserialize<List<ReportSqlModelDetail>>(report.ReportJson) ?? new List<ReportSqlModelDetail>() : new List<ReportSqlModelDetail>();
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
