using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class map : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Map;
    public GameObject objectmap;
    public GameObject road_1map;
    public GameObject road_2map;
    public GameObject wall_right;
    public GameObject wall_left;
    public int soluong;
    public float khoangcach;
    //public float vitriz;
    public Vector3 vitribatdau;
    public Stack<float> stackPool = new Stack<float>();
    

    // Update is called once per frame
    void Start()
    {
        vitribatdau = new Vector3(0, 0, -84);
        
    }


    public void taomap()
    {
        Debug.Log(stackPool.Count);
        if(Map.transform.childCount==0)
            stackPool.Push(-84);
        if (stackPool.Count <= 0)
        {
            stackPool.Push(-84);
        }
        Debug.Log(vitribatdau.z);
        for(int i = 0; i < soluong; i++)
        {
            vitribatdau.z= stackPool.Peek();
            GameObject obj;
            vitribatdau.y = 0;
            vitribatdau.x = 0;
            obj = Instantiate(objectmap, vitribatdau,objectmap.transform.rotation,Map.transform);
            vitribatdau.z+=khoangcach;
            stackPool.Push(vitribatdau.z);
        }
    }
    public void road_1()
    {
        vitribatdau.z = stackPool.Peek();
        GameObject obj;
        vitribatdau.y = 0.15f;
        vitribatdau.x = -1.5f;
        obj = Instantiate(road_1map, vitribatdau, road_1map.transform.rotation,Map.transform);
        vitribatdau.z += 22.5f;
        stackPool.Push(vitribatdau.z);

    }
    public void road_2()
    {
        vitribatdau.z = stackPool.Peek();
        GameObject obj;
        vitribatdau.y = 0.22f;
        vitribatdau.x = -2.6f;
        vitribatdau.z +=29.5f-7f;
        obj = Instantiate(road_2map, vitribatdau, road_2map.transform.rotation,Map.transform);
        stackPool.Push(vitribatdau.z);

    }
    public void creatwall_right()
    {
        GameObject obj;
        vitribatdau.z = stackPool.Peek();
        vitribatdau.y = 0;
        vitribatdau.x = 0;
        vitribatdau.z -= 157f + 7f;
        obj = Instantiate(wall_right, vitribatdau, wall_right.transform.rotation,Map.transform);
        vitribatdau.z += 192f;
        stackPool.Push(vitribatdau.z);
    }
    public void creatwall_left()
    {
        GameObject obj;
        vitribatdau.z = stackPool.Peek(); 
        vitribatdau.y = 0;
        vitribatdau.x = 0;
        vitribatdau.z +=52-7f;
        obj = Instantiate(wall_left, vitribatdau, wall_left.transform.rotation,Map.transform);
        vitribatdau.z -= 17;
        stackPool.Push(vitribatdau.z);
    }
    public void back()
    {
        stackPool.Pop();
        //delete();
    }
    //public void delete()
    //{
        
    //    DestroyImmediate(Map.transform.GetChild(Map.transform.childCount - 1));
    //}
    public void createmty()
    {
        vitribatdau.z += 7;
        stackPool.Push(vitribatdau.z);
    }

}
