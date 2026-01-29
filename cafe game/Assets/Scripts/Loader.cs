using UnityEngine;
using UnityEngine.SceneManagement;
public static class Loader
    

{
    public enum Scene
    {
        StoryIntro,
        GameMenu,
        GameScene,
        EndingScene
    }

    private static Scene targetScene;

    public static void Load(Scene target)
    {
        Time.timeScale = 1;
        targetScene = target;
        SceneManager.LoadScene("2-LoadingScene");
    }

    public static void LoadBack()
    {
        Load(Scene.GameMenu);
    }

    public static void LoaderCallback()
    {
        SceneManager.LoadScene(GetSceneName(targetScene));
    }

    private static string GetSceneName(Scene scene)
    {
        switch (scene)
        {
            case Scene.StoryIntro: return "0-StoryIntro";
            case Scene.GameMenu: return "1-GameMenu";
            case Scene.GameScene: return "3-GameScene";
            case Scene.EndingScene: return "4-EndingScene";
            default: return "1-GameMenu";
        }
    }
}
