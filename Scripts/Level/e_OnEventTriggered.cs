using System;
using UnityEngine;
/// <summary>
/// This script works together with e_WeightPlates Script as it is subscribed..
/// ..to the event there. When attaching OOI(object of interest), make sure OOI is parented to a empty game object..
/// ..this will ensure the code is executable even if the object is disabled. Try not to use Destroy..
/// ..on multiple objects, instead use disable action. This will prevent garbage collection issues.
/// </summary>
public class e_OnEventTriggered : MonoBehaviour
{
    //PS: I love events.
    [Header("Action")]
    public ActionList actions;//Actions : "What the script will do once the trigger is received".
    [Header("References")]
    public e_WeightPlates _weightPlates;//Choose which instance of plate the object should listen to.
    public GameObject ObjectOfInterest;//Object of interest is what will be affected by the script.

    [HideInInspector] public IFunction _function;//Unfortunately, only one single function can be initiated per instance.
    private bool notDestroyed = true;//To prevent CheckNullOOI issues.

    private void Awake()
    {
        _weightPlates.OnActivation += OnActivationTriggerReceived;
        _function = GetComponent<IFunction>();
    }
    private void Update()
    {
        CheckNullOOI();
        CheckNullFunction();
    }
    public enum ActionList //More actions can be added here.
    {
        LOG,
        ENABLE,
        DISABLE,
        TRIGGERFUNCTION,//This will ensure that a custom scripted function is triggered. Sky is the limit.
        DESTROY
    }
    private void OnActivationTriggerReceived(object sender, EventArgs e)//More actions can be perfomed here.
    {
        switch (actions)
        {
            case ActionList.LOG:
                Debug.Log("Action has been triggered!");
                break;
            case ActionList.ENABLE:
                ObjectOfInterest.SetActive(true);
                break;
            case ActionList.DISABLE:
                ObjectOfInterest.SetActive(false);
                break;
            case ActionList.TRIGGERFUNCTION:
                _function.activationFunction();
                break;
            case ActionList.DESTROY:
                UnityEngine.Object.Destroy(ObjectOfInterest);
                notDestroyed = false;
                break;
        }
    }
    //TODO : Make a method to return the current action. Return actions to being private.
    void CheckNullOOI()
    {
        while(notDestroyed == true)
        {
            if (ObjectOfInterest == null)
            {
                Debug.LogWarning("Object of interest cannot be empty!");
            }
            break;
        }
    }
    void CheckNullFunction()
    {
        if (_function == null && actions == ActionList.TRIGGERFUNCTION)
        {
            Debug.LogWarning("Function cannot be null!");
            actions = ActionList.LOG;
        }
    }
}
