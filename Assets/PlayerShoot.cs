using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerShoot : NetworkBehaviour
{
    private const string PlayerTag = "Player";
    public PlayerWeapon weapon;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask mask;

    private void Start()
    {
        if (cam == null) {
            Debug.LogError("");
            this.enabled = false;
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            Shoot();
    }
    [Client]
    void Shoot()
    {
        System.Random rnd = new System.Random();
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, weapon.range, mask))
        {
            if (hit.collider.tag == PlayerTag)
            {
                CmdPlayerShot(hit.collider.name, weapon.damage + rnd.Next(-3, 3));
            }
            else
            {
                Debug.Log("We hit " + hit.collider.name);
            }
        }
    }

    [Command]
    void CmdPlayerShot(string _ID,int damage)
    {
        Debug.Log("We hit " + _ID);
        PlayerManager player = GameManager.GetPlayer(_ID);
        player.TakeDamage(damage);
    }
}
