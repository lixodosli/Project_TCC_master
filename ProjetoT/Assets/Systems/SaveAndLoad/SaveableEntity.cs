using System;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    public class SaveableEntity : MonoBehaviour
    {
        [SerializeField] private string m_Id = string.Empty;

        public string ID => m_Id;

        [ContextMenu("Generate ID")]
        private void GenerateId() => m_Id = Guid.NewGuid().ToString();

        public object CaptureState()
        {
            var state = new Dictionary<string, object>();

            foreach (var saveable in GetComponents<ISaveable>())
            {
                state[saveable.GetType().ToString()] = saveable.CaptureState();
            }

            return state;
        }

        public void RestoreState(object state)
        {
            var stateDictionary = (Dictionary<string, object>)state;

            foreach (var saveable in GetComponents<ISaveable>())
            {
                string typeName = saveable.GetType().ToString();

                if (stateDictionary.TryGetValue(typeName, out object value))
                {
                    saveable.RestoreState(value);
                }
            }
        }
    }
}