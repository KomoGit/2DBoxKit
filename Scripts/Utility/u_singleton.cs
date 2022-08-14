using UnityEngine;

public class u_singleton : MonoBehaviour
{
    static u_singleton Instance;
    //Seems like using this script is impossible to add singleton..
    //..function to multiple objects.
    private void Awake()
    {
        if (Instance == null ) { Instance = this; DontDestroyOnLoad(gameObject); }
        else if (Instance != this) { Object.Destroy(gameObject); }
    }

}
