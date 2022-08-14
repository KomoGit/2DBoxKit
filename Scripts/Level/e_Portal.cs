using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class e_Portal : MonoBehaviour
{
    [SerializeField] private Transform teleportPoint;
    [SerializeField] private AudioClip PortalSound;

    BoxCollider2D _coll;
    AudioSource _source;

    private void Awake()
    {
        _coll = FindObjectOfType<BoxCollider2D>();
        _source = GetComponent<AudioSource>();
        _coll.isTrigger = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.position = teleportPoint.position;
        collision.attachedRigidbody.velocity = Vector2.zero;
    }
}
