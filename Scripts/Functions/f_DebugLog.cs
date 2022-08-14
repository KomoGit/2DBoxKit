using UnityEngine;
/// <summary>
/// This is a custom function to demonstrate power of functions.
/// To make your own function,make a script with 'f_' then with..
/// ...script summary. Inherit IFunction interface and its components...
/// ...and create your function inside of doFunction script. It is also possible...
/// ...to use this script without 'e_OnEventTrigger' script, but not vice-versa.
/// </summary>
public class f_DebugLog : MonoBehaviour,IFunction
{
    public void activationFunction()
    {
        Debug.Log("Custom function is being executed");
    }
    public void deactivationFunction()
    {
        Debug.Log("Custom function's deactivation is being executed");
    }
}