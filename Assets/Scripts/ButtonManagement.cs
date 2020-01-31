using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManagement : MonoBehaviour {
    public Canvas exitMenu;
    public Button exitYes;
    public Button exitNo;
    // Use this for initialization
    void Start () {
        exitMenu.enabled = false;
        Cursor.visible = true;
        //Debug.Log("fhdhfsjfhjdsfh");
        //AudioManager.PlaySound("menu");

		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.A))
        {
            SceneManager.LoadScene("first");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SceneManager.LoadScene("main");
        }

    }
    public void NewGameBtn(string newGameLevel) {
        SceneManager.LoadScene(newGameLevel);

    }
    public void ExitGameBtn()
    {
        exitMenu.enabled = true;

        exitYes.onClick.AddListener(yesClick);
        exitNo.onClick.AddListener(noClick);

        

    }

    void yesClick()
    {
        Application.Quit();
    }
    void noClick()
    {
        exitMenu.enabled = false;
    }
}
