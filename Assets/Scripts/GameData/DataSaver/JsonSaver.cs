using System.IO;
using UnityEngine;

namespace GameData
{
    public class JsonSaver<T> : FileSaver<T> where T : IDataStore
    {
        public JsonSaver(string fileName) : base(fileName)
        {
        }

        public override void Save(T data)
        {
            var json = JsonUtility.ToJson(data);
            using (var writer = GetWriteStream())
            {
                writer.Write(json);
            }
        }

        public override bool Load(out T data)
        {
            if (!File.Exists(_fileName))
            {
                data = default(T);
                return false;
            }

            using (var reader = GetReadStream())
            {
                var json = reader.ReadToEnd();
                Debug.Log(json);
                data = JsonUtility.FromJson<T>(json);
            }

            return true;
        }
    }
}