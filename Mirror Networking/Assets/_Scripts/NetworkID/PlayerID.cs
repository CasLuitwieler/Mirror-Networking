using Mirror;
using UnityEngine;

[RequireComponent(typeof(NetworkIdentity))]
public class PlayerID : NetworkBehaviour
{
    private string _playerID = "not yet assigned";

    public override void OnStartLocalPlayer()
    {
        CreatePlayerID();
        transform.name = _playerID;
        CmdSetPlayerID(_playerID);
    }

    private void CreatePlayerID()
    {
        _playerID = "Player " + GetComponent<NetworkIdentity>().netId.ToString();
    }

    [Command]
    private void CmdSetPlayerID(string playerID)
    {
        transform.name = playerID;
        NetworkIDManager.AddPlayer(playerID, this.gameObject);
    }
}
