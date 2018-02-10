using UnityEngine;
using UnityEngine.SceneManagement;
using Random = MersenneTwister.Random;

public class Spawner : MonoBehaviour
{
    //Groups
    public GameObject[] groups;

    //[SerializeField] private AudioSource RotateAudio;

    public void SpawnNext()
    {
        //Random Generator
        var randGen = new Random();

        //Random Index
        var i = randGen.GetRandom(7);

        //Throw the group into the world by using Instantiate( original, position, rotation )
        //original ----- An existing object that you want to make a copy of
        //position ----- Position of the new object
        //rotation ----- Orientation of the new object
        Instantiate(groups[i], transform.position, Quaternion.identity);
        //transform.position is the Spawner's position; Quaternion.identity is the default rotation
    }

    // Use this for initialization
    private void Start()
    {
        //Spawn initial Group
        SpawnNext();
        if (SceneManager.GetActiveScene().name != "StandardMode")
        {
            PublicClass.isStandardMode = false;
            Grid.initSurAct();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.UpArrow) && !Pauser.Paused)
        //{
        //    RotateAudio.Play();
        //}
    }
}