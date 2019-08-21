using DocumentLight.Core.Entities;
using DocumentLight.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DocumentLight.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationContext _dbContext;
        
        public IRepository<User> UsersRepository { get; }
        public IRepository<File> FilesRepository { get; }
        public IRepository<Template> TemplatesRepository { get; }

        public UnitOfWork(ApplicationContext dbContext, 
                          IRepository<User> users, 
                          IRepository<File> files,
                          IRepository<Template> templates)
        {
            _dbContext = dbContext;
            UsersRepository = users;
            FilesRepository = files;
            TemplatesRepository = templates;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
