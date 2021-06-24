using JodyCore2.Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Data.Repositories
{
    public class BaseRepository<T>
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

    }
}
