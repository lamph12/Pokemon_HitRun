using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class MiniGame: MonoBehaviour
{
    //private Transform transform;
    private int Coins;
    public Text CoinsEarned_txt;
    public GameObject x2_img;
    public GameObject x3_img;
    public GameObject x5_img;
    public Text xCoins_txt;
    public Text xCoinsMini_txt;

    private int CoinsX2;
    private int CoinsX3;
    private int CoinsX5;

    void Start()
    {   

        Coins = BonusEndgame.BonusEndgameInstance.CoinsEarned;
        transform.DOLocalMoveX(-227, 1.2f,false).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        CoinsEarned_txt.text = Coins.ToString();
        CoinsX2 = Coins * 2;
        CoinsX3 = Coins * 3;
        CoinsX5 = Coins * 5;
        
    }

    // Update is called once per frame
    void Update()
    {

        if ((transform.localPosition.x > 105 && transform.localPosition.x <=227)|| (transform.localPosition.x > -227 && transform.localPosition.x <= -105))
        {
            
            x2_img.SetActive(true);
            x3_img.SetActive(false);
            x5_img.SetActive(false);
            xCoins_txt.text = "x2";
            xCoinsMini_txt.text = CoinsX2.ToString();
        }
        if((transform.localPosition.x > 29 && transform.localPosition.x <= 105)|| (transform.localPosition.x > -105 && transform.localPosition.x < -29))
        {

            x2_img.SetActive(false);
            x3_img.SetActive(true);
            x5_img.SetActive(false);
            xCoins_txt.text = "x3";
            xCoinsMini_txt.text = CoinsX3.ToString();
        }
        if (transform.localPosition.x >= -29 && transform.localPosition.x<= 29)
        {

            x2_img.SetActive(false);
            x3_img.SetActive(false);
            x5_img.SetActive(true);
            xCoins_txt.text = "x5";
            xCoinsMini_txt.text = CoinsX5.ToString();
        }
    }
    public void Coinsreceived()
    {
        CoinPicker.coinPicker.coins += Coins;
        PlayerPrefs.SetInt("coins", CoinPicker.coinPicker.coins);
        StartCoroutine("wainttime");
    }
    IEnumerator wainttime()
    {
        yield return new WaitForSeconds(1);
        //GamePlayController.Instance.menuManager.Retry_btn();
        Initiate.Fade(SceneName.GAME_PLAY, Color.black, 3f);
       
    }
    public void KillTheAnimatioN()
    {
        transform.DOKill();
    }
}
