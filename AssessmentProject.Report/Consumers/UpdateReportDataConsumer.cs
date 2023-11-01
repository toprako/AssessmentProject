using DataAccessLayer.Abstract;
using EntityLayer.Concrete.Enum;
using EntityLayer.Concrete.ReportSql;
using MassTransit;
using System.Text.Json;

namespace AssessmentProject.Report.Consumers
{
    public class UpdateReportDataConsumer : IConsumer<ReportSqlModel>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public UpdateReportDataConsumer(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        public async Task Consume(ConsumeContext<ReportSqlModel> context)
        {
            if (context is not null && context.Message is not null)
            {
                var reportModel = context.Message;
                var report = _repositoryWrapper.ReportRepository.GetById(reportModel.Id);
                if (report != null)
                {
                    report.ReportJson = JsonSerializer.Serialize(reportModel.Detail);
                    report.ReportStatus = ReportStatusEnum.Tamamlandı.ToString();
                    _repositoryWrapper.ReportRepository.Update(report);
                    _repositoryWrapper.Save();
                }               
            }
        }
    }
}
