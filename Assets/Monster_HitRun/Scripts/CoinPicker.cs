using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CoinPicker : MonoBehaviour
{
    public static CoinPicker coinPicker;
    public Text textcoins;
    public int coins =0;
    private void Awake()
    {
        coinPicker = this;
    }
    private void Start()
    {
        coins = PlayerPrefs.GetInt("coins");
        Debug.Log(coins);
        //textcoins.text = "Coins :" + coins.ToString();3
             textcoins.text = coins.ToString() + " :";
    }
    
    private void Update()
    {
        textcoins.text = coins.ToString() + " :";
        int cois = PlayerPrefs.GetInt("coins");
        if (coins!= cois)
        {
            PlayerPrefs.SetInt("coins", coins);
 
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "coins")
        {
            
           
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
    }
}
