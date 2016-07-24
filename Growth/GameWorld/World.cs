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
        public const float MainBranchLength = 30f;

        public int Seed { get; }
        public ProceduralTree Tree { get; }

        private readonly Random random;

        public World(int seed)
        {
            Seed = seed;
            random = new Random(Seed);

            Tree = new ProceduralTree(random);

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
                    // Set sub-branches
                    for (int j = i; j < 2; j++)
                    {
                        segment.SetSubbranch();
                    }

                    // Grow the branch
                    segment = segment.Grow();
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

        private void AddMainBranch(UnitVector2 direction)
        {
            Tree.Stem.AddBranch(new ProceduralTreeBranch(Tree, Tree.Stem, direction * MainBranchLength));
        }
    }
}
