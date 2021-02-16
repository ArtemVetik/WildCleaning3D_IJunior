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
    private PlayerData _characteristics;
    private MapFiller _filler;
    private PlayerTail _tail;

    public PlayerData DefaultCharacteristics => _defaultCharacteristics.Characteristic;
    public IPlayerData PlayerData => _characteristics;
    public IPlayerData PlayerDataClone => _characteristics.Clone() as IPlayerData;
    public float Speed => _characteristics.Speed;
    public float Cleanliness => _characteristics.Cleanliness;
    public Vector2Int Direction => _playerMoveSystem.CurrentDirection;
    public bool IsMoving => _playerMoveSystem.IsMoving;

    public event UnityAction MoveStarted;
    public event UnityAction Died;

    protected override void OnAwake()
    {
        var moveSystem = GetComponent<PlaneMoveSystem>();
        moveSystem.Init(this);

        _playerMoveSystem = new PlayerMoveSystem(moveSystem, MeshHeight);
        _tail = new PlayerTail();

        _characteristics = _defaultCharacteristics.Characteristic;
        _characteristics.Load(new JsonSaveLoad());
    }

    public void Init(MapFiller filler)
    {
        _filler = filler;
    }

    public void ModifiedCharacteristics(PlayerData newCharacteristics)
    {
        _characteristics = newCharacteristics;
    }

    public IPlayerData Upgrade()
    {
        _characteristics = _defaultCharacteristics.Characteristic;
        _characteristics.Load(new JsonSaveLoad());
        _characteristics.Upgrade();
        _characteristics.Save(new JsonSaveLoad());

        return _characteristics;
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
        transform.position = cell.transform.position + Vector3.up * MeshHeight.MaxMeshHeight / 2f;
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
