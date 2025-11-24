using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene
    {
        GameMenuScene,
        LoadingScene,
        GameScene
    }

    private static Scene targetScene;

    public static void Load(Scene target)
    {
        targetScene = target;
        SceneManager.LoadScene("1-LoadingScene");
    }

    public static void LoaderCallback()
    {
        SceneManager.LoadScene("2-GameScene");
    }
    public static void LoadBack()
    {
        SceneManager.LoadScene((int)targetScene);
    }
}