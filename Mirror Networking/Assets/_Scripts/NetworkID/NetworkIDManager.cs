using Mirror;
using System.Collections.Generic;
using UnityEngine;

public class NetworkIDManager : NetworkBehaviour
{
    private static Dictionary<string, GameObject> players = new Dictionary<string, GameObject>();
    private bool destroyThis;

    private void Awake()
    {
        destroyThis = true;
    }

    public override void OnStartServer()
    {
        base.OnStartClient();
        destroyThis = false;
    }

    private void Start()
    {
        if (destroyThis)
            Destroy(this.gameObject);
    }

    public static void AddPlayer(string playerID, GameObject player)
    {
        players.Add(playerID, player);
    }
}
