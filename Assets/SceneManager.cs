using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class SceneManager : MonoBehaviour
{
    private Button btnRegiao1, btnRegiao2, btnRegiao3, btnMenu;

    void Start()
    {
        Scene scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
       //Debug.Log("Active Scene is '" + scene.name + "'.");
        
        //Debug.Log("Scene Name " + scene.name);
        if (scene.name == "Menu")
        {
            btnRegiao1 = GameObject.Find("Canvas/PanelCenter/Regiao1").GetComponent<Button>();
            btnRegiao2 = GameObject.Find("Canvas/PanelCenter/Regiao2").GetComponent<Button>();
            btnRegiao3 = GameObject.Find("Canvas/PanelCenter/Regiao3").GetComponent<Button>();
            btnRegiao1.onClick.AddListener(OnButton1Click);
            btnRegiao2.onClick.AddListener(OnButton2Click);
            btnRegiao3.onClick.AddListener(OnButton3Click);
        }
        else if (scene.name == "Graph")
        {
            btnMenu = GameObject.Find("Canvas/PanelLeft/ButtonMenu").GetComponent<Button>();
            btnMenu.onClick.AddListener(OnButtonMenu);
        }
    }
     
    private void OnButton1Click()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Graph");
    }

    private void OnButton2Click()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GraphX");
    }

    private void OnButton3Click()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GraphY");
    }

    private void OnButtonMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
}
