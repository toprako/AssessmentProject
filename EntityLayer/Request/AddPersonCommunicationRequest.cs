using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Request
{
    public class AddPersonCommunicationRequest
    {
        public Guid? Id { get; set; }
        public byte? InformationType { get; set; }
        public string? InformationContent { get; set; }
    }
}
