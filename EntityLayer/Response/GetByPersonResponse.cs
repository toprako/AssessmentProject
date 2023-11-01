using EntityLayer.Response.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Response
{
    public class GetByPersonResponse : BaseResponse
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Company { get; set; }
        public List<GetByPersonInformationType> InformationTypes { get; set; }
    }
    public class GetByPersonInformationType
    {
        public Guid Id { get; set; }
        public byte? InformationType { get; set; }
        public string? InformationContent { get; set; }
    }
}
