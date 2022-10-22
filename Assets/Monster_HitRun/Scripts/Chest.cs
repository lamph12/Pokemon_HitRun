using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    public Prize prize;
    private Button yourButton;
    private bool onclick= false;
    public Text numbercoins;
    public class Giftypessss
    {
        bool coins;
        int number;
        Sprite icon;
    }

    void Start()
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
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
                gameObject.transform.GetChild(1).gameObject.SetActive(true);
            }
            onclick = true;
        }

    }
   
}
