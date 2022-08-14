using System;
using UnityEngine;

public class temp_PointerFollow : MonoBehaviour
{
    Transform pointerObj;
    Transform playerObj;
    private void Awake()
    {
        pointerObj = this.transform;
        playerObj = FindObjectOfType<p_movement>().transform;
    }
    private void Update()
    {
        followPlayer();
    }

    private void followPlayer()
    {
        pointerObj.position = playerObj.position;
    }
}
