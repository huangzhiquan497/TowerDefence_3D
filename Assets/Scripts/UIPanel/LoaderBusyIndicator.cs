using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UIPanel
{
    public class LoaderBusyIndicator : MonoBehaviour
    {
        [SerializeField] private Image _progress;

        private AsyncOperation _asyncOperation;
        public void SetProgress(AsyncOperation value)
        {
            _asyncOperation = value;
        }

        private void Update()
        {
            if (_asyncOperation == null|| _asyncOperation.isDone) return;

            _progress.fillAmount = _asyncOperation.progress;
            Debug.Log(_asyncOperation.progress);
        }

        public class Factory : PlaceholderFactory<LoaderBusyIndicator>
        {
        }
    }
}