using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IRepositoryWrapper : IDisposable
    {
        IPersonRepository PersonRepository { get; }
        ICommunicationRepository CommunicationRepository { get; }
        IReportRepository ReportRepository { get; }
        void Save();
    }
}
