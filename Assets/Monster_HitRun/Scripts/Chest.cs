using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Chest : MonoBehaviour
{
    public Prize prize;
    public CoinPicker CoinsPlayer;
    private Button yourButton;
    public int numberCoins;
    private bool onclick= false;
    public Text numberCoinstxt;
    public bool Coins;
    private bool afterChest=true;
    public int id;
    public Image coinsMove;
    public Transform CoinsPos;
    private Image coinstarget;

    public Canvas canvas;


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
        //canvas = GameObject.Find("MenuManager").GetComponent<Canvas>();
        coinstarget = GameObject.Find("ImageCOINS").GetComponent<Image>();
    }
    private void Start()
    {
       
        yourButton = GetComponent<Button>();                      

    }


    public void onchest()
    {
        if(!onclick)
        {
            if (prize.numberprize > 0)
            {

                prize.numberprize--;
                Debug.Log("da mo chest");
                
                if (Coins)
                {
                    gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    gameObject.transform.GetChild(2).gameObject.SetActive(false);
                    onClick();
                }
                else
                {
                    gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    gameObject.transform.GetChild(1).gameObject.SetActive(false);
                    gameObject.transform.GetChild(2).gameObject.SetActive(true);
                }
                CoinPicker.coinPicker.coins += numberCoins;
            }
            
            
            onclick = true;
            
        }
        
    }
    private void Update()
    {
        if(prize.numberprize <= 0 && afterChest)
        {
            StartCoroutine("endChest");
        }

    }

    IEnumerator  endChest()  
    {
       
        yield return new WaitForSeconds(0.3f);
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
        afterChest = false;
        yield return new WaitForSeconds(1.2f);
        afterChestEnd();
        


    }

    void afterChestEnd()
    {
       
        if (!onclick)
        {
            if (Coins)
            {
                gameObject.transform.GetChild(1).gameObject.SetActive(false);
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                
            }
            else
            {
                gameObject.transform.GetChild(2).gameObject.SetActive(false);
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                
            }
           
        }
        
    }
    void onClick()
    {
        for (int i = 0; i < 30; i++)
        {
            Debug.Log("da vao");
            Image image = Instantiate(coinsMove);
            image.transform.SetParent(canvas.transform);
            image.transform.position = new Vector2(Random.Range(CoinsPos.position.x, CoinsPos.position.x + 474), Random.Range(CoinsPos.position.y, CoinsPos.position.y + 150));
            image.transform.DOMove(coinstarget.transform.position, 1.5f).OnComplete(() => Destroy(image));
            //image.transform.DOMove()

        }
    }

    public void StartClick()
    {
        if(prize.numberprize>0)
        onchest();
    }
    
}
