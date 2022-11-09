//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class creat_enemy : MonoBehaviour
//{
//    public int lvenemies;
//    public bool shotted;
//    private Rigidbody rgEnemies;
//    private Collider collider;
//    public int soluong;
//    private Vector3 vitribatdau;
//    public GameObject Chick;

//    private void Start()
//    {
//        vitribatdau = transform.position;
//        shotted = false;
//        rgEnemies = GetComponent<Rigidbody>();
//        collider = transform.gameObject.GetComponent<Collider>();
//        //Innit();

//    }

//    private void OnTriggerEnter(Collider other)
//    {

//        if (other.gameObject.tag == "Ball")
//        {
//            if (PlayerManager.PlayerManagerIstance.lvPlayer >= lvenemies)
//                PlayerManager.PlayerManagerIstance.lvPlayer = PlayerManager.PlayerManagerIstance.lvPlayer + lvenemies;
//            else
//                PlayerManager.PlayerManagerIstance.lvPlayer -= 10;
//            shotted = true;
//        }
//        if (other.gameObject.tag == "Player")
//        {
//            if (PlayerManager.PlayerManagerIstance.lvPlayer < lvenemies)
//            {
//                other.gameObject.SetActive(false);
//                GamePlayController.Instance.menuManager.GameStace = false;
//                if (PlayerManager.PlayerManagerIstance.numberKey == 3)
//                {
//                    GameObject prize = Instantiate(Resources.Load<GameObject>("UI/PanelPrizesnomal"));
//                    GraphicRaycaster menu;
//                    menu = GamePlayController.Instance.menuManager.GetComponent<GraphicRaycaster>();
//                    menu.enabled = false;
//                }
                    
//                else
//                GamePlayController.Instance.menuManager.BonusEndgame.gameObject.SetActive(true);
//            }
//        }
//    }

   
//    //public void Innit()
//    //{
//    //    for(int i = 0; i < soluong; i++)
//    //    {
//    //        vitribatdau.z += 2.5f;
//    //        Instantiate(Chick, vitribatdau, Chick.transform.rotation);
//    //    }
//    //}



//}
