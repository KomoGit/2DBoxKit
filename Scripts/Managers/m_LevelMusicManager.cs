using UnityEngine;
//using UnityEngine.SceneManagement;

//By Faqan >> 2022

public class m_LevelMusicManager : MonoBehaviour
{
    [SerializeField][Range(0, 1)] private float musicVolume;
    [SerializeField] private AudioSource _audioSource = default;
    [SerializeField] private AudioClip[] SceneMusic;

    private int lvIndex;
    m_LevelManager _levelManager;
   
    private void Awake() //Get components in Awake as Awake is the first to initialize when the game starts.
    {
        NullProtection();
        _audioSource.spatialBlend = 0;
        _audioSource.loop = true;
        _levelManager = FindObjectOfType<m_LevelManager>();
    }

    private void Start()
    {
        lvIndex =  _levelManager.getCurrentIndex();
        playMusic();
    }
    private void Update()
    {
        controlMusicVolume();
    }
    public void controlMusicVolume()
    {
        _audioSource.volume = musicVolume;
    }
    //Never forget to encapsulate.
    private void playMusic()
    {
        if(lvIndex == 0) { this.gameObject.SetActive(false); }
        else { _audioSource.PlayOneShot(SceneMusic[lvIndex]); }       
        Debug.Log(lvIndex);
    }   
    private void NullProtection()
    {
        if(SceneMusic.Length == 0)
        {
            Debug.LogWarning("Warning, no music file can be found. Shutting down this component.");
            this.gameObject.GetComponent<m_LevelMusicManager>().enabled = false;
        }
    }
}
