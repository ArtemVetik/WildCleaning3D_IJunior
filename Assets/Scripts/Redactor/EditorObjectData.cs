using UnityEngine;
using System;

namespace CustomRedactor
{
    [Serializable]
    public struct EditorObjectData
    {
        [SerializeField] private string _name;
        [SerializeField] private LevelObject _levelObject;
        [SerializeField] private ObjectParameters _objectParameter;

        public EditorObjectData(string name)
        {
            _name = name;
            _levelObject = null;
            _objectParameter = null;
        }

        public string Name => _name;
        public LevelObject LevelObject => _levelObject;
        public ObjectParameters ObjectParameters => _objectParameter;
    }
}