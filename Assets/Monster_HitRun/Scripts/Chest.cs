using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    public Prize prize;
    public PlayerManager CoinsPlayer;
    private Button yourButton;
    public int numberCoins;
    private bool onclick= false;
    public Text numberCoinstxt;
    public bool Coins;
    public int id;
    

    public void Init()
    {
        
        Debug.Log("so lan");
        if (Coins)
        {
            numberCoins = Random.Range(500, 3001);
            numberCoinstxt.text = numberCoins.ToString();
            

        }
        else
        {
        }
    }
    private void Start()
    {
        yourButton = GetComponent<Button>();
        yourButton.onClick.AddListener(onchest);
    }


    public void onchest()
    {
        if(!onclick)
        {
            if (prize.numberprize > 0)
            {

                prize.numberprize--;
                Debug.Log("da mo chest");
                //gameObject.SetActive(false);
                
                if (Coins)
                {
                    gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    gameObject.transform.GetChild(2).gameObject.SetActive(false);
                }
                else
                {
                    gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    gameObject.transform.GetChild(1).gameObject.SetActive(false);
                    gameObject.transform.GetChild(2).gameObject.SetActive(true);
                }

            }
            
            onclick = true;
        }
        
    }
    private void Update()
    {
        if(prize.numberprize <= 0)
        {
            StartCoroutine("endChest");
        }

    }

    IEnumerator  endChest()
    {
        yield return new WaitForSeconds(0.5f);
        if (Coins)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            gameObject.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            gameObject.transform.GetChild(2).gameObject.SetActive(true);
        }
        prize.Chestkey.SetActive(false);
        prize.Chestvideo.SetActive(true);
    }

}
