using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerData
{
    float Speed { get; }
    float MaxSpeed { get; }
    float Cleanliness { get; }
    float MaxCleanliness { get; }
}
