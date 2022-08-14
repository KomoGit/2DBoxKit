using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class UI_PauseMenu : MonoBehaviour
{
    private m_LevelManager _lvManager;
    private UI_audioManager _audioManager;
    private p_movement _pMovement;
    private PlayerControls _ctrl;
    private InputAction _menu;
    private AudioSource audioSource;
    public static bool gamePaused = false;
    public GameObject PauseMenuUI;
   
    private void Awake()
    {
        _ctrl = new PlayerControls();
        _lvManager = FindObjectOfType<m_LevelManager>();
        _pMovement = FindObjectOfType<p_movement>();
        _audioManager = FindObjectOfType<UI_audioManager>();
        audioSource = FindObjectOfType<AudioSource>();
    }
    private void OnEnable()
    {
        _menu = _ctrl.UI.Pause;
        _menu.Enable();

        _menu.performed += PauseController;
    }
    private void OnDisable()
    {
        _menu.Disable();
    }
    private void PauseController(InputAction.CallbackContext context)
    {
        if (!gamePaused)
        {
            Pause();
        }
        else
        {
            Resume();
        }     
    }
    public void ReturnToMenu()
    {
        _lvManager.LoadCustomLevel(0);
    }
    public void TerminateApp()
    {
        UnityEditor.EditorApplication.isPlaying = false;//Remove this line on build
        Application.Quit();
    }
    private void Pause()
    {
        _pMovement.enabled = false;
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;     
    } 
    public void Resume()
    {
        _pMovement.enabled = true;
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;    
    }

}
