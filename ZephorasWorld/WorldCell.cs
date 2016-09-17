using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZephorasWorld.Classes;

namespace ZephorasWorld
{
    public class WorldCell
    {
        public string Name { get; set; }
        public string Description { get; set; }

        List<Entity> Occupants { get; set; } = new List<Entity>();

        public int X { get; private set; }
        public int Y { get; private set; }


        public WorldCell(int x, int y)
        {
            Name = "Your House";
            Description = "You wake up with a fuzzy sense";

            X = x;
            Y = y;
        }
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(1024);
            builder.AppendLine($"[{X}, {Y}](You're in{Name}) - {Description}");
            builder.AppendLine($"You See:");
     

            if(Occupants.Count <= 1)
            {
                builder.AppendLine($"-Nothing.");
            }
            else
            {
                foreach(var occupants in Occupants)
                {
                    if (occupants is Player)
                        continue;

                    builder.AppendLine($"-{occupants.Identity(Name)}");
                }
            }
            builder.Clear();
            return builder.ToString();
        }
        public void AddEntity(Entity entity)
        {
            if (entity.Cell != null)
                entity.Cell.RemoveEntity(entity);

            entity.Cell = this;
            Occupants.Add(entity);
            
        }
        public void RemoveEntity(Entity entity)
        {
            Occupants.Remove(entity);
        }

    }
}
