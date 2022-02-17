using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Xml;
using System.Text;

public class xmlReader : MonoBehaviour {

    public TextAsset dictionary;

    public string languageName;
    public int currentLanguage;

    string quitter;
    string entrer;

    public Text textQuitter;
    public Text textEntrer;
    public Dropdown selectDropdown;

    List<Dictionary<string, string>> languages = new List<Dictionary<string, string>>();
    Dictionary<string, string> obj;

    void Awake() {
        Reader();
    }

    void Update() {
        languages[currentLanguage].TryGetValue("Name", out languageName);
        languages[currentLanguage].TryGetValue("quitter", out quitter);
        languages[currentLanguage].TryGetValue("entrer", out entrer);

        textQuitter.text = quitter;
        textEntrer.text = entrer;
    }

    void Reader() {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(dictionary.text);
        XmlNodeList languageList = xmlDoc.GetElementsByTagName("language");

        foreach (XmlNode languageValue in languageList) {
            XmlNodeList languageContent = languageValue.ChildNodes;
            obj = new Dictionary<string, string>();

            foreach (XmlNode value in languageContent) { 

                if (value.Name == "Name") {
                    obj.Add(value.Name, value.InnerText);
                }

                if (value.Name == "quitter") {
                    obj.Add(value.Name, value.InnerText);
                }

                if (value.Name == "entrer") {
                    obj.Add(value.Name, value.InnerText);
                }
        
            }

            languages.Add(obj);

        }

    }

    public void ValueChangeCheck()
    {
        currentLanguage = selectDropdown.value;
    }
}
