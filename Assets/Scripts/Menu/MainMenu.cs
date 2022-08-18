using System;
using System.Collections.Generic;
using Mirror;
using UnityEngine;


namespace Menu
{
    public class MainMenu : MonoBehaviour
    {
        public NetworkManager mainManager;
        
        public GameObject mainPanel;
        public GameObject connectPanel;
        public GameObject optionPanel;

        public void Start()
        {
            MainGame();
        }

        public void MainGame()
        {
            HideAll();
            mainPanel.SetActive(true);
        }
        public void ConnectGame()
        {
            HideAll();
            connectPanel.SetActive(true);
        }

        public void HostGame()
        {
            mainManager.StartHost();
        }

        public void OptionGame()
        {
            HideAll();
            optionPanel.SetActive(true);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        private void HideAll()
        {
            mainPanel.SetActive(false);
            connectPanel.SetActive(false);
            optionPanel.SetActive(false);
        }
    }
}
