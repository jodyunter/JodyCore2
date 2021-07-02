using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Service.Util
{
    public static class RandomUtility
    {
        public static Random GetRandom()
        {
            return new Random();
        }

        public static Random GetRandom(int seed)
        {
            return new Random(seed);
        }
    }
}
