using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZephorasWorld.Classes
{
    class LevelingEntitiy
    {
        public int level;
        public double exp;



        public LevelingEntitiy(int leveling, int experience)
        {
            level = leveling;
            level = 1;
            exp = experience;
            exp = exp * 0.022 + level * level * 2 + 160;

           

    
        }



        public double Experience
        {
            get
            {
                return exp;
            }
            set
            {
                exp = value;
            }
        }
        public int leveling
        {
            get
            {
                return level;
            }
            set
            {
                
                    level = value;
                
            }




        }


    }
}
