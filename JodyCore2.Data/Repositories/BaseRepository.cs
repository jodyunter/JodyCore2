using JodyCore2.Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Data.Repositories
{
    public class BaseRepository<T>:IBaseRepository<T> where T: class, IBaseDto
    {
        public T Update(T dto, JodyContext context)
        {
            context.Update(dto);
            return dto;
        }

        public T Create(T dto, JodyContext context)
        {
            context.Add(dto);
            return dto;
        }

        public T GetByIdentifier(Guid identifier, JodyContext context)
        {
            return context.Set<T>().Where(t => t.Identifier == identifier).FirstOrDefault();
        }

        public IQueryable<T> GetAll(JodyContext context)
        {
            return context.Set<T>();
        }
    }
}
