using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelLoader
{
    public static void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
    }
}
