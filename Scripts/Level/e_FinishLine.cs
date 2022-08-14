using UnityEngine;

public class e_FinishLine : MonoBehaviour
{
    private m_LevelManager _lNext;
    private p_grab _pGrab;
    private void Awake()
    {
        _lNext = FindObjectOfType<m_LevelManager>();
        _pGrab = FindObjectOfType<p_grab>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var tags = collision.gameObject.tag;
        switch (tags)
        {
            case "Player":
                _pGrab.DestroyHeldObject();
                _lNext.LoadNextLevel();
                break;
            default:
                break;
        }
    }
}
