using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prize : MonoBehaviour
{
    private bool key;
    public int numberprize =3;
    public List<Chest> listchest;
    [SerializeField]private GameObject Chestvideo;
    [SerializeField] private GameObject Chestkey;
    [SerializeField] private GameObject NoThanks;
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
            Debug.Log("kakakakakak");
            
            if (lis.id!=numberrandom)
            {
                Debug.Log("da vao 1");
                lis.Coins = true;
                lis.Init();
            }
            else
            {
                Debug.Log("da vao 2");
                lis.Coins = false;
                lis.Init();
            }
        }
    }

   

    // Update is called once per frame
    void Update()
    {
        if (numberprize == 0)
        {
            Chestkey.SetActive(false);
            Chestvideo.SetActive(true);
        }
    }
    public void off()
    {
        gameObject.SetActive(false);
    }


}
