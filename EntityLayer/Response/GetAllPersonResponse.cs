using EntityLayer.Response.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Response
{
    public class GetAllPersonResponse
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Company { get; set; }
        public List<InformationTypeClass> InformationTypes { get; set; }
    }
    public class InformationTypeClass
    {
        public Guid Id { get; set; }
        public byte? InformationType { get; set; }
        public string? InformationContent { get; set; }
    }
}
