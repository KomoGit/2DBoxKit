using System;
using UnityEngine;
using TMPro;

public class p_lives : MonoBehaviour
{
    public event EventHandler OnDeath;
    //bool isDead => m_LivesManager.currentLives <= 0;
    private bool isDead => m_LivesManager.getCurrentLives() <= 0;
    private void Awake()
    {
        OnDeath += DeathEvent;
    }
    private void Update()
    {
        playerDeath();
    }
    private void DeathEvent(object sender,EventArgs e)
    {
        Debug.Log("Game OVER!");
        this.gameObject.SetActive(false);       
    }
    private void playerDeath()
    {
        if(isDead)
        {
            OnDeath?.Invoke(this, EventArgs.Empty);
        }
    }
}
