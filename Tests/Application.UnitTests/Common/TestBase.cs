using System;
using BG.CampusLife.Persistence;

namespace Application.UnitTests.Common
{
    public class TestBase : IDisposable
    {
        private readonly CampusContext _context;

        public TestBase()
        {
            _context = CampusContextFactory.Create();
        }

        public void Dispose()
        {
            CampusContextFactory.Destroy(_context);
        }
    }
}