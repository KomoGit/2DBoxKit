using UnityEngine;
/// <summary>
/// Think of this script as OnEventTriggered but this one actually uses Trigger instead of collision.
/// </summary>
public class e_TriggerFunction : MonoBehaviour
{
    [SerializeField] BoxCollider2D _trigger;
    IFunction _function;
    private void Awake()
    {
        _function = GetComponent<IFunction>();
        checkNullFunction();
        TriggerNotActive();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _function.activationFunction();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _function.deactivationFunction();
    }
    private void TriggerNotActive()
    {
        if (_trigger.isTrigger == false)
        {
            _trigger.isTrigger = true;
        }
    }
    private void checkNullFunction()
    {
        if(_function == null)
        {
            Debug.LogWarning("No function detected, disabling component");
            this.gameObject.SetActive(false);
        }
    }
}
