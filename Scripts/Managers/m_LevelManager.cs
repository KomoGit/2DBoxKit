using UnityEngine;
using UnityEngine.SceneManagement;

public class m_LevelManager : MonoBehaviour
{
    static m_LevelManager lm_Instance;
    private int currentSceneIndex;
    private void Awake()
    {
        //Singleton. If you see this code know that it makes sure there is only 1 of this..
        //..Object in scene.
        if (lm_Instance is null) { lm_Instance = this; DontDestroyOnLoad(gameObject); }
        else if (lm_Instance != this) { Object.Destroy(gameObject); }
    }
    public int getCurrentIndex()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        return currentSceneIndex;
    }
    public void LoadNextLevel() 
    {
        getCurrentIndex();
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
    public void LoadCustomLevel(int index)
    {
        SceneManager.LoadScene(index);
    }
}