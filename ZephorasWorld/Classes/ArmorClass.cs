using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZephorasWorld.Classes
{
    class ArmorClass
    {

        double ACClass;

        double acNumbers = 10;


        public ArmorClass()
        {




            ACClass = acNumbers;
        }


        public double ACNumber
        {
            get
            {
                return ACClass;
            }
            set
            {
                ACClass = value;
            }
        }


    }
}
