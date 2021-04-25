using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using QuickStart;
public class PlayerSetup : Mirror.NetworkBehaviour
{   [SerializeField]
    Behaviour[] ComponentsToDisable;
	[SerializeField]
	string remoteLyaerName = "RemotePlayer";

	public TextMesh playerNameText;
	public GameObject floatingInfo;
	private SceneScript sceneScript;
	private Material playerMaterialClone;

	Camera sceneCamera;


	[SyncVar(hook = nameof(OnNameChanged))]
	public string playerName;

	void OnNameChanged(string _Old, string _New)
	{
		playerNameText.text = playerName;
	}


	void Awake()
	{
		//allow all players to run this
		sceneScript = GameObject.Find("SceneReference").GetComponent<SceneReference>().sceneScript;
	}
	  

	[Command]
	public void CmdSetupPlayer(string _name)
	{
		playerName = _name;
		sceneScript.statusText = $"{playerName} joined.";
	}

	void Start()
	{

		if (!isLocalPlayer)
		{
			DisableComponents();
			AssignRemotePlayer();
			floatingInfo.transform.LookAt(Camera.main.transform);
		}
		else
		{

			sceneCamera = Camera.main;
			if (sceneCamera != null)
			{
				sceneCamera.gameObject.SetActive(false);
				string name = "Player" + Random.Range(100, 999);
				CmdSetupPlayer(name);
			}
		}
	}
	void AssignRemotePlayer()
    {
		gameObject.layer = LayerMask.NameToLayer(remoteLyaerName);
    }

	void DisableComponents()
    {
		for (int i = 0; i < ComponentsToDisable.Length; i++)
		{
			ComponentsToDisable[i].enabled = false;
		}
	}

	void OnDisable()
	{
		if (sceneCamera != null)
		{
			sceneCamera.gameObject.SetActive(true);
		}
	}
}
