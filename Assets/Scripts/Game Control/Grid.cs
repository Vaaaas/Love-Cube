using UnityEngine;

public class Grid : MonoBehaviour
{
    //The 2-dimensional array -- grid
    public const int Width = 10;
    public const int Height = 20;
    public static readonly Transform[,,] grid = new Transform[Width, Height, Width];
    //if is active -> can be clear
    public static bool[] surfaceActive = new bool[Height];

    public static Vector3 RoundVec3(Vector3 vec)
    {
        return new Vector3(Mathf.Round(vec.x), Mathf.Round(vec.y), Mathf.Round(vec.z));
    }
    //Judge if a certain coordinate is in between the borders or if it's outside of the borders
    public static bool InsideBorder(Vector3 pos)
    {
        return (int) pos.x >= 0 && (int) pos.x < Width && (int) pos.y >= 0 && (int) pos.z >= 0 && (int) pos.z <= Width;
    }

    //Delete all blocks in A Certain Row
    public static void DeleteSurface(int y)
    {
        for (var x = 0; x < Width; x++)
        {
            for (var z = 0; z < Width; z++)
            {
                if (grid[x, y, z] != null)
                {
                    Destroy(grid[x, y, z].gameObject);
                    grid[x, y, z] = null;
                }
            }
        }
    }

    //Whenver a row was deleted, the rows above fall towards the bottom by one unit
    private static void DecreaseSurface(int y)
    {
        for (var x = 0; x < Width; x++)
        {
            for (var z = 0; z < Width; z++)
            {
                //!!!!! If one row has no block exist, return.
                if (grid[x, y, z] == null) continue;
                //Move one unit towards bottom
                grid[x, y - 1, z] = grid[x, y, z];
                grid[x, y, z] = null;

                //Update block position
                //Decrease the Y coordinate by 1
                grid[x, y - 1, z].position += new Vector3(0, -1, 0);
            }
        }
    }

    //Whenever a row was deleted, decrease all rows above it
    private static void DecreaseRowsAbove(int y)
    {
        for (var i = y; i < Height; i++)
        {
            DecreaseSurface(i);
        }
    }

    private static bool CheckOneSurface(int y)
    {
        for (int i = 0; i < Width; i++)
        {
            if (!IsXLineFull(i, y))
                return false;
            if (!IsZLineFull(i, y))
                return false;
        }
        return true;
    }

    //Find out if a row is full of blocks
    private static bool IsXLineFull(int x, int y)
    {
        for (var z = 0; z < Width; z++)
        {
            if (grid[x, y, z] != null)
            {
                return true;
            }
        }
        return false;
    }

    private static bool IsZLineFull(int z, int y)
    {
        for (var x = 0; x < Width; x++)
        {
            if (grid[x, y, z] != null)
            {
                return true;
            }
        }
        return false;
    }

    public static void initSurAct()
    {
        for (int i = 0; i < Height; i++)
        {
            surfaceActive[i] = true;
        }
    } 

    //Detect all full rows and always decrease the above row's Y coordinate by 1
    public static int DeleteFullRows()
    {
        var counter = 0;
        //Check X full, check one surface
        for (int i = 0; i < Height; i++)
        {
            if (CheckOneSurface(i))
            {
                if (PublicClass.isStandardMode || CheckSurfaceActive(i))
                {
                    DeleteSurface(i);
                    DecreaseRowsAbove(i + 1);
                    i--;
                    counter++;
                }
                else
                {
                    surfaceActive[i] = false;
                    counter++;
                }
            }
        }
        return counter;
        //for (var y = 0; y < Height; y++)
        //{
        //    if (!IsRowFull(y)) continue;
        //    DeleteSurface(y);
        //    DecreaseRowsAbove(y + 1);
        //    //Still need to detect this row in next step
        //    y--;
        //    counter++;
        //}
    }

    private static bool CheckSurfaceActive(int y)
    {
        for (int i = 0; i < Height; i++)
        {
            if (i != 0 && i!=y && surfaceActive[i])
            {
                return false;
            }
        }
        return true;
    }

    // Use this for initialization
    private void Start()
    {

    }


    // Update is called once per frame
    private void Update()
    {
    }
}