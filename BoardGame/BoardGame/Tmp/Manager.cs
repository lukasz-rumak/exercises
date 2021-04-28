using System.Collections.Generic;
using System.Linq;

namespace BoardGame.Tmp
{
    public class Manager
    {
        public List<Node> CreateKnightsNodes(int withSize)
        {
            //withSize = 5;

            var nodes = new List<Node>();
            var startX = 0;
            var startY = 1;
            for (int i = 0; i < withSize; i++)
            {
                for (int j = 0; j < withSize; j++)
                    if (j == startX)
                    {
                        nodes.Add(new Node(j, i)); 
                        startX += 2;
                    }
                
                startX = startY % 2 == 0 ? 0 : 1;
                startY += 1;
            }

            foreach (var node in nodes)
            {
                if (node.X + 1 < withSize && node.Y + 1 < withSize)
                    node.NorthEast = nodes.First(n => n.X == node.X + 1 && n.Y == node.Y + 1);
                if (node.X + 1 < withSize && node.Y - 1 >= 0)
                    node.SouthEast = nodes.First(n => n.X == node.X + 1 && n.Y == node.Y - 1);
                if (node.X - 1 >= 0 && node.Y - 1 >= 0)
                    node.SouthWest = nodes.First(n => n.X == node.X - 1 && n.Y == node.Y - 1);
                if (node.X - 1 >= 0 && node.Y + 1 < withSize)
                    node.NorthWest = nodes.First(n => n.X == node.X - 1 && n.Y == node.Y + 1);
            }

            return nodes;
        }
    }
}