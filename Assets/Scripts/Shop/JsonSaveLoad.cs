using UnityEngine;

public class JsonSaveLoad : ISaveLoadVisiter
{
    private const string SkinSavedKey = "BoosterInventory";
    private const string PlayerDataKey = "PlayerData";

    public BoosterInventory Load(BoosterInventory skinSaved)
    {
        if (PlayerPrefs.HasKey(SkinSavedKey))
        {
            string saveJson = PlayerPrefs.GetString(SkinSavedKey);
            return JsonUtility.FromJson<BoosterInventory>(saveJson);
        }

        return new BoosterInventory();
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

    public void Save(BoosterInventory skinSaved)
    {
        string saveJson = JsonUtility.ToJson(skinSaved);
        PlayerPrefs.SetString(SkinSavedKey, saveJson);
        PlayerPrefs.Save();
    }

    public void Save(PlayerData playerData)
    {
        string saveJson = JsonUtility.ToJson(playerData);
        PlayerPrefs.SetString(SkinSavedKey, saveJson);
        PlayerPrefs.Save();
    }
}
