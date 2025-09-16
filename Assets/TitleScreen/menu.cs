using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    private void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
    }
    public void StartGane()
    {
        SceneManager.LoadScene("2D");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
