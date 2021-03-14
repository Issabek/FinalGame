using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class PlayerSetup : Mirror.NetworkBehaviour
{   [SerializeField]
    Behaviour[] ComponentsToDisable;

    Camera sceneCamera;
    void Start()
    {
        if (!isLocalPlayer)
        {
            foreach (Behaviour component in ComponentsToDisable)
            {
                component.enabled = false;
            }
        }
        else
        {
            sceneCamera = Camera.main;
            if (sceneCamera != null)
            {
                Camera.main.gameObject.SetActive(false);
            }
        }
    }
     void OnDisable()
    {
        if (sceneCamera != null)
        {
            Camera.main.gameObject.SetActive(true);
        }
    }
}
