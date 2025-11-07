using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Saves.Scripts
{
    public abstract class DataModel<T> : IPersistence where T : new()
    {
        public T Data { get; private set; }
        public bool IsLoaded { get; private set; }

        public string SaveKey => GetType().FullName;
        public object Persistence => Data;

        // todo to async
        public UniTask Load(CancellationToken cancellationToken)
        {
            if (IsLoaded)
            {
                return UniTask.CompletedTask;
            }

            if (PlayerPrefs.HasKey(SaveKey))
            {
                string saveValue = PlayerPrefs.GetString(SaveKey);

                if (string.IsNullOrEmpty(saveValue))
                {
                    Data = new T();
                }
                else
                {
                    try
                    {
                        Data = JsonUtility.FromJson<T>(saveValue) ?? new T();
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"Failed to parse save for {SaveKey}: {e.Message}");
                        PlayerPrefs.DeleteKey(SaveKey);
                        Data = new T();
                    }
                }
            }
            else
            {
                Data = new T();
            }

            IsLoaded = true;
            return UniTask.CompletedTask;
        }

        // todo to async
        public UniTask Save(CancellationToken cancellationToken)
        {
            if (IsLoaded == false)
            {
                Debug.LogError("Data model cannot be saved, cause model is not loaded.");
                return UniTask.CompletedTask;
            }

            try
            {
                string saveValue = JsonUtility.ToJson(Data);
                PlayerPrefs.SetString(SaveKey, saveValue);
                PlayerPrefs.Save();
            }
            catch (Exception e)
            {
                Debug.LogError($"Error saving model {SaveKey}: {e.Message}");
            }
            
            return UniTask.CompletedTask;
        }

        public void ResetData()
        {
            PlayerPrefs.DeleteKey(SaveKey);
            Data = new T();
        }
    }
}