using UnityEngine;

public class UI_audioManager : MonoBehaviour
{

    [Header("Buttons")]
    [SerializeField] private AudioClip buttonHover = default;    
    [SerializeField] private AudioClip buttonClick = default;
    [SerializeField] private AudioClip buttonBack = default;
    [SerializeField] AudioSource _audioMan;

    private void Awake()
    {
        _audioMan = FindObjectOfType<AudioSource>();
    }
    public void playOnHover()
    {
        _audioMan.PlayOneShot(buttonHover);
    }
    public void playOnClick()
    {
        _audioMan.PlayOneShot(buttonClick);
    }
}