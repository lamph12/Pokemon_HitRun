using Crystal;
using UnityEngine;

public enum StateGame
{
    Loading = 0,
    Playing = 1,
    Win = 2,
    Lose = 3,
    Pause = 4,
    UnDecide = 5,
}

public class GamePlayController : Singleton<GamePlayController>
{
    private static bool isBannerShow;
    public PlayerContain playerContain;
    public GameScene gameScene;
    public MenuManager menuManager;
    public PlayerManager Player;


    public StateGame state;

    [Header("Safe Area")] public SafeArea safeArea;

    Vector3 touchPosWorld;

    //Change me to change the touch phase used.
    TouchPhase touchPhase = TouchPhase.Ended;

    void Update()
    {
        //We check if we have more than one touch happening.
        //We also check if the first touches phase is Ended (that the finger was lifted)
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == touchPhase)
        {
            //We transform the touch position into word space from screen space and store it.
            touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

            Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);

            //We now raycast with this information. If we have hit something we can process it.
            RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);

            if (hitInformation.collider != null)
            {
                //We should have hit something with a 2D Physics collider!
                GameObject touchedObject = hitInformation.transform.gameObject;
                //touchedObject should be the object someone touched.
                Debug.Log("Touched " + touchedObject.transform.name);
            }
        }
    }

    protected override void OnAwake()
    {
        //GameController.Instance.currentScene = SceneType.GamePlay;

        Init();
    }

    public void Init()
    {
#if UNITY_IOS
if (safeArea != null)
            safeArea.enabled = true;
#endif

        //playerContain.Init(); 
        menuManager.Init();
        Player.Init();
        Debug.Log(GameController.Instance.useProfile.CurrentLevelPlay);
        InitLevel(GameController.Instance.useProfile.CurrentLevelPlay);
        Debug.Log("da vao init");
        
        //gameScene.Init();
        {
            GameController.Instance.admobAds.DestroyBanner();
            GameController.Instance.admobAds.ShowBanner();
            isBannerShow = true;
        }
    }

    public void InitLevel(int indexLevel)
    {
        if (indexLevel > KeyPref.MAX_LEVEL)
        {
            indexLevel = KeyPref.MAX_LEVEL;
        }
        //Skip null level [BuildTest]

        //change current level here 
        GameObject level = Instantiate(Resources.Load<GameObject>("Levels/Level_" + indexLevel));
        Debug.Log(GameController.Instance.useProfile.CurrentLevelPlay);
        //{
        //    Debug.Log("da WIn");
        //    
        //}
        ////Load ra level theo Json
        if (Resources.Load<GameObject>("Levels/Level_" + indexLevel) != null)
        {
            //GamePlayController.Init();
            state = StateGame.Playing;
            
        }
        Initiate.Fade(SceneName.GAME_PLAY, Color.black, 3f);
    }



    public bool IsShowRate()
    {
        if (!UseProfile.CanShowRate)
            return false;
        var X = GameController.Instance.useProfile.CurrentLevelPlay - 1;
        if (X < RemoteConfigController.GetFloatConfig(FirebaseConfig.LEVEL_START_SHOW_RATE, 5))
            return false;
        if (X == RemoteConfigController.GetFloatConfig(FirebaseConfig.LEVEL_START_SHOW_RATE, 5) ||
            (X <= RemoteConfigController.GetIntConfig(FirebaseConfig.MAX_LEVEL_SHOW_RATE, 31) &&
             X % 10 == 1)) return true;
        return false;
    }

    public void NextLevel()
    {
        if (Player.Win == true)
        {

            //GameController.Instance.useProfile.CurrentLevelPlay++;
            Debug.Log("da WIn");

            var currentLevel = GameController.Instance.useProfile.CurrentLevelPlay;
            currentLevel += 1;
            if (currentLevel >= KeyPref.MAX_LEVEL)
            {
                currentLevel = KeyPref.MAX_LEVEL;
                GameController.Instance.useProfile.CurrentLevelPlay = currentLevel;

            }
            else
            {
                GameController.Instance.useProfile.CurrentLevelPlay = currentLevel;
                Debug.Log(GameController.Instance.useProfile.CurrentLevelPlay);
                Initiate.Fade(SceneName.GAME_PLAY, Color.black, 3f);

            }
        }
    }
    public void PlayAnimFly()
    {
    }

    public void PlayAnimFlyOut()
    {
    }
}