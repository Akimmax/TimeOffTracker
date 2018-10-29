using System;
using System.Collections.Generic;
using System.Text;

namespace TOT.Interfaces
{
    public interface IMapper
    {
        TDest Map<TSrc, TDest>(TSrc src);
        void Map<TSrc, TDest>(TSrc src, TDest dest);
    }
}
