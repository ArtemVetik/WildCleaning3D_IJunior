using UnityEngine;

public class JsonSaveLoad : ISaveLoadVisiter
{
    private const string PlayerDataKey = "PlayerData_";
    private const string BoosterInventoryKey = "BoosterInventory";
    private const string CleanerInventoryKey = "CleanerInventory";
    private const string ChestsInventoryKey = "ChestsInventory";
    private const string ScoreBalanceKey = "ScoreBalance";
    private const string DiamondBalanceKey = "DiamondBalanceKey";

    #region PlayerData
    public void Save(PlayerData playerData)
    {
        string saveJson = JsonUtility.ToJson(playerData);
        PlayerPrefs.SetString(PlayerDataKey + playerData.ID, saveJson);
        PlayerPrefs.Save();
    }

    public PlayerData Load(PlayerData playerData)
    {
        if (PlayerPrefs.HasKey(PlayerDataKey + playerData.ID))
        {
            string saveJson = PlayerPrefs.GetString(PlayerDataKey + playerData.ID);
            return JsonUtility.FromJson<PlayerData>(saveJson);
        }

        return null;
    }
    #endregion

    #region BoosterInventory
    public void Save(BoosterInventory skinSaved)
    {
        string saveJson = JsonUtility.ToJson(skinSaved, true);
        PlayerPrefs.SetString(BoosterInventoryKey, saveJson);
        PlayerPrefs.Save();
    }

    public BoosterInventory Load(BoosterInventory skinSaved)
    {
        if (PlayerPrefs.HasKey(BoosterInventoryKey))
        {
            string saveJson = PlayerPrefs.GetString(BoosterInventoryKey);
            return JsonUtility.FromJson<BoosterInventory>(saveJson);
        }

        return skinSaved;
    }

    #endregion

    #region CleanerInventory
    public void Save(CleanerInventory cleanerData)
    {
        string saveJson = JsonUtility.ToJson(cleanerData, true);
        PlayerPrefs.SetString(CleanerInventoryKey, saveJson);
        PlayerPrefs.Save();
    }

    public CleanerInventory Load(CleanerInventory cleanerData)
    {
        if (PlayerPrefs.HasKey(CleanerInventoryKey))
        {
            string saveJson = PlayerPrefs.GetString(CleanerInventoryKey);
            return JsonUtility.FromJson<CleanerInventory>(saveJson);
        }

        return cleanerData;
    }

    #endregion

    #region ChestsInventory
    public void Save(ChestInventory chestInventory)
    {
        string saveJson = JsonUtility.ToJson(chestInventory, true);
        PlayerPrefs.SetString(ChestsInventoryKey, saveJson);
        PlayerPrefs.Save();
    }

    public ChestInventory Load(ChestInventory chestInventory)
    {
        if (PlayerPrefs.HasKey(ChestsInventoryKey))
        {
            string saveJson = PlayerPrefs.GetString(ChestsInventoryKey);
            return JsonUtility.FromJson<ChestInventory>(saveJson);
        }

        return chestInventory;
    }

    #endregion

    #region ScoreBalance
    public void Save(ScoreBalance scoreBalance)
    {
        string saveJson = JsonUtility.ToJson(scoreBalance, true);
        PlayerPrefs.SetString(ScoreBalanceKey, saveJson);
        PlayerPrefs.Save();
    }

    public ScoreBalance Load(ScoreBalance scoreBalance)
    {
        if (PlayerPrefs.HasKey(ScoreBalanceKey))
        {
            string saveJson = PlayerPrefs.GetString(ScoreBalanceKey);
            return JsonUtility.FromJson<ScoreBalance>(saveJson);
        }

        return scoreBalance;
    }

    #endregion

    #region DiamondBalance
    public void Save(DiamondBalance diamondBalance)
    {
        string saveJson = JsonUtility.ToJson(diamondBalance, true);
        PlayerPrefs.SetString(DiamondBalanceKey, saveJson);
        PlayerPrefs.Save();
    }

    public DiamondBalance Load(DiamondBalance diamondBalance)
    {
        if (PlayerPrefs.HasKey(DiamondBalanceKey))
        {
            string saveJson = PlayerPrefs.GetString(DiamondBalanceKey);
            return JsonUtility.FromJson<DiamondBalance>(saveJson);
        }

        return diamondBalance;
    }
    #endregion
}
