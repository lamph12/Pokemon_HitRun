using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class PlayerManager : MonoBehaviour
{
    public static PlayerManager PlayerManagerIstance;
    private float distance = 0;
    private Vector3 player;
    public bool InRun = false;

    //public MenuManager menuManager;

    public int lvPlayer;
    public Text textlv;

    public Camera cameramain;
    public Transform Ballpos;

    public float speed;
   
    private Touch touch;
    private float speedleftright = 0.016f;

    public bool Move = true;
    public Animator anim;
    private Rigidbody rigidbody;
    private Collider collider;

    public PathType pathsystem = PathType.Linear;
    private Vector3[] pathval = new Vector3[8];
    private GameObject[] pathval_ = new GameObject[8];
    private List<GameObject> listpoint = new List<GameObject>();
    private List<Vector3> listvector = new List<Vector3>();
    public LayerMask layerEnemy;
    public ParticleSystem particleSpeed;
    public bool powerspeed;

    public int numberKey;


    public bool Win;
    public bool therotation = true;
    public bool thewall = false;

    private void Awake()
    {
        
        rigidbody = gameObject.GetComponent<Rigidbody>();
        collider = gameObject.GetComponent<Collider>();
        anim = GetComponent<Animator>();
        PlayerManagerIstance = this;

    }
    public void Init()
    {
        //menuManager.Init();
        Win = false;
        therotation = true;
        //localtrans = GetComponent<Transform>();
        textlv.text = "Lv " + lvPlayer.ToString();
        
        thewall = false;
    }
    void Update()
    {
        transform.position = new Vector3((Mathf.Clamp(transform.position.x, -distance - 4.3f, distance + 4.3f)), transform.position.y, transform.position.z);
        textlv.text = "Lv " + lvPlayer.ToString();

        if (GamePlayController.Instance.menuManager.GameStace && Move)
        {
            //Debug.Log(GamePlayController.Instance.menuManager.GameStace);
            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.MoveTowards(transform.position.z, 1000, speed * Time.deltaTime));
            MovePlayer();
        }

        var ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 13))
        {
            Debug.DrawRay(transform.position, transform.forward * 13, Color.red);
            if (hit.transform.gameObject.tag == "enemies")
            {
               
                Enemies shot = hit.transform.gameObject.GetComponent<Enemies>();
                Collider rgenemies = hit.transform.gameObject.GetComponent<Collider>();
                if (!shot.shotted)
                {
                    if (lvPlayer >= shot.lvenemies)
                    {
                        shot.shotted = true;
                        rgenemies.isTrigger = true;
                        Pool_manager.Pool_managerInstance.spawnpool_enemy("bong", Ballpos, hit.transform.gameObject,shot.lvenemies);                       
                        anim.SetLayerWeight(1, 1f);
                    }

                }

            }

        }
        else
            anim.SetLayerWeight(1, 0f);

    }
    public void MovePlayer()
    {
        PlayRun();
        
        if (!thewall)
        {

            if (Input.touchCount > 0)
            {
                //Debug.Log("hehehe");
                touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved)
                {
                    transform.position = new Vector3(transform.position.x + touch.deltaPosition.x * speedleftright, transform.position.y, transform.position.z);

                }

            }
            
            if (transform.position.y > 10f)
            {
                Debug.Log("nhay xuong");
                rigidbody.velocity = new Vector3(0, -2.5f, 0);
            }
            
            }
        
        if (transform.position.y < -2f)
        {
            gameObject.SetActive(false);
            GamePlayController.Instance.menuManager.GameStace = false;
            if (PlayerManager.PlayerManagerIstance.numberKey == 3)
            {
                GameObject prize = Instantiate(Resources.Load<GameObject>("UI/PanelPrizesnomal"));
                GraphicRaycaster menu;
                menu = GamePlayController.Instance.menuManager.GetComponent<GraphicRaycaster>();
                menu.enabled = false;
            }

            else
                GamePlayController.Instance.menuManager.BonusEndgame.gameObject.SetActive(true);
        }
        //if (lvPlayer <= 0)
        //{
        //    gameObject.SetActive(false);
        //    GamePlayController.Instance.menuManager.GameStace = false;
        //    if (PlayerManager.PlayerManagerIstance.numberKey == 3)
        //    {
        //        GameObject prize = Instantiate(Resources.Load<GameObject>("UI/PanelPrizesnomal"));
        //        GraphicRaycaster menu;
        //        menu = GamePlayController.Instance.menuManager.GetComponent<GraphicRaycaster>();
        //        menu.enabled=false;
        //    }

        //    else
        //        GamePlayController.Instance.menuManager.BonusEndgame.gameObject.SetActive(true);
        //}

    }

    public void PlayRun()
    {
        anim.SetBool("IsRun", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag ("Lo_xo"))
        //{
        //    rigidbody.velocity = new Vector3(0, 15f,0);
        //    Debug.Log("aaaa");
        //    anim.SetBool("IsJump", true);
        //    anim.SetBool("IsRun", false);

        //    //rigidbody.velocity = new Vector3(0, 8.5f, -3);
        //    //Debug.Log("aaaa");
        //    //anim.SetBool("IsJump", true);
        //    //anim.SetBool("IsRun", false);

        //}
        if (other.CompareTag("Lo_xo_right"))
        {
            therotation = false;
            transform.rotation = Quaternion.Euler(0, 0, 0);

            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.MoveTowards(transform.position.z, 1000, speed * Time.deltaTime));
            Cameractl.CameractlIstance.rotationcamera = true;
            Cameractl.CameractlIstance.right = true;
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + 4.2f, transform.position.y + 2f, transform.position.z), 50 * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, 35f), 50f * Time.deltaTime);
            rigidbody.isKinematic = true;
            thewall = true;

        }
        if (other.CompareTag("coins"))
        {
            CoinPicker.coinPicker.coins++;
            //PlayerPrefs.SetInt("coins", coins);
            Destroy(other.gameObject);
            //Debug.Log("coins" + coins);
            //textcoins.text = coins.ToString() + " :";
        }
        if (other.CompareTag("Lo_xo_left"))
        {
            therotation = false;
            Cameractl.CameractlIstance.rotationcamera = true;
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + -4.75f, transform.position.y + 2f, transform.position.z), 50 * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, -35f), 50f * Time.deltaTime);
            rigidbody.isKinematic = true;
            thewall = true;

            //transform.position = new Vector3(transform.position.x + -3.6f, transform.position.y + 1.2f, transform.position.z);
            //transform.Rotate(0, 0, -25f);
            //rigidbody.isKinematic = true;
        }
        if (other.CompareTag("Speed"))
        {
            //rigidbody.velocity = new Vector3(0, 0, 20);
            //rigidbody.isKinematic = true;
            StartCoroutine("Speed");
            powerspeed = true;
            particleSpeed.Play();
        }
        //if (other.transform.tag == "Trap_gai")
        //{
        //    gameObject.SetActive(false);
        //    MenuManager.MenuManagerIstance.GameStace = false;
        //    MenuManager.MenuManagerIstance.YouLose.gameObject.SetActive(true);

        //}
        if (other.CompareTag( "in"))
        {

            Destroy(other.gameObject);
            Cameractl.CameractlIstance.flash = true;
            player = transform.position;
            distance = 500;
            transform.position = new Vector3(transform.position.x + 500f, transform.position.y, transform.position.z);

        }
        if (other.CompareTag("out"))
        {
            distance = 0;
            transform.position = player;
            Cameractl.CameractlIstance.flash = true;
        }

        if (other.CompareTag("road_1"))
        {
            GameObject child = other.transform.GetChild(0).gameObject;
            toList(child);
            Debug.Log("da vao");
            MovePaval(pathval_);
        }
        if (other.CompareTag("road1_1"))
        {
            GameObject child = other.transform.GetChild(0).gameObject;
            toList(child);
            MovePaval(pathval_);
            Debug.Log("da vao");
        }
        if (other.CompareTag("road_2_1"))
        {
            toList(other.gameObject);
            MovePaval(pathval_);
        }
        if (other.CompareTag ("road_2_2"))
        {

            toList(other.gameObject);
            MovePaval(pathval_);
        }
        

    }
  
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Plane"))
        {
            if (InRun)
            {
                anim.SetLayerWeight(1, 0f);
                anim.SetBool("IsRun", true);
                anim.SetBool("IsJump", false);
                anim.SetBool("IsJumpWall", false);
            }
            if (lvPlayer > 0 && lvPlayer <= 100)
            {
                transform.localScale = new Vector3(40, 40, 40);//scale0%
                speed = 17;
            }
            if (lvPlayer > 100 && lvPlayer <= 200)
            {
                transform.localScale = new Vector3(42, 42, 42);//scale 10%
                speed = 18;
            }
            if (lvPlayer > 200 && lvPlayer <= 300)
            {
                transform.localScale = new Vector3(44, 44, 44);//scale 20%
                speed = 19;
            }
            if (lvPlayer > 300 && lvPlayer <= 400)
            {
                transform.localScale = new Vector3(46, 46, 46); //scale 30%
                speed = 20;
            }
            if (lvPlayer > 400 && lvPlayer <= 500)
            {
                transform.localScale = new Vector3(50, 50, 50); //scale 40%
                speed = 21;
            }
            if (lvPlayer > 500)
            {
                transform.localScale = new Vector3(52, 52, 52);//scale  50%
                speed = 21.5f;
            }

        }
        if (collision.transform.CompareTag("Lo_xo"))
        {
            speed = 18.7f;
            rigidbody.velocity = new Vector3(0, 11f, -8f);
            Debug.Log("aaaa");
            anim.SetBool("IsJump", true);
            anim.SetBool("IsRun", false);

        }
        if (collision.transform.CompareTag("Lo_xo_Finish"))
        {
            rigidbody.velocity = new Vector3(0, 11f, 0);
            speed =19;
            Debug.Log("aaaa");
            anim.SetBool("IsJump", true);
            anim.SetBool("IsRun", false);

        }
        if (collision.transform.CompareTag( "Trap_gai"))
        {
            Debug.Log("vao trap gai");
            Move = false;
            rigidbody.isKinematic = true;
            collider.isTrigger = true;
            
            StartCoroutine(timeskip()); 

        }
    }

    public void ToListVector(GameObject[] pavalpoint)
    {
        for (int i = 0; i < pavalpoint.Length; i++)
        {
            pathval[i] = pavalpoint[i].transform.position;
        }
    }
    public void MovePaval(GameObject[] pavalpoint)
    {
        Move = false;
        ToListVector(pavalpoint);
        distance = 200;
        rigidbody.isKinematic = true;
        transform.DOPath(pathval, 1.5f, pathsystem).SetEase(Ease.Linear).OnComplete(() =>
       {
           Move = true;
           distance = 0;
           rigidbody.isKinematic = false;
       });
    }
    IEnumerator timeskip()
    {  
        if(lvPlayer>10)
        lvPlayer -= 10;
        else
        {
            gameObject.SetActive(false);
            GamePlayController.Instance.menuManager.GameStace = false;
            if (PlayerManager.PlayerManagerIstance.numberKey == 3)
            {
                GameObject prize = Instantiate(Resources.Load<GameObject>("UI/PanelPrizesnomal"));
                GraphicRaycaster menu;
                menu = GamePlayController.Instance.menuManager.GetComponent<GraphicRaycaster>();
                menu.enabled = false;
            }

            else
                GamePlayController.Instance.menuManager.BonusEndgame.gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(0.15f);
        Move = true;
        
        yield return new WaitForSeconds(0.5F);
        rigidbody.isKinematic = false;
        collider.isTrigger = false;
    }
    public void toList(GameObject dt)
    {
        Debug.Log(dt.transform.childCount);
        for (int i = 0; i < dt.transform.childCount; i++)
        {
            pathval_[i] = dt.transform.GetChild(i).gameObject;
        }
    }
    public IEnumerator Speed()
    {
        Debug.Log("da vao");
        rigidbody.isKinematic = true;
        
        speed += 10;
        yield return new WaitForSeconds(0.6f);
        rigidbody.isKinematic = false;
        speed -= 10 * Time.deltaTime;
        particleSpeed.Stop();
        powerspeed = false;
    }
    
}











