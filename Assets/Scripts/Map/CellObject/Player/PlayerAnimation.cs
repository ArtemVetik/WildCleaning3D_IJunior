using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public static class Parameters
    {
        public const string Move = nameof(Move);
    }

    [SerializeField] private Player _player;
    [SerializeField] private Animator _animator;

    private void Update()
    {
        float yAngle = Mathf.Atan2(_player.Direction.x, _player.Direction.y);
        var targetRotation = Quaternion.Euler(0, yAngle * Mathf.Rad2Deg - 90f, 0);

        _player.transform.rotation = Quaternion.RotateTowards(_player.transform.rotation, targetRotation, 500f * Time.deltaTime);

        _animator.SetBool(Parameters.Move, _player.IsMoving);
    }
}
