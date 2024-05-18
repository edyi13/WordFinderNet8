using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordFinder.Application.Interface.Persistence;

namespace WordFinder.Infrastructure.Persistence.Repositories
{
    internal class UnitOfWork : IUnitOfWork
    {
        public IUserRepository Users { get; }

        public UnitOfWork(IUserRepository users)
        {
            Users = users ?? throw new ArgumentNullException(nameof(users));
        }

        public void Dispose()
        {
            System.GC.SuppressFinalize(this);
        }
    }
}
