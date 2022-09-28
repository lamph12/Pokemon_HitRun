using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public bool GameStace =false;
    public GameObject menuElement;
    public GameObject YouLose;
    public GameObject Again;
    public GameObject BonusEndgame;
    public Buy_Boss buy_Boss;
    public Buy_offline buy_Offline;
    public Buy_StartLv buy_StartLv;
    
    Ray ray;
    RaycastHit hit;
    public void Init()
    {
        GameStace = false;
        buy_StartLv.Init();
        buy_Offline.Init();
        buy_Boss.Init();
        Debug.Log("da vao init");
    }

    // Update is called once per frame
    void Update()
    {
        
    
    }
    public void StartGame()
    {
        Debug.Log("start game");
        GameStace = true;
        menuElement.SetActive(false);
        PlayerManager.PlayerManagerIstance.InRun = true;
    }
    public void EndGame()
    {
        YouLose.SetActive(false);
        YouLose.SetActive(false);
        Again.SetActive(true);
    }
    public void Retry_btn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Active Scene : " + SceneManager.GetActiveScene().name);
    }

   
}
