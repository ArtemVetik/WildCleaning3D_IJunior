using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct AmbientColor
{
    [SerializeField, ColorUsage(true, true)] private Color _skyColor;
    [SerializeField, ColorUsage(true, true)] private Color _equatorColor;
    [SerializeField, ColorUsage(true, true)] private Color _groundColor;

    public Color SkyColor => _skyColor;
    public Color EquatorColor => _equatorColor;
    public Color GroundColor => _groundColor;
}

public class RoomRenderSettings : MonoBehaviour
{
    [SerializeField] private AmbientColor _ambientColors;
    [SerializeField] private Texture _floorUV;
    [SerializeField] private Sprite[] _dirtySprites;

    public AmbientColor AmbientColors => _ambientColors;
    public Texture FloorUV => _floorUV;
    public Sprite[] DirtySprites => _dirtySprites;

    private void Start()
    {
        RenderSettings.ambientSkyColor = _ambientColors.SkyColor;
        RenderSettings.ambientEquatorColor = _ambientColors.EquatorColor;
        RenderSettings.ambientGroundColor = _ambientColors.GroundColor;
    }
}
