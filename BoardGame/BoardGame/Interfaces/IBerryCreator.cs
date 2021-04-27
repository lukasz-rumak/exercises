using BoardGame.Models.Berries;

namespace BoardGame.Interfaces
{
    public interface IBerryCreator
    {
        IBerry CreateBerryBasedOnType(string berryType, string coordinates); 
    }
}