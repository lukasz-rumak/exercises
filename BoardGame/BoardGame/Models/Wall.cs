namespace BoardGame.Managers
{
    public class Wall
    {
        public (int, int) WallPositionField1 { get; set; }
        public (int, int) WallPositionField2 { get; set; }

        public Wall ReversedWall()
        {
            return new Wall
            {
                WallPositionField1 = this.WallPositionField2,
                WallPositionField2 = this.WallPositionField1
            };
        }
    }
}