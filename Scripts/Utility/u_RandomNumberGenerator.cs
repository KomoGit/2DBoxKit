using UnityEngine;

public class u_RandomNumberGenerator : MonoBehaviour
{

    public int generateNumber()
    {
        int rNum = Random.Range(0, 2);
        return rNum;
    }

}
