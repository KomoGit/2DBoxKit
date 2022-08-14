using UnityEngine;
public class m_LivesManager : MonoBehaviour
{
    [HideInInspector] private static int currentLives = 3;
    public static int getCurrentLives()
    {
        return currentLives;
    }
    public static void setCurrentLives(int lives)
    {
        currentLives = lives;
    }
    public static void takeDamage()
    {
        currentLives--;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
