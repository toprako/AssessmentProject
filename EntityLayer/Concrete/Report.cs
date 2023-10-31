using EntityLayer.Concrete.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Report : BaseEntity
    {
        public DateTime? RequestDate { get; set; }
        public string? ReportStatus { get; set; }
        public string? ReportJson { get; set; }
    }
}
