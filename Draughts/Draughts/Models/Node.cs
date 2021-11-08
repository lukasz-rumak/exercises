using System.Linq.Expressions;

namespace Draughts.Models
{
    public class Node
    {
        public Node PreviousNode { get; set; }
        public Node LeftNode { get; set; }
        public Node RightNode { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
    }
}