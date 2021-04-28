namespace BoardGame.Tmp
{
    public class Node
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Node NorthEast { get; set; }
        public Node SouthEast { get; set; }
        public Node SouthWest { get; set; }
        public Node NorthWest { get; set; }

        public Node(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}