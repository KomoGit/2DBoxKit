using UnityEngine;

public class p_singleton : MonoBehaviour
{
    static p_singleton psngl;
    private void Awake()
    {
        if (psngl == null) { psngl = this; DontDestroyOnLoad(gameObject); }
        else if (psngl != this) { Object.Destroy(gameObject); }
    }
}
