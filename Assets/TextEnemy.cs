using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TextEnemy : MonoBehaviour
{
    private TMPro.TextMeshProUGUI textenemy;
    public Enemies lvBat;
    private GameObject Player;
    public Image EnoughLv;
    public Image notEnoughLv;
    private PlayerManager temp;


    private void Start()
    {
        textenemy = GetComponent<TMPro.TextMeshProUGUI>();
        textenemy.text = "Lv"+ lvBat.lvenemies.ToString() ;
        Player = GameObject.Find("Boy_run");
        temp = Player.GetComponent<PlayerManager>();
       
    }


    // Update is called once per frame
    void Update()
    {
        if (lvBat.lvenemies >temp.lvPlayer)
        {
            EnoughLv.gameObject.SetActive(false);
            notEnoughLv.gameObject.SetActive(true);
        }
        else
        {
            EnoughLv.gameObject.SetActive(true);
            notEnoughLv.gameObject.SetActive(false);
        }
    }
}
