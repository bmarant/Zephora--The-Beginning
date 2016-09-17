using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZephorasWorld.Classes
{

    public enum enemy { bat,wolf,beetle, scarab, drachnid,  unknown }
   
    public abstract class Enemy
    {

     

        protected enemy _enemy;
    
        protected int health;

        Random EnemyHp = new Random();

    


        public enemy enemys
        {
            get
            {


                return _enemy;
            }
            set
            {
               

                _enemy = value;
            }
        }

        
        public int enemyHealth
        {
            get
            {
                return health;


            }
            set
            {



             

                health = value;
            }
        }


       public Enemy()
        {

           

            

            enemys = enemy.bat;

       
           
            
           
        }
        
    }
   
}
