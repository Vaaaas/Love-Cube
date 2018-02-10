using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Group : MonoBehaviour
{
    private static Text ScoreText;

    private static double _deltaTime = 0.05*(11 - Player.Level);
    private const double moveBuffer = 0.2;
    private const int Difficulty = 15;
    private float _lastFall, _lastMove;


    //Verify each child block's position
    private bool IsValidGridPos()
    {
        //foreach (var vec in from Transform child in transform select Grid.RoundVec3(child.position))
        foreach(Transform child in transform)
        {
            Vector3 vec = Grid.RoundVec3(child.position);
            //Not inside border?
            if (!Grid.InsideBorder(vec))
            {
                return false;
            }

            //Have block in grid cell?
            //Not part of same group?
            //Debug.Log("Grid.grid[(int)vec.x, (int)vec.y].parent :  " + Grid.grid[(int)vec.x, (int)vec.y].parent);
            int x = (int) vec.x;
            int y = (int) vec.y;
            int z = (int) vec.z;
            if (z < 0 || z > Grid.Width - 1)
            {
                return false;
            }
            if (Grid.grid[x, y, z] != null && Grid.grid[x, y, z].parent != transform)
            {
                //Block's position is not empty and its parent is not this group
                return false;
            }
        }
        return true;
    }

    //After change the position of a group,
    //remove all the old block positions from the grid,
    //and add all the new block positions to the grid
    private void UpdateGrid()
    {
        //Remove old children from grid
        for (var y = 0; y < Grid.Height; y++)
        {
            for (var x = 0; x < Grid.Width; x++)
            {
                for (var z = 0; z < Grid.Width; z++)
                {
                    if (Grid.grid[x, y, z] != null) 
                    //there is something in this block
                    {
                        if (Grid.grid[x, y, z].parent == transform) 
                        {
                            //this grid's parent is 
                            Grid.grid[x, y, z] = null;
                        }
                    }
                }
            }
        }

        //Add new children to grid
        foreach (Transform child in transform)
        {
            Vector3 vec = Grid.RoundVec3(child.position);
            Grid.grid[(int) vec.x, (int) vec.y, (int) vec.z] = child;
        }
    }

    public static void RefreshInfo()
    {
        ScoreText.text = Player.Score.ToString();
    }

    private void Awake()
    {
        ScoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
    }

    // Use this for initialization
    private void Start()
    {
        _lastFall = Time.time;
        _lastMove = Time.time;
        //Default position not valid? It's Game Over
        if (!IsValidGridPos())
        {
            Destroy(gameObject);
            Time.timeScale = 0;
            PublicClass.IsGameOver = true;
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Input.GetButtonDown("Fire1") && Time.time - _lastMove >= moveBuffer && MenuInGaming.Paused != true)
        {

            if (CameraControl.PositionCount%2 == 0)
            {
                transform.Rotate(90*(CameraControl.PositionCount - 1), 0, 0, Space.World);
                if (IsValidGridPos())
                {
                    UpdateGrid();
                    _lastMove = Time.time;
                }
                else
                {
                    transform.Rotate(-90*(CameraControl.PositionCount - 1), 0, 0, Space.World);
                }
            }
            else
            {
                transform.Rotate(0, 0, 90 * (CameraControl.PositionCount - 2), Space.World);
                if (IsValidGridPos())
                {
                    UpdateGrid();
                    _lastMove = Time.time;
                }
                else
                {
                    transform.Rotate(0, 0, -90 * (CameraControl.PositionCount - 2), Space.World);
                }
            }
        }
        else if (Input.GetButtonDown("Fire2") && Time.time - _lastMove >= moveBuffer && MenuInGaming.Paused != true)
        {
            transform.Rotate(0, -90, 0, Space.World);
            if (IsValidGridPos())
            {
                UpdateGrid();
                _lastMove = Time.time;
            }
            else
            {
                transform.Rotate(0, 90, 0, Space.World);
            }
        }
        else if (Input.GetButtonDown("Fire3") && Time.time - _lastMove >= moveBuffer && MenuInGaming.Paused != true)
        {
            //left
            transform.Rotate(0, 90, 0, Space.World);
            if (IsValidGridPos())
            {
                UpdateGrid();
                _lastMove = Time.time;
            }
            else
            {
                transform.Rotate(0, -90, 0, Space.World);
            }
        }
        else if (Input.GetButtonDown("Jump") && Time.time - _lastMove >= moveBuffer && MenuInGaming.Paused != true)
        {
            if (CameraControl.PositionCount % 2 == 0)
            {
                transform.Rotate(-90 * (CameraControl.PositionCount - 1), 0, 0, Space.World);
                if (IsValidGridPos())
                {
                    UpdateGrid();
                    _lastMove = Time.time;
                }
                else
                {
                    transform.Rotate(90 * (CameraControl.PositionCount - 1), 0, 0, Space.World);
                }
            }
            else
            {
                transform.Rotate(0, 0, -90 * (CameraControl.PositionCount - 2), Space.World);
                if (IsValidGridPos())
                {
                    UpdateGrid();
                    _lastMove = Time.time;
                }
                else
                {
                    transform.Rotate(0, 0, 90 * (CameraControl.PositionCount - 2), Space.World);
                }
            }
        }
        //Move Left
        else if (Input.GetAxis("Horizontal") < 0 && Time.time - _lastMove >= moveBuffer && MenuInGaming.Paused != true)
        {
            if (CameraControl.PositionCount%2 == 0)
            {
                transform.position += new Vector3(1 * (CameraControl.PositionCount - 1), 0, 0);
                if (IsValidGridPos())
                {
                    UpdateGrid();
                    _lastMove = Time.time;
                }
                else
                {
                    transform.position += new Vector3(-1 * (CameraControl.PositionCount - 1), 0, 0);
                }
            }
            else
            {
                transform.position += new Vector3(0, 0, 1 * (CameraControl.PositionCount - 2));
                if (IsValidGridPos())
                {
                    UpdateGrid();
                    _lastMove = Time.time;
                }
                else
                {
                    transform.position += new Vector3(0, 0, -1 * (CameraControl.PositionCount - 2));
                }
            }
        }
        //Move Right
        else if (Input.GetAxis("Horizontal") > 0 && Time.time - _lastMove >= moveBuffer && MenuInGaming.Paused != true)
        {
            if (CameraControl.PositionCount % 2 == 0)
            {
                transform.position += new Vector3(-1 * (CameraControl.PositionCount - 1), 0, 0);
                if (IsValidGridPos())
                {
                    UpdateGrid();
                    _lastMove = Time.time;
                }
                else
                {
                    transform.position += new Vector3(1 * (CameraControl.PositionCount - 1), 0, 0);
                }
            }
            else
            {
                transform.position += new Vector3(0, 0, -1 * (CameraControl.PositionCount - 2));
                if (IsValidGridPos())
                {
                    UpdateGrid();
                    _lastMove = Time.time;
                }
                else
                {
                    transform.position += new Vector3(0, 0, 1 * (CameraControl.PositionCount - 2));
                }
            }
        }
        //Rotate
        else if (Input.GetAxis("Vertical") > 0 && Time.time - _lastMove >= moveBuffer && MenuInGaming.Paused != true)
        {
            if (CameraControl.PositionCount % 2 == 0)
            {
                transform.position += new Vector3(0, 0, -1 * (CameraControl.PositionCount - 1));
                if (IsValidGridPos())
                {
                    UpdateGrid();
                    _lastMove = Time.time;
                }
                else
                {
                    transform.position += new Vector3(0, 0, 1 * (CameraControl.PositionCount - 1));
                }
            }
            else
            {
                transform.position += new Vector3(1 * (CameraControl.PositionCount - 2), 0, 0);
                if (IsValidGridPos())
                {
                    UpdateGrid();
                    _lastMove = Time.time;
                }
                else
                {
                    transform.position += new Vector3(-1 * (CameraControl.PositionCount - 2), 0, 0);
                }
            }
        }
        else if (Input.GetAxis("Vertical") < 0 && Time.time - _lastMove >= moveBuffer && MenuInGaming.Paused != true)
        {
            if (CameraControl.PositionCount % 2 == 0)
            {
                transform.position += new Vector3(0, 0, 1 * (CameraControl.PositionCount - 1));
                if (IsValidGridPos())
                {
                    UpdateGrid();
                    _lastMove = Time.time;
                }
                else
                {
                    transform.position += new Vector3(0, 0, -1 * (CameraControl.PositionCount - 1));
                }
            }
            else
            {
                transform.position += new Vector3(-1 * (CameraControl.PositionCount - 2), 0, 0);
                if (IsValidGridPos())
                {
                    UpdateGrid();
                    _lastMove = Time.time;
                }
                else
                {
                    transform.position += new Vector3(1 * (CameraControl.PositionCount - 2), 0, 0);
                }
            }
        }
        //Move Downwards and fall
        if ((Input.GetButtonDown("Fall") || Input.GetAxis("FallSt") < 0 || Time.time - _lastFall >= _deltaTime) &&
            MenuInGaming.Paused != true && Time.time - _lastMove >= moveBuffer)
        {
            //Modify position
            transform.position += new Vector3(0, -1, 0);

            //See if valid
            if (IsValidGridPos())
            {
                //It's valid. Update grid
                UpdateGrid();
            }
            else
            {
                transform.position += new Vector3(0, 1, 0);

                //Clear filled horizontal lines
                var counter = Grid.DeleteFullRows();
                if (counter != 0)
                {
                    switch (counter)
                    {
                        case 1:
                            Player.Score += 40 * (Player.Level + 1);
                            break;
                        case 2:
                            Player.Score += 100 * (Player.Level + 1);
                            break;
                        case 3:
                            Player.Score += 300 * (Player.Level + 1);
                            break;
                        case 4:
                            Player.Score += 1200 * (Player.Level + 1);
                            break;
                    }
                    Player._Rows += counter;
                    ScoreText.text = Player.Score.ToString();
                    if ((Player._Rows > (Player.Level + 1)*Difficulty) ||
                        (PublicClass.isStandardMode && Player._Rows == 15))
                    {
                        Player.Level += 1;
                        _deltaTime = 0.05 * (11 - Player.Level);
                    }
                }
                //Spawn next Group
                FindObjectOfType<Spawner>().SpawnNext();

                //Disable script
                enabled = false;
            }

            _lastFall = Time.time;
            _lastMove = Time.time;
        }
    }
}