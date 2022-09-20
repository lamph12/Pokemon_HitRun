using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class map : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject objectmap;
    public int soluong;
    public float khoangcach;
    //public float vitriz;
    public Vector3  vitribatdau = new Vector3(0, 0,-84 );

    // Update is called once per frame
    void Start()
    {
       
        //taomap();
    }


    public void taomap()
    {
        Debug.Log(vitribatdau.z);
        for(int i = 0; i < soluong; i++)
        {
            GameObject obj;
            obj = Instantiate(objectmap, vitribatdau,objectmap.transform.rotation);
            vitribatdau.z+=7;
        }
    }

}
