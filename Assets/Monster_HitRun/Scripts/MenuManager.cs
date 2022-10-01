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
    public PlayerManager Player;
    
    Ray ray;
    RaycastHit hit;
    public void Init()
    {
        GameStace = false;
        //buy_StartLv.Init();
        buy_Offline.Init();
        buy_Boss.Init();

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

        GameController.Instance.LoadScene(SceneName.LOADING_SCENE);
        Debug.Log("Active Scene : " + SceneManager.GetActiveScene().name);
    }

   
}
