using System.Collections.Generic;
using BoardGame.Interfaces;

namespace BoardGame.Tmp
{
    public interface IKnightNodes
    {
        void CreateKnightsNodes(int withSize);
        bool ValidateBerryAdd(int x, int y);
    }
    public class KnightNodes
    {
        private readonly Dictionary<(int, int), Node> _nodes = new Dictionary<(int, int), Node>();

        public KnightNodes(int withSize)
        {
            CreateKnightsNodes(withSize);
        }

        public void CreateKnightsNodes(int withSize)
        {
            CreateNodes(withSize);
            CreateLinkBetweenNodes(withSize);
        }

        public bool ValidateBerryAdd(int x, int y)
        {
            return _nodes.ContainsKey((x, y));
        }

        private void CreateNodes(int withSize)
        {
            for (int i = 0; i < withSize; i++)
            {
                var flag = i % 2 == 0;
                for (int j = 0; j < withSize; j++)
                {
                    if (flag)
                    {
                        _nodes.Add((j, i), new Node(j, i));
                        flag = false;
                    }
                    else flag = true;
                }
            }
        }
        
        private void CreateLinkBetweenNodes(int withSize)
        {
            foreach (var element in _nodes)
            {
                var value = element.Value;
                var x = value.X;
                var y = value.Y;
                if (x + 1 < withSize && y + 1 < withSize)
                    value.NorthEast = _nodes[(x + 1, y + 1)];
                if (x + 1 < withSize && y - 1 >= 0)
                    value.SouthEast = _nodes[(x + 1, y - 1)];
                if (x - 1 >= 0 && y - 1 >= 0)
                    value.SouthWest = _nodes[(x - 1, y - 1)];
                if (x - 1 >= 0 && y + 1 < withSize)
                    value.NorthWest = _nodes[(x - 1, y + 1)];
            }
        }
    }
}