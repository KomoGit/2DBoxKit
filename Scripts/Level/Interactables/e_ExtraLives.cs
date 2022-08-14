using UnityEngine;
/// <summary>
/// Use this to give extra health to players.
/// It will automatically configure itself, all you have to do is to add it to the game object.
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public class e_ExtraLives : MonoBehaviour
{
    BoxCollider2D _coll;
    private void Awake()
    {      
        _coll = GetComponent<BoxCollider2D>();
        _coll.isTrigger = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int currentLives = m_LivesManager.getCurrentLives();
        m_LivesManager.setCurrentLives(currentLives + 1);
        this.gameObject.SetActive(false);
    }
}
