using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorMath;

namespace GameWorld
{
    public class World
    {
        public const float BranchLength = 30f;
        public const float GrowthFreedom = (float)(2 * Math.PI / 8);

        public int Seed { get; }
        public ProceduralTree Tree { get; }

        private readonly Random random;

        public World(int seed)
        {
            Seed = seed;
            Tree = new ProceduralTree(seed);

            random = new Random(Seed);

            // Add the four main branches in each quadrant
            for (int i = 1; i <= 4; i++)
            {
                AddMainBranch(DrawUnitVectorInQuadrant(i));
            }

            // Grow each main branch a couple of segments
            foreach (var branch in Tree.Stem.Branches)
            {
                var segment = branch;
                for (int i = 0; i < 3; i++)
                {
                    var growthAdjustment = (float)(random.NextDouble() - 0.5) * GrowthFreedom;
                    segment = AddBranch(segment, segment.Vector.Rotate(growthAdjustment).Normalize());
                }
            }
        }

        private UnitVector2 DrawUnitVectorInQuadrant(int quadrant)
        {
            switch (quadrant)
            {
                case 1:
                    return UnitVector2.GetInstance(-1f, (float)random.NextDouble());
                case 2:
                    return UnitVector2.GetInstance(-(float)random.NextDouble(), 1f);
                case 3:
                    return UnitVector2.GetInstance((float)random.NextDouble(), 1f);
                case 4:
                    return UnitVector2.GetInstance(1f, (float)random.NextDouble());
                default:
                    throw new Exception("Invalid quadrant specified.");
            }
        }

        private ProceduralTreeBranch AddMainBranch(UnitVector2 direction)
        {
            return AddBranch(Tree.Stem, direction);
        }

        private ProceduralTreeBranch AddBranch(ProceduralTreeBranch parentBranch, UnitVector2 direction)
        {
            var newBranch = new ProceduralTreeBranch(parentBranch, BranchLength*direction);
            Tree.AddBranch(
                parentBranch,
                newBranch);

            return newBranch;
        }
    }
}
