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
    public bool Boss;

    
    

     void Start()
    {
        shotted = false;
        rgEnemies = GetComponent<Rigidbody>();
        collider = transform.gameObject.GetComponent<Collider>();
        if(Boss)
        {
            textenemy.Init();
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Ball")
        {
            if (PlayerManager.PlayerManagerIstance.lvPlayer >= lvenemies)
                PlayerManager.PlayerManagerIstance.lvPlayer = PlayerManager.PlayerManagerIstance.lvPlayer + lvenemies;
            else
                
            shotted = true;
        }
        if (other.gameObject.tag == "Player")
        {
            if (PlayerManager.PlayerManagerIstance.lvPlayer < lvenemies)
            {
                other.gameObject.SetActive(false);
                GamePlayController.Instance.menuManager.GameStace = false;
                GamePlayController.Instance.menuManager.BonusEndgame.gameObject.SetActive(true);


            }
            if (PlayerManager.PlayerManagerIstance.powerspeed)
            {
                PlayerManager.PlayerManagerIstance.lvPlayer = PlayerManager.PlayerManagerIstance.lvPlayer + lvenemies;
                gameObject.SetActive(false);
            }

        }
    }

    


}
