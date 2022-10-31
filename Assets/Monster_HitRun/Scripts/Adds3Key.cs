using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Adds3Key : MonoBehaviour
{
    private Button yButton;
    [SerializeField] private GameObject PanelPrize;

    void Start()
    {
        yButton = GetComponent<Button>();
        yButton.onClick.AddListener(click);
    }

    private void click()
    {
        Destroy(PanelPrize);
        GameObject prize = Instantiate(Resources.Load<GameObject>("UI/PanelPrizesvideo" ));
        Debug.Log("da vao");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
