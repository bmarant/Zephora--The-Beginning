using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZephoraWorld.Classes
{

    public enum Movements
    {
        East,
        West,
        North,
        South
    };


    class StateMachineMovements
    {

        class StateTransition
        {
            readonly Movements movements;


            public StateTransition(Movements movingstatements)
            {
                movements = movingstatements;

            
            }
            public override int GetHashCode()
            {
                return 17 + 32 * movements.GetHashCode();
            }



            public override bool Equals(object obj)
            {
                StateTransition other = obj as StateTransition;
                return other != null && this.movements == other.movements;
            }
        }
  




    }
}
