using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemies : MonoBehaviour
{
    public int lvenemies;
    public bool shotted;
    private Rigidbody rgEnemies;
    private Collider collider;
    public TextEnemy textenemy;
    private Animator anim;
    public bool Boss;

    
    

     void Start()
    {
        shotted = false;
        rgEnemies = GetComponent<Rigidbody>();       
        if (Boss)
         anim=   transform.GetChild(0).GetComponent<Animator>();
        else
            anim = GetComponent<Animator>();
        collider = transform.gameObject.GetComponent<Collider>();
        if(Boss)
        {
            textenemy.Init();
        }

        anim.Play((int)0.01f, -1, Random.value);
    }

    private void OnTriggerEnter(Collider other)
    {

        //if (other.gameObject.tag == "Ball")
        //{
        //    //if (PlayerManager.PlayerManagerIstance.lvPlayer >= lvenemies)
        //    PlayerManager.PlayerManagerIstance.lvPlayer = PlayerManager.PlayerManagerIstance.lvPlayer + lvenemies;
        //    other.gameObject.SetActive(false);
        //    gameObject.SetActive(false);
        //    //else

        //    //shotted = true;
        //}
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("da vao chua");
            if (PlayerManager.PlayerManagerIstance.lvPlayer < lvenemies)
            {
                Debug.Log("vao if");
                other.gameObject.SetActive(false);
                GamePlayController.Instance.menuManager.GameStace = false;
                if (PlayerManager.PlayerManagerIstance.numberKey == 3)
                {
                    GameObject prize = Instantiate(Resources.Load<GameObject>("UI/PanelPrizesnomal"));
                    GraphicRaycaster menu;
                    menu = GamePlayController.Instance.menuManager.GetComponent<GraphicRaycaster>();
                    menu.enabled=false;
                }

                else
                    GamePlayController.Instance.menuManager.BonusEndgame.gameObject.SetActive(true);
                //GamePlayController.Instance.menuManager.BonusEndgame.gameObject.SetActive(true);


            }
            else
            {

                 Debug.Log("vao else");
                    PlayerManager.PlayerManagerIstance.lvPlayer = PlayerManager.PlayerManagerIstance.lvPlayer + lvenemies;
                    gameObject.SetActive(false);
              
            }
           

        }
    }

    


}
