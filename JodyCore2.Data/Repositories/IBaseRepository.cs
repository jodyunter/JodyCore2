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

        T GetByIdentifier(Guid identifier, JodyContext context);
        IList<T> GetAll(JodyContext context);

    }
}