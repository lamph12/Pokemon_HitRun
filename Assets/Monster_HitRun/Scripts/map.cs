using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class map : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject objectmap;
    public GameObject road_1map;
    public GameObject road_2map;
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
            vitribatdau.y = 0;
            vitribatdau.x = 0;
            obj = Instantiate(objectmap, vitribatdau,objectmap.transform.rotation);
            vitribatdau.z+=7;
        }
    }
    public void road_1()
    {
        GameObject obj;
        vitribatdau.y = 0.15f;
        vitribatdau.x = -1.5f;
        obj = Instantiate(road_1map, vitribatdau, road_1map.transform.rotation);
        vitribatdau.z += 22.5f;
    }
    public void road_2()
    {
        GameObject obj;
        vitribatdau.y = 0.22f;
        vitribatdau.x = -2.6f;
        vitribatdau.z +=29.5f-7f;
        obj = Instantiate(road_2map, vitribatdau, road_2map.transform.rotation);


    }

}
