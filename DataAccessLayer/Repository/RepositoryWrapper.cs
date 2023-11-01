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
        private readonly RepositoryContext _context;
        private IPersonRepository _personRepository;
        private ICommunicationRepository _communicationRepository;
        private IReportRepository _reportRepository;

        public RepositoryWrapper(RepositoryContext context) => _context = context;

        public IPersonRepository PersonRepository => _personRepository ??= new PersonRepository(_context);

        public ICommunicationRepository CommunicationRepository => _communicationRepository ??= new CommunicationRepository(_context);

        public IReportRepository ReportRepository => _reportRepository ??= new ReportRepository(_context);

        public void Dispose()
        {

        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
