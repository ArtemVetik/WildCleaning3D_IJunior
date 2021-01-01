using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerDataDecorator : PlayerData
{
	protected IPlayerData Data;

	public PlayerDataDecorator(IPlayerData data)
	{
		Data = data;
	}
}
