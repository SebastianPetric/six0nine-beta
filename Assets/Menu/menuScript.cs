using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class menuScript : MonoBehaviour {
    public Canvas quitMenu;
    public Canvas startMenu;
    public Image startText;
    public Image exitText;

	// Use this for initialization
	void Start () {
        quitMenu = quitMenu.GetComponent<Canvas>();
        startText = startText.GetComponent<Image>();
        exitText = exitText.GetComponent<Image>();
        quitMenu.enabled = false;
        

    }
	
	// Update is called once per frame
	void Update () {
	
	}
    public void ExitPress()
    {
        quitMenu.enabled = true;
        startText.enabled = false;
        exitText.enabled = false;
    }
}
