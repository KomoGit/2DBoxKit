using UnityEngine;
using TMPro;

public class UI_Player : MonoBehaviour
{
    [SerializeField] TMP_Text livesText;

    private void Update()
    {
        //livesText.text = m_LivesManager.currentLives.ToString("Lives : " + m_LivesManager.getCurrentLives());
        livesText.text = m_LivesManager.getCurrentLives().ToString("Lives : " + m_LivesManager.getCurrentLives());
    }
}
