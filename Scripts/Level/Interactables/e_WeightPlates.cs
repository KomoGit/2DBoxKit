using System;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
//This script will activate objects based on whether..
//..mass of plate is enough to trigger activation event.
//Multiple objects can listen to the single event from different parts of the map.
public class e_WeightPlates : MonoBehaviour
{
    [SerializeField] private float mass;
    [SerializeField] private float massRequired; //For activation.
    
    public event EventHandler OnActivation;
    public event EventHandler OnDeactivation;

    Rigidbody2D _rb;
    private float combinedMass;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.mass = mass;
        errorCheck();
        lockObject();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var playerTag = collision.collider.CompareTag("Player");
        var ioTag = collision.collider.CompareTag("Interactives/Rigidbody");

        if (playerTag || ioTag)
        {
            calculateMass(collision.collider.attachedRigidbody.mass);//Gets the mass of object colliding with it.
            if(massRequired <= combinedMass)
            {
                OnActivation?.Invoke(this, EventArgs.Empty);//Activates function.
            }
            return;
        }        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        OnDeactivation?.Invoke(this,EventArgs.Empty);//Deactivates function.
    }
    private void lockObject()
    {
        _rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        _rb.freezeRotation = true;
    }
    private float calculateMass(float m)
    {
        combinedMass = mass + m;
        return combinedMass;
    }
    private void errorCheck()
    {
        if(mass <= 0)
        {
            Debug.LogWarning("Warning,mass of the object cannot be less than 0! Deactivating object.");
            this.gameObject.SetActive(false);
        }
    }
}
