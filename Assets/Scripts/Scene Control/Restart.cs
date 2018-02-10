using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Restart : MonoBehaviour
{
    [SerializeField]
    private AudioSource CloseAudio;

    [SerializeField]
    private AudioSource PauseAudio;

    [SerializeField]
    private GameObject TargetMenu;

    public void RestartFunc()
    {
        if (FindObjectOfType<Spawner>() != null)
        {
            var tempArray = GameObject.FindGameObjectsWithTag("Group");
            foreach (var gObj in tempArray)
            {
                Destroy(gObj);
            }
            FindObjectOfType<Spawner>().SpawnNext();
            MenuInGaming.Paused = false;
            TargetMenu.SetActive(false);
            CloseAudio.Play();
            MenuInGaming.IsShowing = !MenuInGaming.IsShowing;
            Time.timeScale = MenuInGaming.Paused ? 0 : 1;
        }
        else
        {
            SceneManager.LoadScene("GamingScene");
        }
        //PublicClass.Score = 0;
        //PublicClass.Level = 0;
        //PublicClass.Rows = 0;
        Group.RefreshInfo();
    }

    // Use this for initialization
    private void Start()
    {
        //RestartBtn.onClick.AddListener(RestartFunc);
    }

    // Update is called once per frame
    private void Update()
    {
        //RestartBtn
    }
}