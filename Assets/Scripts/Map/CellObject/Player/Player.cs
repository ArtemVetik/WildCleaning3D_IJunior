using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlaneMoveSystem))]
public class Player : CellObject, IMoveable, ISpeedyObject
{
    [SerializeField] private PlayerCharacteristics _defaultCharacteristics;
    [SerializeField] private ParticleSystem _diedEffectTemplate;

    private PlayerMoveSystem _playerMoveSystem;
    private MapFiller _filler;
    private PlayerTail _tail;

    public PlayerData Characteristics { get; private set; }
    public float Speed => Characteristics.Speed;
    public Vector2Int Direction => _playerMoveSystem.CurrentDirection;

    public event UnityAction MoveStarted;
    public event UnityAction Died;

    private void Awake()
    {
        var moveSystem = GetComponent<PlaneMoveSystem>();
        moveSystem.Init(this);

        _playerMoveSystem = new PlayerMoveSystem(moveSystem);
        _tail = new PlayerTail();

        Characteristics = _defaultCharacteristics.Characteristic;
        Characteristics.Load(new JsonSaveLoad());
    }

    public void Init(MapFiller filler)
    {
        _filler = filler;
    }

    public void ModifiedCharacteristics(PlayerData newCharacteristics)
    {
        Characteristics = newCharacteristics;
    }

    private void OnMoveEnded(GameCell finishCell)
    {
        CurrentCell = finishCell;
        finishCell.Mark();

        _tail.Add(finishCell, _playerMoveSystem.CurrentDirection);
    }

    public void Move(Vector2Int direction)
    {
        bool move = _playerMoveSystem.Move(CurrentCell, direction);

        if (move)
            MoveStarted?.Invoke();
    }

    public void Replace(GameCell cell)
    {
        _playerMoveSystem.ForceStop();
        CurrentCell = cell;
        transform.position = cell.transform.position;
    }

    private void OnMarkedCellCrossed(GameCell cell)
    {
        _filler.TryFill(_tail);
        _tail.Clear();
    }

    private void OnPlayerStopped(GameCell cell)
    {
        _filler.TryFill(_tail);
        _tail.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            _playerMoveSystem.ForceStop();
            Died?.Invoke();
        }
    }

    private void OnDied()
    {
        Vector3 spawnPosition = transform.position + Vector3.up * transform.localScale.y;
        Instantiate(_diedEffectTemplate, spawnPosition, _diedEffectTemplate.transform.rotation);
    }

    private void OnEnable()
    {
        _playerMoveSystem.MoveEnded += OnMoveEnded;
        _playerMoveSystem.Stopped += OnPlayerStopped;
        _playerMoveSystem.MarkedCellCrossed += OnMarkedCellCrossed;
        Died += OnDied;
    }

    private void OnDisable()
    {
        _playerMoveSystem.MoveEnded -= OnMoveEnded;
        _playerMoveSystem.Stopped -= OnPlayerStopped;
        _playerMoveSystem.MarkedCellCrossed -= OnMarkedCellCrossed;
        Died -= OnDied;
    }
}
