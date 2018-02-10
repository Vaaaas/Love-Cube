using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PublicClass : MonoBehaviour {
    public static bool isStandardMode;
    [SerializeField]
    public GameObject GameOverMenu;

    [SerializeField] public Text FinalScore;
    [SerializeField]
    public Text FinalLevel;

    [SerializeField]
    public Text TimeText;

    [SerializeField] public GameObject LevelUp;

    public static bool IsGameOver;

    private float appearTime;


    public static void ClearField()
    {
        var tempArray = GameObject.FindGameObjectsWithTag("Group");
        foreach (var gObj in tempArray)
        {
            Object.Destroy(gObj);
        }
    }

    public static void StartGame()
    {
        //Score = 0;
        //Level = 0;
        //Rows = 0;
        MenuInGaming.Paused = false;
        MenuInGaming.IsShowing = false;
        if (Object.FindObjectOfType<Spawner>() != null)
        {
            Object.FindObjectOfType<Spawner>().SpawnNext();
        }
        else
        {
            SceneManager.LoadScene("GamingScene");
        }
    }

    public static void BackToStartScreen()
    {
        SceneManager.LoadScene("Map");
    }

    public void BackTOMap()
    {
        SceneManager.LoadScene("Map");
    }

    // Use this for initialization
    void Start () {
        Player.timeLast = 600;
        Player.timeBuffer = Time.time;
        Player.LevelBuffer = Player.Level;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update () {
        if (Player.timeLast <= 0.0|| IsGameOver)
        {
            GameOverMenu.SetActive(true);
            Time.timeScale = 0;
            FinalScore.text = "Your Score : " + Player.Score;
            if ((Player.Level - Player.lastLevel) != 0)
            {
                FinalLevel.text = "Your Level : " + Player.lastLevel + " + " + (Player.Level - Player.lastLevel);
            }
            else
            {
                FinalLevel.text = "Your Level : " + Player.Level;
            }
            //SceneManager.LoadScene("Map");
        }
        if (Player.Level > Player.LevelBuffer)
        {
            LevelUp.SetActive(true);
            appearTime = Time.time;
            Player.LevelBuffer = Player.Level;
        }
        if (Time.time - appearTime >= 8)
        {
            LevelUp.SetActive(false);
        }

        Player.timeLast = Player.timeLast - (Time.time - Player.timeBuffer);
        Player.timeBuffer = Time.time;
        TimeText.text = Player.getLastTime();

        if (!isStandardMode && Player._Rows == 15)
        {
            for (int i = 0; i < 15; i++)
            {
                Grid.DeleteSurface(i);
                Grid.initSurAct();
            }
            Player._Rows = 0;
            FindObjectOfType<Spawner>().SpawnNext();
        }
    }
}
