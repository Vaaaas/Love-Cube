using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MapControl : MonoBehaviour {
    [SerializeField]public GameObject[] BoyBtns=new GameObject[4];
    [SerializeField]
    public GameObject[] GirlBtns = new GameObject[3];

    public void StartTutorial(bool isBoy)
    {
        SceneManager.LoadScene("StandardMode");
    }

    public void StartStandard()
    {
        SceneManager.LoadScene("StandardMode");
    }

    public void StartHigher()
    {
        SceneManager.LoadScene("HigherMode");
    }

    public void StartPuzzle()
    {
        SceneManager.LoadScene("HigherMode");
    }

    public void ExitGame()
    {
        Application.Quit();
    }


    // Use this for initialization
    void Start () {
        if (Player.IsBoy)
        {
            for (int i = 0; i < 3; i++)
            {
                GirlBtns[i].SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                BoyBtns[i].SetActive(false);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
