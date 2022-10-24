using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    public Prize prize;
    private Button yourButton;
    public Image imageCoins;
    public Image imageSkin;
    public int numberCoins;
    private bool onclick= false;
    public Text numberCoinstxt;
    public bool Coins;
    public int id;
    

    public void Init()
    {
        yourButton = GetComponent<Button>();
        yourButton.onClick.AddListener(onchest);
        Debug.Log("so lan");
        if (Coins)
        {
            numberCoins = Random.Range(500, 3001);
            numberCoinstxt.text = numberCoins.ToString();
            

        }
        else
        {
            Debug.Log("yeu em");
        }
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
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
                //gameObject.transform.GetChild(1).gameObject.SetActive(true);
                if (Coins)
                {

                }
            }
            onclick = true;
        }

    }


   
}
