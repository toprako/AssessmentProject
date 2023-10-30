using EntityLayer.Concrete.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Person : BaseEntity
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string? Company { get; set; }

        public virtual ICollection<Communication>? Communications{ get; set; }
    }
}
