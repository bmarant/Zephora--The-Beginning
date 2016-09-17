using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZephorasWorld.Classes
{
    class BagItems
    {


        string itemNames;
        int counts = 0;

      


        public string items
        {
            get
            {
                return itemNames;
            }
            set
            {
                itemNames = value;
            }

        }
        public int count
        {

            get
            {
                return counts;
            }
            set
            {
                counts = value;
            }

        }


        }
    }
