using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInitializer : MonoBehaviour
{
    [SerializeField] private SwipeInput _swipeInput;
    [SerializeField] private MapFiller _filler;

    public Player InstPlayer { get; private set; }

    public void SetPlayer(Player player)
    {
        _swipeInput.Init(player);
        player.Init(_filler);

        InstPlayer = player;
    }
}
