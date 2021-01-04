using UnityEngine;

public class JsonSaveLoad : ISaveLoadVisiter
{
    private const string SkinSavedKey = "BoosterInventory";
    private const string PlayerDataKey = "PlayerData";

    #region PlayerData

    public void Save(PlayerData playerData)
    {
        string saveJson = JsonUtility.ToJson(playerData);
        PlayerPrefs.SetString(SkinSavedKey, saveJson);
        PlayerPrefs.Save();
    }

    public PlayerData Load(PlayerData playerData)
    {
        if (PlayerPrefs.HasKey(PlayerDataKey))
        {
            string saveJson = PlayerPrefs.GetString(SkinSavedKey);
            return JsonUtility.FromJson<PlayerData>(saveJson);
        }

        return new PlayerData();
    }

    #endregion

    #region BoosterInventory
    public void Save(BoosterInventory skinSaved)
    {
        string saveJson = JsonUtility.ToJson(skinSaved);
        PlayerPrefs.SetString(SkinSavedKey, saveJson);
        PlayerPrefs.Save();
    }

    public BoosterInventory Load(BoosterInventory skinSaved)
    {
        if (PlayerPrefs.HasKey(SkinSavedKey))
        {
            string saveJson = PlayerPrefs.GetString(SkinSavedKey);
            return JsonUtility.FromJson<BoosterInventory>(saveJson);
        }

        return new BoosterInventory();
    }
    #endregion
}
