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
    public PlayerManager CheckWin;
    public MenuManager menuManager;


    public StateGame state;

    [Header("Safe Area")] public SafeArea safeArea;

    private void Update()
    {

    }

    protected override void OnAwake()
    {
        GameController.Instance.currentScene = SceneType.GamePlay;

        Init();
    }

    public void Init()
    {
#if UNITY_IOS
if (safeArea != null)
            safeArea.enabled = true;
#endif

        //playerContain.Init(); 

        InitLevel(GameController.Instance.useProfile.CurrentLevelPlay);
        menuManager.Init();
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

        if (CheckWin.Win=true)
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
            Initiate.Fade(SceneName.GAME_PLAY, Color.black, 3f);
        }
    }
    public void PlayAnimFly()
    {
    }

    public void PlayAnimFlyOut()
    {
    }
}