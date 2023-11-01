using EntityLayer.Concrete.ReportSql;
using EntityLayer.Response.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Response
{
    public class GetReportResponse : BaseResponse
    {
        public Guid Id { get; set; }
        public DateTime? RequestDate { get; set; }
        public string ReportStatus { get; set; }
        public List<ReportSqlModelDetail> ReportDetail { get; set; }
    }
}
