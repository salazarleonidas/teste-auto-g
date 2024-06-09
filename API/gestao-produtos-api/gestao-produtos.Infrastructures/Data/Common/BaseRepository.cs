using gestao_produtos.Infrastructures.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;

namespace gestao_produtos.Infrastructures.Data.Common
{
    internal class BaseRepository<TEntity> : IDisposable where TEntity : class
    {
        private bool _disposed;

        protected readonly GestaoProdutosContext _context;
        protected readonly DbSet<TEntity> DbSet;

        protected BaseRepository(GestaoProdutosContext context)
        {
            _context = context;
            DbSet = _context.Set<TEntity>();
        }

        ~BaseRepository() => Dispose(false);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
                _context.Dispose();

            _disposed = true;
        }
    }
}
