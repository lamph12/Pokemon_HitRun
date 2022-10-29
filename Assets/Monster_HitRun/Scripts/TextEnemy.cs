using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TextEnemy : MonoBehaviour
{
    private TMPro.TextMeshProUGUI textenemy;
    public Enemies lvBat;
    public Image EnoughLv;
    public Image notEnoughLv;



    public void Init()
    {
        textenemy = GetComponent<TMPro.TextMeshProUGUI>();
        textenemy.text = "Lv"+ lvBat.lvenemies.ToString() ;
        //temp = Player.GetComponent<PlayerManager>();
       
    }


    // Update is called once per frame
    void Update()
    {
        
        checkLevel();
    }
    void checkLevel()
    {
        if (lvBat.lvenemies > PlayerManager.PlayerManagerIstance.lvPlayer)
        {
            EnoughLv.enabled = false;
            notEnoughLv.enabled = true;
        }
        else
        {
            EnoughLv.enabled = true;
            notEnoughLv.enabled = false;
        }
    }
}
