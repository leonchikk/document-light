using DocumentLight.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DocumentLight.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<User> UsersRepository { get; }
        IRepository<File> FilesRepository { get; }
        IRepository<Template> TemplatesRepository { get; }
        Task SaveAsync();
    }
}
