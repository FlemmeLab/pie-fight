using Mirror;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class ConnectMenu : MonoBehaviour
    {
        public NetworkManager mainManager;
        public Text addressInput;

        public void StartClient()
        {
            mainManager.networkAddress = addressInput.text;
            mainManager.StartClient();
        }
        
    }
}
