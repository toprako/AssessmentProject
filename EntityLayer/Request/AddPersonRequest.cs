using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Request
{
    public class AddPersonRequest
    {
        [Required(ErrorMessage = "İsim Alanı Zorunludur.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyadı Alanı Zorunludur.")]
        public string SurName { get; set; }

        public string? Company { get; set; }

        public List<InformationTypeClass>? InformationTypes { get; set; }
    }

    public class InformationTypeClass
    {
        public byte? InformationType { get; set; }
        public string? InformationContent { get; set; }
    }
}
