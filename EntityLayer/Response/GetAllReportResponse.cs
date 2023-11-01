using EntityLayer.Concrete.ReportSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Response
{
    public class GetAllReportResponse
    {
        public List<GetAllReportClass> Reports { get; set; }
    }
    public class GetAllReportClass
    {
        public Guid Id { get; set; }
        public DateTime? RequestDate { get; set; }
        public string ReportStatus { get; set; }
        public List<ReportSqlModelDetail> ReportDetail { get; set; }
    }
}
