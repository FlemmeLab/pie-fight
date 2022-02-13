using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Menu
{
    public class OptionsMenu : MonoBehaviour
    {
        public AudioMixer audioMixer;
        public Dropdown resolutionDropdown;
        
        private List<Resolution> _resolutions;

        public void Start()
        {
            _resolutions = Screen.resolutions.ToList();
            _resolutions.Reverse();
            
            var options = new List<string>();

            foreach (Resolution resolution in _resolutions)
            {
                options.Add(resolution.ToString());
            }

            // options.Reverse();

            int index = options.FindIndex(value => value.Equals(Screen.currentResolution.ToString()));

            Debug.Log(Screen.currentResolution.ToString());
            Debug.Log(index.ToString());
            
            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = index != -1 ? index : 0;
            resolutionDropdown.RefreshShownValue();
        }

        public void SetVolume(float volume)
        {
            audioMixer.SetFloat("MainVolume", volume);
        }

        public void SetFullScreen(bool isFullScreen)
        {
            Screen.fullScreen = isFullScreen;
        }

        public void SetResolution(int index)
        {
            Resolution resolution = _resolutions[index];

            Debug.Log(index.ToString());
            Debug.Log(resolution.ToString());
            
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen, resolution.refreshRate);
        }
    }
}
