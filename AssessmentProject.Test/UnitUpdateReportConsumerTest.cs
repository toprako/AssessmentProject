using AssessmentProject.Report.Consumers;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete.ReportSql;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentProject.Test
{
    public class UnitUpdateReportConsumerTest
    {
        #region Variable
        private readonly UpdateReportDataConsumer updateReportData;
        #endregion

        public UnitUpdateReportConsumerTest()
        {
            var service = A.Fake<IRepositoryWrapper>();
            updateReportData = new UpdateReportDataConsumer(service);
        }

        [Fact]
        public async Task Consume_ReturnOKAsync()
        {
            var fakeConsume = A.Fake<MassTransit.ConsumeContext<ReportSqlModel>>();
            await updateReportData.Consume(fakeConsume);
        }
    }
}
