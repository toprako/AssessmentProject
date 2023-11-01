using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete.ReportSql
{
    public class ReportSqlModel
    {
        public Guid Id { get; set; }
        public List<ReportSqlModelDetail> Detail { get; set; }
    }
    public class ReportSqlModelDetail
    {
        public string InformationContent { get; set; }
        public double PersonCount { get; set; }
        public double PhoneCount { get; set; }

    }
}
