using Mirror;
using UnityEngine;

[RequireComponent(typeof(NetworkIdentity))]
public class PlayerID : NetworkBehaviour
{
    private string _playerID = "not yet assigned";

    private void Awake()
    {
        CreatePlayerID();
    }

    public override void OnStartLocalPlayer()
    {
        CmdSetPlayerID();
    }

    private void CreatePlayerID()
    {
        _playerID = "Player " + GetComponent<NetworkIdentity>().netId.ToString();
    }

    [Command]
    private void CmdSetPlayerID()
    {
        Debug.Log("Adding " + _playerID);
        NetworkIDManager.AddPlayer(_playerID, this.gameObject);
    }
}
