using UnityEngine;

public class JsonSaveLoad : ISaveLoadVisiter
{
    private const string SkinSavedKey = "BoosterInventory";

    public BoosterInventory Load()
    {
        if (PlayerPrefs.HasKey(SkinSavedKey))
        {
            string saveJson = PlayerPrefs.GetString(SkinSavedKey);
            return JsonUtility.FromJson<BoosterInventory>(saveJson);
        }

        return null;
    }

    public void Save(BoosterInventory skinSaved)
    {
        string saveJson = JsonUtility.ToJson(skinSaved);
        PlayerPrefs.SetString(SkinSavedKey, saveJson);
        PlayerPrefs.Save();
    }
}
