using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomRedactor;

public abstract class ObjectParameters : ScriptableObject
{
    public abstract bool CanApply(LevelObject cellObject);
    public abstract void Apply(CellObject cellObject);
}
