using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZephorasWorld.Classes
{
    
    public enum EntityGender {Male, Female, Unknown}
    public enum EntityClass {Magician, Rogue, Cleric, Paladin, Necromancer,Druid, Beastlord,Ranger,Enchanter,Wizard,Warrior, Unknown}


 

    public abstract class Entity
    {

        #region Field Region
        public WorldCell Cell { get; internal set; }
        protected Random rand;
        public abstract string Identity(string name, int health);
        public abstract string Identity(string name);
        public abstract bool IsAlive();
        protected EntityGender _gender;
        protected EntityClass _characterclass;
        protected string _name;
        protected int _strength;
        protected int _dexterity;
        protected int _wisdom;
        public int _health;
        public int expEnterd;
        protected int _strengthModifier, _dexterityModifier, _wisdomModifier, _healthModifier;
        

#endregion

        #region Property Region

        public int experience
        {
            get {
                return expEnterd;
                   }
            set
            {
                expEnterd = value;
            }

        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public EntityGender Gender
        {
            get { return _gender; }
             set { _gender = value; }
        }
        public int Strength
        {
            get { return _strength + _strengthModifier; }
            set { _strength = value; }
        }
        public int Dexterity
        {
            get { return _dexterity + _dexterityModifier; }
            set { _dexterity = value; }
        }
        public int Wisdom
        {
            get { return _wisdom + _wisdomModifier; }
            set { _wisdom = value; }
        }
        public int Health
        {
            get { return _health + _healthModifier; }
             set { _health = value; }
        }

        public EntityClass CharacterClass
        {
            get { return _characterclass; }
            set { _characterclass = value; }
        }
        #endregion
      
        #region Constructors Region

        public Entity()
        {

        }

        public Entity(string name)
        {



            Random rands = new Random();
            Name = name; 
                Gender = EntityGender.Unknown;
                CharacterClass = EntityClass.Unknown; 
                Strength = rands.Next(80, 110);
                Dexterity = rands.Next(85, 100);
                Wisdom = rands.Next(115, 134);
                Health = rands.Next(90, 100);
            expEnterd = 0;

                
                


         


        }
        public Entity(string name, int dex, int str, int wis, int hlt)
        {



            Random rands = new Random();
            Name = name;
            Gender = EntityGender.Unknown;
            CharacterClass = EntityClass.Unknown;
            Strength = str;
            Dexterity = dex;
            Wisdom = wis;
            Health = hlt;
            expEnterd = 0;








        }



        #endregion

        #region Method Region



        #endregion

    }


}

