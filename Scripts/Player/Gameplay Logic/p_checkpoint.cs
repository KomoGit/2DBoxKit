using UnityEngine;
using UnityEngine.SceneManagement;

//Pretty self explanatory, this script allows the player to respawn at a checkpoint..
//..but also load the next level when entering finish.
public class p_checkpoint : MonoBehaviour
{
    private GameObject _gameLogicCarrier;
    private p_grab _playerGrab;
    private m_respawnManager _chManager;

    private void Awake()
    {
        _playerGrab = FindObjectOfType<p_grab>();
        _chManager = FindObjectOfType<m_respawnManager>();
        _gameLogicCarrier = FindObjectOfType<m_respawnManager>().gameObject;
        checkForMissingGL();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var tags = collision.gameObject.tag;
        switch (tags)
        {
            case "Gamelogic/Checkpoint":
                _chManager.currentCheckpoint = collision.gameObject.transform;
                _chManager.setSpawnPos(_chManager.currentCheckpoint); //New implementation with get set.
                break;
            case "Gamelogic/Death"://Here you should destroy the player.
                _playerGrab.DestroyHeldObject();
                LoadScene();                
                _chManager.respawnPlayer();                                            
                break;
        }
    }

    private void LoadScene()
    {
        var currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
    //This script is a failsafe, in case developers..
    //..forget to add in Logic empty into the scene hierarcy.
    private void checkForMissingGL()
    {
        if(_gameLogicCarrier == null)
        {
            Debug.LogWarning("Failure to find core level scripts. Are you missing the 'Gamelogic' empty?");
        }
    }
}
