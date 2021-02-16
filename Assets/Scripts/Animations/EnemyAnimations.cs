using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(Animator))]
public class EnemyAnimations : MonoBehaviour
{
    public class Parameters
    {
        public static readonly string Move = nameof(Move);
    }

    private Enemy _enemy;
    private Animator _animator;
    private Quaternion _targetRotation;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _enemy.MoveStarting += OnMoveStarting;
        _enemy.MovePausing += OnMovePausing;

        _targetRotation = Quaternion.identity;
    }

    private void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, 10f * Time.deltaTime);
    }

    private void OnMoveStarting(GameCell nextCell)
    {
        _targetRotation = CalculateTargetRotation(nextCell);
        _animator.SetBool(Parameters.Move, true);
    }

    private void OnMovePausing()
    {
        _animator.SetBool(Parameters.Move, false);
    }

    private Quaternion CalculateTargetRotation(GameCell nextCell)
    {
        var shift = nextCell.Position - _enemy.CurrentCell.Position;
        var forwardDirection = new Vector3(shift.x, 0, shift.y);
        return Quaternion.LookRotation(forwardDirection, Vector3.up);
    }

    private void OnDisable()
    {
        _enemy.MoveStarting -= OnMoveStarting;
        _enemy.MovePausing -= OnMovePausing;
    }
}
