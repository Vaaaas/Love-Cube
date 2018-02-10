using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuInGaming : MonoBehaviour
{
    public static bool Paused;
    public static bool IsShowing;

    [SerializeField] private AudioSource CloseAudio;
    [SerializeField] private GameObject ContinueBtn;

    [SerializeField] private Button ExitBtn;

    [SerializeField] private AudioSource PauseAudio;

    [SerializeField] private GameObject TargetMenu;
    [SerializeField]
    private GameObject GameOverMenu;


    public void BackToStartScreen()
    {
        PublicClass.ClearField();
        PublicClass.StartGame();
        PublicClass.BackToStartScreen();
    }


    // Use this for initialization
    private void Start()
    {
        //ContinueBtn.GetComponent<Button>().onClick.AddListener(Continue);
        //ExitBtn.onClick.AddListener(BackToStartScreen);
    }

    public void Continue()
    {
        Paused = false;
        TargetMenu.SetActive(false);
        CloseAudio.Play();
        IsShowing = !IsShowing;
        Time.timeScale = Paused ? 0 : 1;
    }

    public void ReturnMenu()
    {
        Player.timeLast = 600;
        Player.timeBuffer = Time.time;
        Player.LevelBuffer = Player.Level;
        Time.timeScale = 1;
        Paused = false;
        SceneManager.LoadScene("Map");
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)||Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            if (IsShowing)
            {
                Continue();
            }
            else
            {
                Paused = true;
                TargetMenu.SetActive(true);
                PauseAudio.Play();
                IsShowing = !IsShowing;
                EventSystem.current.SetSelectedGameObject(ContinueBtn);
            }
            Time.timeScale = Paused ? 0 : 1;
        }
    }
}