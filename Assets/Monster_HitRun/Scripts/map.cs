using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class map : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Mapparent;
    public GameObject Trap_parent;
    public GameObject Loxo_parent;
    public GameObject Coins_Parent;

    public GameObject Coins;
    public GameObject Trap_gai;
    public GameObject Lo_xo;
    public GameObject objectmap;
    public GameObject road_1map;
    public GameObject road_2map;
    public GameObject wall_right;
    public GameObject wall_left;

    public int soluong;
    public float khoangcach;

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
        if(Mapparent.transform.childCount==0)
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
            obj = Instantiate(objectmap, vitribatdau,objectmap.transform.rotation,Mapparent.transform);
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
        obj = Instantiate(road_1map, vitribatdau, road_1map.transform.rotation,Mapparent.transform);
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
        obj = Instantiate(road_2map, vitribatdau, road_2map.transform.rotation,Mapparent.transform);
        stackPool.Push(vitribatdau.z);

    }
    public void creatwall_right()
    {
        GameObject obj;
        vitribatdau.z = stackPool.Peek();
        vitribatdau.y = 0;
        vitribatdau.x = 0;
        vitribatdau.z -= 157f + 7f;
        obj = Instantiate(wall_right, vitribatdau, wall_right.transform.rotation,Mapparent.transform);
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
        obj = Instantiate(wall_left, vitribatdau, wall_left.transform.rotation,Mapparent.transform);
        vitribatdau.z -= 17;
        stackPool.Push(vitribatdau.z);
    }
    public void back()
    {
        stackPool.Pop();
        //delete();
    }
    public void creat_Trap()
    {
        for (int i = 0; i < soluong; i++)
        {
            GameObject obj;          
            obj = Instantiate(Trap_gai, vitribatdau, Trap_gai.transform.rotation, Trap_parent.transform);
            vitribatdau.z += khoangcach;
        }
    }
    public void creat_Loxo()
    {
        for (int i = 0; i < soluong; i++)
        {
            vitribatdau.y = 0.2f;
            GameObject obj;
            obj = Instantiate(Lo_xo, vitribatdau, Lo_xo.transform.rotation, Loxo_parent.transform);
            vitribatdau.z += khoangcach;
        }
    }
    public void creat_Coins()
    {
        for (int i = 0; i < soluong; i++)
        {
            vitribatdau.y = 0;
            GameObject obj;
            obj = Instantiate(Coins, vitribatdau, Coins.transform.rotation, Coins_Parent.transform);
            vitribatdau.z += khoangcach;
        }
    }

    public void createmty()
    {
        vitribatdau.z += 7;
        stackPool.Push(vitribatdau.z);
    }

}
