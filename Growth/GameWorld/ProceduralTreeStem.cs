using System;
using System.Collections.Generic;
using VectorMath;

namespace GameWorld
{
    internal class ProceduralTreeStem : ProceduralTreeBranch
    {
        public override Vector2 Origin => new Lazy<Vector2>(() => Vector2.Zero).Value;
        public override float Thickness => 3f;

        internal ProceduralTreeStem(Vector2 vector) : base(null, vector)
        {
        }
    }
}