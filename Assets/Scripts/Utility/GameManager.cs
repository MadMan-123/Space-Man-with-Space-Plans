using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum GameState
{
    MainMenu,
    ScrollingShooter,
    TopDownShooter,
};
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] Vector2 ScrollShooterRes = new Vector2(400, 800);
    [SerializeField] Vector2 MainMenuRes = new Vector2(1920, 1080);
    [SerializeField] Vector2 TopDownShooterRes = new Vector2(1920, 1080);
    [SerializeField] float fRange = 75f;
    [SerializeField] FlickerScript LeavingPlanetMessage;
    [SerializeField] GameObject LeavingAnimation;
    static public GameState state;

    bool bCoroutineIsRunning = false;
    bool bRun = true;
    Health health;


    void Awake()
    {
        //singleton boilerplate 

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }



    }

    void Start()
    {
        LoadIntoMainMenu();

    }

    void Update()
    {
        switch (state)
        {
            case GameState.MainMenu:

                if (ScoreManager.Instance)
                    ScoreManager.Instance.AddScore(-ScoreManager.Instance.GetScore());

                break;
            case GameState.TopDownShooter:
                if (!LeavingPlanetMessage)
                    LeavingPlanetMessage = GameObject.FindObjectOfType<FlickerScript>();
                //check distance of player to (0,0) if out of range load into Scrolling shooter
                GameObject Player = GameObject.FindGameObjectWithTag("Player");

                if (Player)
                {
                    Vector2 PlayerPos = Player.transform.position;

                    float fSqrMag = (Vector2.SqrMagnitude(Vector2.zero - PlayerPos));
                    //using sqr magnitude for cheaper processing
                    if (fSqrMag > fRange * fRange)
                    {

                        if (bRun)
                        {

                            bRun = false;
                            if (Player.TryGetComponent<HideBody>(out HideBody hideBody))
                            {
                                if (Player.TryGetComponent<Health>(out health)) ;

                                hideBody.StartHideBody();



                            }

                            //spawn in animation
                            Instantiate(LeavingAnimation, Player.transform.position, Player.transform.rotation);


                            if (health && health.GetHealth() > 0)
                            {
                                Invoke(nameof(LoadIntoScrollingShooter), 2f);
                            }

                        }


                    }
                    else if (!bCoroutineIsRunning && fSqrMag > (fRange / 3 * 2) * (fRange / 3 * 2) && fSqrMag < fRange * fRange)
                    {

                        StartCoroutine(nameof(FlickerMessage), 2f);
                    }
                    else
                    {
                        LeavingPlanetMessage.ObjToFlicker.SetActive(false);
                    }


                    if (health && health.GetHealth() <= 0)
                    {
                        CancelInvoke(nameof(LoadIntoScrollingShooter));
                    }
                }

                break;
        }


    }


    public void LoadIntoMainMenu()
    {
        StopCoroutine(nameof(FlickerMessage));
        Debug.Log("Loading Into menu");

        SceneManager.LoadScene(0);
        state = GameState.MainMenu;
        Screen.SetResolution((int)MainMenuRes.x, (int)MainMenuRes.y, true);
        ShowCursor();


        //stop music
        if (TryGetComponent<AudioSource>(out AudioSource source))
        {
            source.loop = false;
            source.Stop();
        }

    }
    public void LoadIntoScrollingShooter()
    {

        StopCoroutine(nameof(FlickerMessage));

        Debug.Log("Loading Into scrolling shooter");

        SceneManager.LoadScene(1);

        state = GameState.ScrollingShooter;

        Screen.SetResolution((int)ScrollShooterRes.x, (int)ScrollShooterRes.y, true);


        //play music
        if (TryGetComponent<AudioSource>(out AudioSource source))
        {
            source.loop = true;
            source.Play();
        }

        //check if instance exists
        if (ScoreManager.Instance)
        {
            Invoke(nameof(UpdateScore), .5f);
        }

        HideCursor();


    }

    public void LoadIntoPlanet()
    {
        bRun = true;
        StopCoroutine(nameof(FlickerMessage));

        //find new objects 

        Debug.Log("Loading Into Planet");
        //change the res of the screen
        SceneManager.LoadScene(2);
        state = GameState.TopDownShooter;

        Screen.SetResolution((int)TopDownShooterRes.x, (int)TopDownShooterRes.y, true);

        //set rand seed


        if (ScoreManager.Instance)
        {
            Invoke(nameof(UpdateScore), .5f);
        }

        ShowCursor();

        Invoke(nameof(GenMap), .1f);
    }

    void GenMap()
    {
        if (MapGen.Instance)
            MapGen.Instance.StartGenMap();
    }

    void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    void UpdateScore()
    {
        Debug.Log("Updating Score");
        ScoreManager.Instance.UpdateTextOBJ();
        ScoreManager.Instance.UpdateScore();
    }
    public void SavePlayerData()
    {

        //save the high score
        SaveSytem.SavePlayerData(ScoreManager.Instance, "Bill");
        Debug.Log($"HighScore: {SaveSytem.LoadPlayerData().iHighScore}");
    }

    IEnumerator FlickerMessage()
    {
        bCoroutineIsRunning = true;
        if (state != GameState.TopDownShooter || LeavingPlanetMessage == null) yield return null;
        LeavingPlanetMessage.ObjToFlicker.SetActive(false);
        yield return new WaitForSecondsRealtime(1.5f);
        if (state != GameState.TopDownShooter || LeavingPlanetMessage == null) yield return null;
        LeavingPlanetMessage.ObjToFlicker.SetActive(true);
        bCoroutineIsRunning = false;

    }
}
