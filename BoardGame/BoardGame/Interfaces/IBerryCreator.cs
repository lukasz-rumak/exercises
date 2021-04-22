using BoardGame.Models.Berries;

namespace BoardGame.Interfaces
{
    public interface IBerryCreator
    {
        IBerry CreateBerryBasedOnType(BerryType berryType, string coordinates);
        BerryType MapToBerryType(string berryType);
    }
}