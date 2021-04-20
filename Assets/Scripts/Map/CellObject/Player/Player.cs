using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlaneMoveSystem))]
public class Player : CellObject, IMoveable, ISpeedyObject
{
    [SerializeField] private PlayerCharacteristics _defaultCharacteristics;
    [SerializeField] private ParticleSystem _diedEffectTemplate;
    [SerializeField] private PlayerReplacer _replacer;

    private PlayerMoveSystem _playerMoveSystem;
    private PlayerData _characteristics;
    private MapFiller _filler;
    private PlayerTail _tail;
    private bool _isDied = false;
    private bool _moveLocked = false;

    public PlayerData DefaultCharacteristics => _defaultCharacteristics.Characteristic;
    public IPlayerData PlayerData => _characteristics;
    public IPlayerData StartPlayerData { get; private set; }
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

        StartPlayerData = _characteristics.Clone() as IPlayerData;
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

    public IPlayerData Downgrade()
    {
        _characteristics = _defaultCharacteristics.Characteristic;
        _characteristics.Load(new JsonSaveLoad());
        _characteristics.Downgrade();
        _characteristics.Save(new JsonSaveLoad());

        return _characteristics;
    }

    private void OnMoveEnded(GameCell finishCell)
    {
        CurrentCell = finishCell;

        if (finishCell.IsMarked)
        {
            finishCell.DoubleMark();
            return;
        }

        finishCell.PartiallyMark();
        _tail.Add(finishCell, _playerMoveSystem.CurrentDirection);
    }

    public void Move(Vector2Int direction)
    {
        if (_moveLocked)
            return;

        bool move = _playerMoveSystem.Move(CurrentCell, direction, _tail);

        if (move)
            MoveStarted?.Invoke();
    }

    public void Replace(GameCell cell)
    {
        //_playerMoveSystem.ForceStop();
        //_playerMoveSystem.ResetDirection();

        //CurrentCell = cell;
        //transform.position = cell.transform.position + Vector3.up * MeshHeight.MaxMeshHeight / 2f;

        _playerMoveSystem.ForceStop();
        _playerMoveSystem.ResetDirection();

        _moveLocked = true;

        _replacer.Replaced += OnReplaced;
        _replacer.Replace(transform, cell.transform.position + Vector3.up * MeshHeight.MaxMeshHeight / 2f, cell);
    }

    private void OnReplaced(GameCell cell)
    {
        CurrentCell = cell;
        _moveLocked = false;
    }

    private void OnMarkedCellCrossed(GameCell cell)
    {
        FillContour();
    }

    public void ForceStop()
    {
        _playerMoveSystem.ForceStop();
    }

    private void OnPlayerStopped(GameCell cell)
    {
        FillContour();
    }

    private void FillContour()
    {
        _filler.TryFill(_tail);
        _tail.FillTail();
        _tail.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            if (_isDied)
                return;

            Died?.Invoke();
        }
    }

    private void OnDied()
    {
        _playerMoveSystem.ForceStop();
        GetComponent<Collider>().enabled = false;

        Vector3 spawnPosition = transform.position + Vector3.up * transform.localScale.y;
        Instantiate(_diedEffectTemplate, spawnPosition, _diedEffectTemplate.transform.rotation);

        _isDied = true;
    }

    private void OnTailDamaged()
    {
        if (_isDied)
            return;

        Died?.Invoke();
    }

    private void OnEnable()
    {
        _tail.Damaged += OnTailDamaged;
        _playerMoveSystem.MoveEnded += OnMoveEnded;
        _playerMoveSystem.Stopped += OnPlayerStopped;
        _playerMoveSystem.MarkedCellCrossed += OnMarkedCellCrossed;
        Died += OnDied;
    }

    private void OnDisable()
    {
        _tail.Damaged -= OnTailDamaged;
        _playerMoveSystem.MoveEnded -= OnMoveEnded;
        _playerMoveSystem.Stopped -= OnPlayerStopped;
        _playerMoveSystem.MarkedCellCrossed -= OnMarkedCellCrossed;
        Died -= OnDied;
    }
}
