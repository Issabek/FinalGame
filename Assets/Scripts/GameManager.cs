using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class GameManager : MonoBehaviour
{
    private static Dictionary<string, PlayerManager> Players = new Dictionary<string, PlayerManager>();
    private const string IdPrefix = "Player ";
    public static void RegisterPlayer(string _netID, PlayerManager Player)
    {
        string PlayerID = IdPrefix + _netID;
        Players.Add(PlayerID, Player);
        Player.transform.name = PlayerID;
    }
    public static void UnRegisterPlayer(string PlayerId)
    {
        Players.Remove(PlayerId);
    }
    public static PlayerManager GetPlayer(string PlayerId)
    {
        return Players[PlayerId];
    }
    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(200, 200, 200, 500));
        GUILayout.BeginVertical();

        foreach (string PlayerId in Players.Keys)
        {
            GUILayout.Label(PlayerId + " - " + Players[PlayerId].transform.name);
        }

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
}
