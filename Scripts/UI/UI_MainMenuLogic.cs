using UnityEngine;

public class UI_MainMenuLogic : MonoBehaviour
{
    public void terminateApp()
    {
        Debug.Log("Thanks for playing :)");
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
