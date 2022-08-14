using System.Collections;
using UnityEngine;
//This one only targets objects that aren't player..
//..for sake of cinematography, and cutscenes.
public class u_cutsceneCamera : MonoBehaviour
{
    Cinemachine.CinemachineVirtualCamera _CNM;
    p_CNMTarget _playerCamera;
    p_movement _playerMovement;
    [Header("Reference & Values")]
    [SerializeField] GameObject ObjectOfInterest;
    [SerializeField] float CutsceneDuration;


    //Hidden Values
    private bool cutsceneTriggered = false;
    bool pointReached => _CNM.transform == ObjectOfInterest.transform;

    private void Awake()
    {
        _CNM = FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();
        _playerCamera = FindObjectOfType<p_CNMTarget>();
        _playerMovement = FindObjectOfType<p_movement>();
    }
    private void Update()
    {
        errorCheck();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var tag = collision.gameObject.tag;
        if(tag == "Player")
        {
            moveCamera();
            _playerMovement.stopMovement();
        }     
    }
    //Need to be able to reset the camera when..
    //..Cutscene is over. So a timelimit..(Possibly through Coroutines) (Completed)
    //..and default values needs to be stored. (Completed)
    private void moveCamera()
    {
        _playerMovement.enabled = false;
        while (!pointReached && !cutsceneTriggered)
        {
            _CNM.Follow = ObjectOfInterest.transform;
            _playerCamera.enabled = false;
            cutsceneTriggered = true;
            StartCoroutine("resetCamera",CutsceneDuration);
            break;
        }
    }
    IEnumerator resetCamera(float CutsceneDuration)
    {
        yield return new WaitForSeconds(CutsceneDuration);
        if (_playerCamera.enabled == false)
        {
            _playerCamera.enabled = true;
            _playerMovement.enabled = true;
            _CNM.Follow = null;
            this.gameObject.SetActive(false);
        }
        yield break;
    }
    //This code prevents users from assigning value that is negative or zero..
    //..Such values will cause the script to glitch.
    void errorCheck()
    {
        if(CutsceneDuration <= 0)
        {
            Debug.Log("Critical error, cutscene duration cannot be 0 or less! Disabling the component.");
            this.gameObject.SetActive(false);
        }
    }
}