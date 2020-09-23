using System.Collections;
using UniRx;
using UnityEngine.SceneManagement;

public interface ISceneLoader
{
    void LoadScene(string sceneName);
}

public class SceneLoader : ISceneLoader
{
    private readonly ILoaderBusy _loader;

    public SceneLoader(ILoaderBusy loader)
    {
        _loader = loader;
    }

    private const string _sceneTransition = "SceneLoader";

    public void LoadScene(string sceneName)
    {
        MainThreadDispatcher.StartCoroutine(StartLoad(sceneName));
    }

    private IEnumerator StartLoad(string sceneName)
    {
        var asyncOperation1 = SceneManager.LoadSceneAsync(_sceneTransition, LoadSceneMode.Single);
        _loader.Show(asyncOperation1);
        yield return asyncOperation1;
        _loader.Hide();
        var asyncOperation2 = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        _loader.Show(asyncOperation2);
        yield return asyncOperation2;
        yield return SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(_sceneTransition));
        _loader.Hide();
    }
}