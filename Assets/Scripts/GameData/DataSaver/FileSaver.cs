using System.IO;
using UnityEngine;

namespace GameData
{
    public abstract class FileSaver<T> : IDataSaver<T> where T : IDataStore
    {
        protected string _fileName;

        protected FileSaver(string fileName)
        {
            _fileName = GetFinalSaveFileName(fileName);
        }


        public abstract void Save(T data);
        public abstract bool Load(out T data);

        public void Delete()
        {
            File.Delete(_fileName);
        }

        private string GetFinalSaveFileName(string fileName)
        {
#if UNITY_EDITOR
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
#else
            var fullPath = Path.Combine(Application.persistentDataPath, fileName);
#endif

            return fullPath;
        }

        protected virtual StreamWriter GetWriteStream()
        {
            return new StreamWriter(new FileStream(_fileName, FileMode.Create));
        }

        protected virtual StreamReader GetReadStream()
        {
            return new StreamReader(new FileStream(_fileName, FileMode.Open));
        }
    }
}