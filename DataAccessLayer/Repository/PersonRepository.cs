using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(RepositoryContext context) : base(context)
        {
        }

        public Person GetByIdIncludeCommunication(Guid Id)
        {
            return _context.Persons.Include(x => x.Communications).Where(x => x.Id == Id).FirstOrDefault();
        }
    }
}
