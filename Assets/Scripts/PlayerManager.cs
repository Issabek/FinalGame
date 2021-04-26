using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class PlayerManager : NetworkBehaviour
{
    [SerializeField]
    private int MaxHealth = 100;

    [SyncVar]
    private int CurrentHealth;

    
    private void Awake()
    {
        SetDefaults();
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("You took damage: " + damage+" before health: "+CurrentHealth);
        CurrentHealth -= damage;
        Debug.Log("Current health: "+CurrentHealth);
    }
    public void SetDefaults()
    {
        CurrentHealth = MaxHealth;
    }
}
