using System.Collections.Generic;
using VectorMath;

namespace GameWorld
{
    internal class ProceduralTreeStem : ProceduralTreeBranch
    {
        public override float Thickness => 3f;

        internal ProceduralTreeStem(Vector2 vector) : base(null, vector)
        {
        }
    }
}