namespace BoardGame.Interfaces
{
    public interface IPawn
    {
        int PawnId { get; set; }
        bool IsAlive { get; set; }
        IPosition Position { get; }
        void ExecuteThePlayerInstruction(char instruction);
    }
}