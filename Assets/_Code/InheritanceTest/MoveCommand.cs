using UnityEngine;

namespace _Code.InheritanceTest
{
    public class MoveCommand : Command
    {
        public MoveCommand(IEntity entity, Vector3 direction) : base(entity)
        {
            
        }
    }
}