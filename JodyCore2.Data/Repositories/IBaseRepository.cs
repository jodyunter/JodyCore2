using JodyCore2.Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Data.Repositories
{
    public interface IBaseRepository<T> where T : class, IBaseDto
    {
        public T Update(T dto, JodyContext context);
        public T Create(T dto, JodyContext context);
        public IList<T> Update(IList<T> dtos, JodyContext context);
        public IList<T> Create(IList<T> dtos, JodyContext context);
        public void Delete(T dto, JodyContext context);
        public void Delete(IList<T> dto, JodyContext context);
        IQueryable<T> GetByIdentifier(Guid identifier, JodyContext context);
        IQueryable<T> GetAll(JodyContext context);
        IQueryable<T> WithAllObjects(IQueryable<T> query);

    }
}