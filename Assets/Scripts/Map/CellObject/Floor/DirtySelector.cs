using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DirtySelector : MonoBehaviour
{
    [SerializeField] private Sprite[] _dirtyTextures;
    [SerializeField] private GameObject[] _tiles;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        int index = 0;
        for (; index < _tiles.Length; index++)
        {
            if (_tiles[index].activeSelf)
                break;
        }

        _spriteRenderer.sprite = _dirtyTextures[index];
    }

    public void Init(Sprite[] dirtyTextures)
    {
        _dirtyTextures = dirtyTextures;
    }
}
