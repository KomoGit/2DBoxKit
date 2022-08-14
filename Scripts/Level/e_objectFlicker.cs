using UnityEngine;
/// <summary>
/// This provides devs to enable disable objects on random basis...
/// ...interval of the flicker can be modified by the dev to determine...
/// ...how fast the object will be activated / deactivated.
/// ...add this to an empty and assign object you want modified to OOI.
/// DO NOT USE THIS FOR CORE GAMEPLAY PUZZLES!
/// </summary>
public class e_objectFlicker : MonoBehaviour
{
    u_RandomNumberGenerator rNumGen;

    [SerializeField] private float Interval;
    [SerializeField] private GameObject ObjectOfInterest;//OOI

    private float nextActionTime = 0.0f;   
    private int rNum;

    private void Awake()
    {
        rNumGen = GetComponent<u_RandomNumberGenerator>();
        nullProtection();
    }
    private void Update()
    {
        objectController();
        if (Time.time > nextActionTime)
        {
            nextActionTime += Interval;
            rNum = rNumGen.generateNumber();
        }
    }
    private void objectController()
    {
        switch (rNum)
        {
            case 1:
                ObjectOfInterest.SetActive(true);
                break;
            case 0:
                ObjectOfInterest.SetActive(false);
                break;
        }
    }
    private void nullProtection()
    {
        if (Interval <= 0)
        {
            Debug.LogWarning("Warning, interval cannot be zero or less than. This script cannot function.");
            this.gameObject.SetActive(false);
        }
        if (ObjectOfInterest == null )
        {
            Debug.LogWarning("Warning, OOI is empty. This script cannot function.");
            this.gameObject.SetActive(false);
        }     
    }
}