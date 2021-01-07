using UnityEngine;

public class JsonSaveLoad : ISaveLoadVisiter
{
    private const string BoosterInventoryKey = "BoosterInventory";
    private const string CleanerInventoryKey = "CleanerInventory";

    #region PlayerData
    public void Save(PlayerData playerData)
    {
        string saveJson = JsonUtility.ToJson(playerData);
        PlayerPrefs.SetString(playerData.ID, saveJson);
        PlayerPrefs.Save();
    }

    public PlayerData Load(PlayerData playerData)
    {
        if (PlayerPrefs.HasKey(playerData.ID))
        {
            string saveJson = PlayerPrefs.GetString(playerData.ID);
            return JsonUtility.FromJson<PlayerData>(saveJson);
        }

        return null;
    }
    #endregion

    #region BoosterInventory
    public void Save(BoosterInventory skinSaved)
    {
        string saveJson = JsonUtility.ToJson(skinSaved, true);
        Debug.Log("SAVE: " + saveJson);

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

        return new BoosterInventory();
    }

    #endregion

    #region CleanerInventory
    public void Save(CleanerInventory cleanerData)
    {
        string saveJson = JsonUtility.ToJson(cleanerData, true);
        Debug.Log("SAVE: " + saveJson);
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

        return new CleanerInventory();
    }

    #endregion
}
