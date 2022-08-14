using UnityEngine;
//Pretty simple code, just keeps the checkpoint data in an indestructable script.
public class m_respawnManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [HideInInspector] public Transform spawnPos { get; private set; }
    [HideInInspector] public Transform currentCheckpoint = default;
    private Rigidbody2D _rb;
    private void Awake()
    {
        DontDestroyOnLoad(this);
        _rb = player.GetComponentInChildren<Rigidbody2D>();      
    }
    public void respawnPlayer()
    {
        m_LivesManager.takeDamage();
        _rb.velocity = Vector2.zero;
        player.position = spawnPos.position;
    }
    public void setSpawnPos(Transform pos)
    {
        spawnPos = pos;
    }
    public void setCheckpoint(Transform checkpoint)
    {
        currentCheckpoint = checkpoint;
    }
}
