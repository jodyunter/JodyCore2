using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Data.Dto
{
    public interface IBaseDto
    {
        public int Id { get; set; }
        public Guid Identifier { get; set; }
    }
}
