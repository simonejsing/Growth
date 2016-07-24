using System;
using System.Collections.Generic;
using VectorMath;

namespace GameWorld
{
    public class ProceduralTreeBranch
    {
        public const float GrowthFreedom = (float)(2 * Math.PI / 8);
        public const float SubbranchFreedom = (float)(Math.PI / 4);

        private List<ProceduralTreeBranch> branchList = new List<ProceduralTreeBranch>();

        public IEnumerable<ProceduralTreeBranch> Branches => branchList;

        public ProceduralTree Tree { get; }
        public ProceduralTreeBranch Parent { get; }
        public Vector2 Vector { get; }
        public virtual Vector2 Origin => new Lazy<Vector2>(() => Parent.Origin + Parent.Vector).Value;
        public virtual float Thickness => new Lazy<float>(() => Math.Max(1.0f, Parent.Thickness - 1.0f)).Value;

        internal ProceduralTreeBranch(ProceduralTree tree, ProceduralTreeBranch parent, Vector2 vector)
        {
            Tree = tree;
            Parent = parent;
            Vector = vector;
        }

        public void AddBranch(ProceduralTreeBranch newBranch)
        {
            branchList.Add(newBranch);
        }

        public ProceduralTreeBranch Grow()
        {
            var growthAdjustment = (float)(Tree.Random.NextDouble() - 0.5) * GrowthFreedom;
            var vector = Vector.Length*Vector.Rotate(growthAdjustment).Normalize();
            var newBranch = new ProceduralTreeBranch(Tree, this, vector);
            AddBranch(newBranch);
            return newBranch;
        }

        public ProceduralTreeBranch SetSubbranch()
        {
            var direction = Vector;
            var subbranchAdjustment = 0.1f + (float)(Tree.Random.NextDouble()) * SubbranchFreedom;
            // Flip?
            if (Tree.Random.NextDouble() > 0.5)
            {
                subbranchAdjustment = -subbranchAdjustment;
            }

            var vector = Vector.Length/2.0f * direction.Rotate(subbranchAdjustment).Normalize();

            var newBranch = new ProceduralTreeBranch(Tree, this, vector);
            AddBranch(newBranch);
            return newBranch;
        }
    }
}