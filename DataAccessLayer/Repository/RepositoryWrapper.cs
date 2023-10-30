using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _context;
        private IPersonRepository _personRepository;
        private ICommunicationRepository _communicationRepository;

        public RepositoryWrapper(RepositoryContext context)
        {
            _context = context;
        }

        public IPersonRepository PersonRepository => _personRepository ??= new PersonRepository(_context);

        public ICommunicationRepository CommunicationRepository => _communicationRepository ??= new CommunicationRepository(_context);

        public void Save()
        {
            _context.SaveChanges(); 
        }
    }
}
