using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ZephoraWorld;


namespace ZephorasWorld.Classes
{
    public class attack


    {
        public string dmgMessage;
        public int dmgsDone;
        public string damagetoenemy;
        public int dmgtowardsenemy;
        public int level;
        public string levling;
        ArmorClass armor;

        Random dmg = new Random();
        Random dmgs = new Random();
       


        public attack(string message, int dmgDone, int level) 
        {

           // level = Convert.ToInt32(World.charLevels);
           

     

          

            if(level >= 1 && level <= 6)
            {
                armor = new ArmorClass();
                int armors = Convert.ToInt32(armor.ACNumber);
                dmgMessage = message;
                dmgMessage = "You have been hit for" + dmg.Next(0, 10 / armors);

                dmgsDone = dmgDone;
                dmgsDone = dmg.Next(0, 10);
                damagetoenemy = message;
                damagetoenemy = "You have hit the enemy for" + dmg.Next(0, 10);
                dmgtowardsenemy = dmgDone;
                dmgtowardsenemy = dmgs.Next(0, 10);

            }
            else if (level >= 6 && level <= 13)
            {
                armor = new ArmorClass();
            
                int armors = Convert.ToInt32(armor.ACNumber);
                dmgMessage = message;
                dmgMessage = "You have been hit for" + dmg.Next(0, 15 / armors);

                dmgsDone = dmgDone;
                
                dmgsDone =  dmg.Next(0, 15);
                damagetoenemy = message;
                damagetoenemy = "You have hit the enemy for" + dmg.Next(0, 15);
               
                dmgtowardsenemy = dmgDone;
                dmgtowardsenemy = dmgs.Next(0, 20);

            }
            else if (level >= 14 && level <= 21)
            {
                armor = new ArmorClass();
                int armors = Convert.ToInt32(armor.ACNumber);
                dmgMessage = message;
                dmgMessage = "You have been hit for" + dmg.Next(0, 24 / armors);
                dmgsDone = dmgDone;
                dmgsDone = dmg.Next(0, 24);
                damagetoenemy = message;
                damagetoenemy = "You have hit the enemy for" + dmg.Next(0, 35);
                dmgtowardsenemy = dmgDone;
                dmgtowardsenemy = dmgs.Next(0, 35);

            }
            else if (level >= 22 && level <= 36)
            {
                armor = new ArmorClass();
                int armors = Convert.ToInt32(armor.ACNumber);
                dmgMessage = message;
                dmgMessage = "You have been hit for" + dmg.Next(0, 45 / armors);
                dmgsDone = dmgDone;
                dmgsDone = dmg.Next(0, 45);
                damagetoenemy = message;
                damagetoenemy = "You have hit the enemy for" + dmg.Next(0, 68);
                dmgtowardsenemy = dmgDone;
                dmgtowardsenemy = dmgs.Next(0, 68);

            }
            else if (level >= 37 && level <= 50)
            {

                armor = new ArmorClass();
                int armors = Convert.ToInt32(armor.ACNumber);
                dmgMessage = message;
                dmgMessage = "You have been hit for" + dmg.Next(0, 65 / armors);
                    dmgsDone = dmgDone;
                    dmgsDone = dmg.Next(0, 65);
                    damagetoenemy = message;
                    damagetoenemy = "You have hit the enemy for" + dmg.Next(0, 90);
                    dmgtowardsenemy = dmgDone;
                    dmgtowardsenemy = dmgs.Next(0, 90);
                

            }

        }


        
        
    public string damageMessage
        {
            get
            {
                return dmgMessage;
            }
        set
            {
                dmgMessage = value;
            }

        }

     public int damage
    {
         get
        {
            return dmgsDone;
        }
         set
        {
            dmgsDone = value;
        }
    }
      
  
        public string enemydamage
     {
            get
         {
             return damagetoenemy;
         }
            set
         {
             damagetoenemy = value;
         }
     }

    }

        
    

}
    

