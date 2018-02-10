using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    [SerializeField] public GameObject Title;
    [SerializeField] public GameObject Story;
    // Use this for initialization
    private void Start()
    {

    }

    private static void InitPlayer(bool IsBoy)
    {
        Player.IsBoy = IsBoy;
    }

    public void SelectBoy()
    {
        InitPlayer(true);
        SceneManager.LoadScene("Map");
    }

    public void SelectGirl()
    {
        InitPlayer(false);
        SceneManager.LoadScene("Map");
    }

    // Update is called once per frame
    private void Update()
    {
        if (Title.activeSelf && Input.anyKeyDown)
        {
            Title.SetActive(false);
            Story.SetActive(true);
        }
    }
}   