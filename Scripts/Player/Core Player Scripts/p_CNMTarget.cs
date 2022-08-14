using UnityEngine;
//This script will make the camera follow the player..
//..and only the player.
public class p_CNMTarget : MonoBehaviour
{
    Cinemachine.CinemachineVirtualCamera _CNM;
    p_movement player;

    private void Awake()
    {
        _CNM = FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();
        player = FindObjectOfType<p_movement>();
    }
    private void Update()
    {
        while(_CNM.Follow == null)
        {
            _CNM.Follow = player.transform;
            break;
        }
    }
    
}