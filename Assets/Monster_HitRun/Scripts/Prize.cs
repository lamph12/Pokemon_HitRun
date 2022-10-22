using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prize : MonoBehaviour
{
    private bool key;
    public int numberprize =3;
    public List<GameObject> listchest = new List<GameObject>();
    [SerializeField]private GameObject Chestvideo;
    [SerializeField] private GameObject Chestkey;
    [SerializeField] private GameObject NoThanks;

    


    private Button nothanks;
    void Start()
    {
        nothanks = NoThanks.GetComponent<Button>();
        nothanks.onClick.AddListener(off);
        for(int i = 0; i < listchest.Count; i++) { 

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
