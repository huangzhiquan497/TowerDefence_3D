using System;
using GameData;
using UnityEngine;
using UnityEngine.UI;

namespace Test
{
    public class TestFileSave : MonoBehaviour
    {
        [SerializeField] private bool encrypted;
        [SerializeField] private Button _button;
        [SerializeField] private Button _decodeButton;
        private FileSaver<GameDataStore> _jsonSaver;

        private void Start()
        {
            var dataStore = new GameDataStore();
            if (encrypted)
            {
                _jsonSaver = new EncryptedJsonSaver<GameDataStore>("encrypted_save");
                dataStore.MasterVolume = 0.5f;
            }

            else
            {
                _jsonSaver = new JsonSaver<GameDataStore>("normal_save");
                dataStore.MasterVolume = 0.8f;
            }

            _button.onClick.AddListener(() => _jsonSaver.Save(dataStore));
            _decodeButton.onClick.AddListener(() =>
            {
                try
                {
                    if (_jsonSaver.Load(out GameDataStore data))
                    {
                        Debug.Log(data.MasterVolume);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            });
        }
    }
}