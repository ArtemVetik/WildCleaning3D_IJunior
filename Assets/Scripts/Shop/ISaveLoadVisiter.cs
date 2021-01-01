public interface ISaveLoadVisiter
{
    void Save(BoosterInventory skinSaved);
    BoosterInventory Load(BoosterInventory skinSaved);

    void Save(PlayerData playerData);
    PlayerData Load(PlayerData playerData);
}
