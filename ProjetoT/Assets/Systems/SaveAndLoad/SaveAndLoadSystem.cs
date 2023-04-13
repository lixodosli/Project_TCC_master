using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace SaveSystem
{
    public class SaveAndLoadSystem : MonoBehaviour
    {
        [SerializeField]
        private SaveChannel m_SaveChannel;
        private string SavePath => $"{Application.persistentDataPath}/save.txt";

        private void Awake()
        {
            m_SaveChannel.OnSave += Save;
            m_SaveChannel.OnLoad += Load;
        }

        private void OnDestroy()
        {
            m_SaveChannel.OnSave -= Save;
            m_SaveChannel.OnLoad -= Load;
        }

        private void Start()
        {
            Debug.Log(SavePath);
        }

        [ContextMenu("Save")]
        public void Save()
        {
            var state = LoadFile();
            CaptureState(state);
            SaveFile(state);
        }

        [ContextMenu("Load")]
        public void Load()
        {
            var state = LoadFile();
            RestoreState(state);
        }

        [ContextMenu("Reset Save")]
        public void ResetSave()
        {
            File.Delete(SavePath);
        }

        private Dictionary<string, object> LoadFile()
        {
            if (!File.Exists(SavePath))
            {
                return new Dictionary<string, object>();
            }

            using (FileStream stream = File.Open(SavePath, FileMode.Open))
            {
                var formatter = new BinaryFormatter();
                stream.Position = 0;
                return (Dictionary<string, object>)formatter.Deserialize(stream);
            }
        }

        private void SaveFile(object state)
        {
            using (var stream = File.Open(SavePath, FileMode.Create))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, state);
            }
        }

        private void CaptureState(Dictionary<string, object> state)
        {
            foreach (var saveable in FindObjectsOfType<SaveableEntity>())
            {
                state[saveable.ID] = saveable.CaptureState();
            }
        }

        private void RestoreState(Dictionary<string, object> state)
        {
            foreach (var saveable in FindObjectsOfType<SaveableEntity>())
            {
                if (state.TryGetValue(saveable.ID, out object value))
                {
                    saveable.RestoreState(value);
                }
            }
        }
    }
}