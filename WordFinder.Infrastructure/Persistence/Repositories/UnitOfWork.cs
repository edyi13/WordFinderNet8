using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordFinder.Application.Interface.Persistence;
using WordFinder.Domain.Entities;

namespace WordFinder.Infrastructure.Persistence.Repositories
{
    internal class UnitOfWork : IUnitOfWork
    {
        public IWordFinderRepository WordFinder { get; }

        public UnitOfWork(IWordFinderRepository wordFinder)
        {
            WordFinder = wordFinder;
        }

        public void Dispose()
        {
            System.GC.SuppressFinalize(this);
        }
    }
}
