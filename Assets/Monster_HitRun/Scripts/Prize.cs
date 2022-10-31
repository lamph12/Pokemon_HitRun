using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prize : MonoBehaviour
{
    public int numberprize =3;
    public List<Chest> listchest;
    public GameObject Chestvideo;
    public GameObject Chestkey;
    public GameObject NoThanks;
    int numberrandom;
    


    private Button nothanks;
    void Start()
    {
        numberrandom = Random.Range(1, 10);
        nothanks = NoThanks.GetComponent<Button>();
        nothanks.onClick.AddListener(off);
        Debug.Log(listchest.Count);

        foreach(var lis in listchest)
        {

            if (lis.id != numberrandom)
            {
                lis.Coins = true;
                lis.Init();
            }
            else
            {
                lis.Coins = false;
                lis.Init();
            }
        }
    }

   

    // Update is called once per frame
   
    public void off()
    {
        gameObject.SetActive(false);
        GraphicRaycaster menu;
        menu = GamePlayController.Instance.menuManager.GetComponent<GraphicRaycaster>();
        menu.enabled = true;
        GamePlayController.Instance.menuManager.BonusEndgame.gameObject.SetActive(true);
    }


}
