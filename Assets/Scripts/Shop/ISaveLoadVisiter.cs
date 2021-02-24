public interface ISaveLoadVisiter
{
    void Save(BoosterInventory skinSaved);
    BoosterInventory Load(BoosterInventory skinSaved);

    void Save(PlayerData playerData);
    PlayerData Load(PlayerData playerData);

    void Save(CleanerInventory cleanerData);
    CleanerInventory Load(CleanerInventory cleanerData);

    void Save(ChestInventory chestInventory);
    ChestInventory Load(ChestInventory chestInventory);

    void Save(GoldBalance scoreBalance);
    GoldBalance Load(GoldBalance scoreBalance);

    void Save(DiamondBalance diamondBalance);
    DiamondBalance Load(DiamondBalance diamondBalance);
}
