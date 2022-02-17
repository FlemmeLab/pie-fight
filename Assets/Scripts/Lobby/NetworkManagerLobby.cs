using System;
using Mirror;
using UnityEngine;

namespace Lobby
{
    public class NetworkManagerLobby : NetworkBehaviour
    {
        [Scene] public string menuScene = String.Empty;

        [Header("Room")] public NetworkRoomPlayerLobby roomPlayerPrefab = null;
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
