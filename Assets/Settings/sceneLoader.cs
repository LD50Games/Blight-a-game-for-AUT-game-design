using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneLoader : MonoBehaviour
{
    public void loadScene(string SceneName)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneName);
        
    }
}
