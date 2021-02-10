using System;
using UnityEngine;

[Serializable]
public class PlayerData : IPlayerData, ISavedObject
{
    [SerializeField] private float _startSpeed;
    [SerializeField] private float _startCleanliness;
    [ReadOnly, SerializeField] private string _id;
    [Header("ModifiedData")]
    [ReadOnly, SerializeField] private float _speed;
    [ReadOnly, SerializeField] private float _cleanliness;

    public string ID => _id;
    public virtual float Speed => _speed;
    public virtual float Cleanliness => _cleanliness;

    public void Upgrade()
    {
        _speed += 0.05f;
        _cleanliness = Mathf.Clamp(_cleanliness + 0.01f, 0, 1);
    }

    public void Load(ISaveLoadVisiter saveLoadVisiter)
    {
        PlayerData savedData = saveLoadVisiter.Load(this);

        if (savedData == null)
        {
            _speed = _startSpeed;
            _cleanliness = _startCleanliness;
        }
        else
        {
            _speed = savedData._speed;
            _cleanliness = savedData._cleanliness;
        }
    }

    public void Save(ISaveLoadVisiter saveLoadVisiter)
    {
        saveLoadVisiter.Save(this);
    }
}
