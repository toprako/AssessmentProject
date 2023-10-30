using EntityLayer.Concrete.Common;
using EntityLayer.Concrete.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Communication : BaseEntity
    {
        public CommunicationEnum InformationType { get; set; }
        public string? InformationContent { get; set; }

        public Guid? PersonId { get; set; }
        public Person? Person { get; set; }
    }
}
