using JodyCore2.Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JodyCore2.Data.Repositories
{
    //remember to control when you call something like ToList.  This is when a query will execute
    public class BaseRepository<T>:IBaseRepository<T> where T: class, IBaseDto
    {

        public virtual IQueryable<T> AlwaysInclude(IQueryable<T> query)
        {
            return query;
        }

        public virtual T Update(T dto, JodyContext context)
        {
            context.Update(dto);
            return dto;
        }
        public virtual IList<T> Update(IList<T> dto, JodyContext context)
        {
            context.UpdateRange(dto);
            return dto;
        }

        public virtual T Create(T dto, JodyContext context)
        {
            context.Add(dto);
            return dto;
        }
        public virtual IList<T> Create(IList<T> dto, JodyContext context)
        {
            context.AddRange(dto);
            return dto;
        }

        public virtual void Delete(T dto, JodyContext context)
        {
            context.Remove(dto);
            return;
        }

        public virtual void Delete(IList<T> dtos, JodyContext context)
        {
            context.RemoveRange(dtos);
        }

        public virtual IQueryable<T> GetByIdentifier(Guid identifier, JodyContext context)
        {
            return AlwaysInclude(context.Set<T>().Where(t => t.Identifier == identifier));
        }

        //this can be used to build your own queries.        
        public virtual IQueryable<T> GetAll(JodyContext context)
        {
            return AlwaysInclude(context.Set<T>());
        }
    }
}
