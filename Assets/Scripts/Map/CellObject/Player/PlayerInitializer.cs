using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInitializer : MonoBehaviour
{
    [SerializeField] private BaseInput _keyboardInput;
    [SerializeField] private BaseInput _swipeInput;
    [SerializeField] private MapFiller _filler;

    public Player InstPlayer { get; private set; }

    public event UnityAction<Player> PlayerInitialized;

    public void SetPlayer(Player player)
    {
#if UNITY_EDITOR
        _keyboardInput.Init(player);
#else
        _swipeInput.Init(player);
#endif
        player.Init(_filler);

        InstPlayer = player;
        PlayerInitialized?.Invoke(InstPlayer);
    }
}
