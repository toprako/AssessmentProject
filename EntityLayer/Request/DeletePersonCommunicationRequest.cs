using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Request
{
    public class DeletePersonCommunicationRequest
    {
        public Guid? Id { get; set; }
        public Guid? CommunicationId { get; set; }
    }
}
