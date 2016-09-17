using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZephorasWorld
{
    public abstract class IEntity
    {
        public WorldCell Cell { get; internal set; }
        public abstract string Identity();
        public abstract string IsAlive();

    }
}
