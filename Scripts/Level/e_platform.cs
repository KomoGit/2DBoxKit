using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(EdgeCollider2D))]
//Use this script for platforms players..
//..can interact with.
public class e_platform : MonoBehaviour
{
    [Header("References & Values")]
    [SerializeField] private float platformSpeed;
    [SerializeField] private int startPoint;
    [SerializeField] private Transform[] points;

    private Transform _playerEmpty;
    Rigidbody2D _rb;

    private int i;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerEmpty = FindObjectOfType<p_singleton>().transform;
    }
    private void Start()
    {
        transform.position = points[startPoint].position;
        _rb.isKinematic = true;
    }
    private void Update()
    {
        errorCheck();
        movePlatform();      
    }
    private void movePlatform()
    {
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;
            if (i == points.Length)
            {
                i = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, platformSpeed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(_playerEmpty);
        }
        else
        {
            collision.transform.SetParent(null);
        }
    }
    private void errorCheck()
    {
        if(points.Length <= 1) 
        {
            Debug.Log("Critical error, points of transform cannot be less than one. Disabling component");
            this.gameObject.SetActive(false);   
        }
    }
}