using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZephorasWorld.Classes
{
   public class Player : Entity
    {

       

        Random rand = new Random();
        
        public Player() : base()
            
        {

        }
        public Player(string name, EntityGender eGender, EntityClass eClass) 
            :base(name)
        {
            Name = name;
            Gender = eGender;
           CharacterClass = eClass;
        }

        public Player(string name, int dexterity, int strength, int wisdom, int health) : base(name, health, strength, wisdom, dexterity)
        {
            Name = name;
            Strength = strength;
            Wisdom = wisdom;
            Dexterity = dexterity;
            Health = health;
            Identity(name, health);
        }

        public Player(string name, EntityGender eGender, EntityClass eClass, int dexterity, int strength, 
            int wisdom, int health) 
            : base(name)
        {
            Name = name;
            Gender = eGender;
            CharacterClass = eClass;
            Dexterity = dexterity;
            Strength = strength;
            Health = health;
            Wisdom = wisdom;
        }

        public override string Identity(string name)
        {
            return $"{name}";
        }

        public override string Identity(string name, int health)
        {

          return $"{name}, he has {health / 2} Health";
        }

        public override bool IsAlive()
        {
           
            return Health > 0 ? true : false;
        }
    }
}
