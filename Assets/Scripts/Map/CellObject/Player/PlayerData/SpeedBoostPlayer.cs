using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostPlayer : PlayerDataDecorator
{
	public SpeedBoostPlayer(IPlayerData data) : base(data) { }

	public override float Speed => Data.Speed * 1.5f;
}
