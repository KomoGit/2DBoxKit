using System;
using UnityEngine;
[RequireComponent(typeof(e_OnEventTriggered))]
public class e_OnEventDisabled : MonoBehaviour
{
    //If you don't want to include a deactivation event, do not add this to the object.
    //All core components are inherited from e_OnEventTriggered class.
    e_OnEventTriggered _eventTriggerController;
    e_OnEventTriggered.ActionList actionList;
    private void Awake()
    {
        _eventTriggerController = GetComponent<e_OnEventTriggered>();
        _eventTriggerController._weightPlates.OnDeactivation += OnDeactivationTriggerReceived;
        actionList = _eventTriggerController.actions;
    }

    private void OnDeactivationTriggerReceived(object sender, EventArgs e)//This should corresspond with behaviour that is executed on activation and reverse.
    {
        //Actions are reveresed here, enable is technically disable, so on and so forth.
        switch (actionList)
        {   
            case e_OnEventTriggered.ActionList.LOG:
                Debug.Log("Deactivation action has been triggered!");
                break;
            case e_OnEventTriggered.ActionList.ENABLE:
                _eventTriggerController.ObjectOfInterest.SetActive(false);
                break;
            case e_OnEventTriggered.ActionList.DISABLE:
                _eventTriggerController.ObjectOfInterest.SetActive(true);
                break;
            case e_OnEventTriggered.ActionList.TRIGGERFUNCTION:
                _eventTriggerController._function.deactivationFunction();//Do Deactivation Custom Function.
                break;
        }
    }

}
