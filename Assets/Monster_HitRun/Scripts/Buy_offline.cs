using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buy_offline : MonoBehaviour
{
    
    public int usecoinsOffline;
    int Propertiesoffline = 0;
    private int coinsupOffline;
    [SerializeField]private Text usecoinsOfflinetext;
    [SerializeField]private Text Propertiesofflinetext;
    [SerializeField] private ParticleSystem up;


    
    public void Init()
    {
        coinsupOffline = 50;
        Propertiesoffline = 5;
        //coinsupOffline = PlayerPrefs.GetInt("coinsupOffline");
        //usecoinsOffline = PlayerPrefs.GetInt("usecoinsOffline");
        //Propertiesoffline = PlayerPrefs.GetInt("Propertiesoffline");

    }
    private void Update()
    {
        
        usecoinsOfflinetext.text = usecoinsOffline.ToString();
        Propertiesofflinetext.text = Propertiesoffline.ToString();

    }
    public void Buy()
    {
        if (CoinPicker.coinPicker.coins >= usecoinsOffline)
        {
            up.Play();
            CoinPicker.coinPicker.coins -= usecoinsOffline;
            PlayerPrefs.SetInt("coins", CoinPicker.coinPicker.coins);
            Propertiesoffline +=5;
            coinsupOffline = 25;
            usecoinsOffline += coinsupOffline;

            PlayerPrefs.SetInt("coinsup", coinsupOffline);
            PlayerPrefs.SetInt("usecoinsOffline", usecoinsOffline);
            PlayerPrefs.SetInt("Propertiesoffline", Propertiesoffline);
        }
    }
}

