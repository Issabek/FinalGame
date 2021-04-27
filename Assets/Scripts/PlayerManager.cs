using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class PlayerManager : NetworkBehaviour
{
    [SerializeField]
    private int MaxHealth = 100;


    [SyncVar]
    private bool _isDead = false;
    public bool isDead
    {
        get
        {
            return _isDead;
        }
        protected set
        {
            _isDead = value;
        }
    }

    [SyncVar]
    private int CurrentHealth;

    [SerializeField]
    private Behaviour[] disableOnDeath;
    private bool[] wasEnabled;

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(3f);
        SetDefaults();
        Transform startPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = startPoint.position;
        transform.rotation = startPoint.rotation;
    }

    public void Setup()
    {
        wasEnabled = new bool[disableOnDeath.Length];
        for(int i = 0; i < disableOnDeath.Length; i++)
        {
            wasEnabled[i] = disableOnDeath[i].enabled;
        }
        SetDefaults();

    }
    public void Update()
    {
        if (!isLocalPlayer)
            return;
        if (Input.GetKeyDown(KeyCode.K))
        {
            RpcTakeDamage(100);
        }
    }
    [ClientRpc]
    public void RpcTakeDamage(int damage)
    {
        if (isDead)
        {
            return;
        }
        Debug.Log("You took damage: " + damage+" before health: "+CurrentHealth);
        CurrentHealth -= damage;
        //GameObject originalGameObject = GameObject.FindWithTag("Player");
        //GameObject child = originalGameObject.transform.GetChild(2).gameObject;
        //Animator anim = child.GetComponent<Animator>();
        //anim.SendMessage("SetBool",("isFired",true));
        //anim.SetBool("isFired", false);
        Debug.Log("Current health: "+CurrentHealth);
        if (CurrentHealth <= 0)
        {
            Death();
        }
    }
    private void Death()
    {
        isDead = true;
        for(int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = false;
        }
        Collider _col = GetComponent<Collider>();
        if (_col != null)
            _col.enabled = true;

        StartCoroutine(Respawn());
    }
    public void SetDefaults()
    {
        isDead = false;
        CurrentHealth = MaxHealth;
        for(int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = wasEnabled[i];
        }
        Collider _col = GetComponent<Collider>();
        if (_col != null)
            _col.enabled = true;
    }
}
