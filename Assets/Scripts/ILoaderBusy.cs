using System;
using UIPanel;
using UnityEngine;
using Object = UnityEngine.Object;

public interface ILoaderBusy
{
    void Show(AsyncOperation asyncOperation);
    void Hide();
}

public class LoaderBusy : ILoaderBusy
{
    private readonly LoaderBusyIndicator.Factory _factory;

    public LoaderBusy(LoaderBusyIndicator.Factory factory)
    {
        _factory = factory;
    }

    private LoaderBusyIndicator _current;
    public void Show(AsyncOperation asyncOperation)
    {
        _current =_factory.Create();
        _current.SetProgress(asyncOperation);
    }

    public void Hide()
    {
        if (_current)
        {
            Object.DestroyImmediate(_current.gameObject);
        }
    }
}