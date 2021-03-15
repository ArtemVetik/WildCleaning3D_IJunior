using System;
using UnityEngine;

[Serializable]
public class PlayerData : IPlayerData, ISavedObject, ICloneable
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
    public float MaxSpeed => 8f;
    public float MaxCleanliness => 1f;

    public void Upgrade()
    {
        _speed = Mathf.Clamp(_speed + 0.05f, 0f, 8f);
        _cleanliness = Mathf.Clamp(_cleanliness + 0.01f, 0f, 1);
    }

    public void Downgrade()
    {
        _cleanliness = Mathf.Clamp(_cleanliness - 0.01f, 0f, 1f);
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

    public object Clone()
    {
        var cloneData = new PlayerData();
        cloneData._startSpeed = _startSpeed;
        cloneData._startCleanliness = _startCleanliness;
        cloneData._id = _id;
        cloneData._speed = _speed;
        cloneData._cleanliness = _cleanliness;

        return cloneData;
    }
}
