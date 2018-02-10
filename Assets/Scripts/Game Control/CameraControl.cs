using UnityEngine;

public class CameraControl : MonoBehaviour
{
    //private static int _deltaTime = 1;
    private const double moveBuffer = 0.5;
    private float _lastMove;
    public static int PositionCount=0;
    public static Vector3[] CameraPositions = new Vector3[4];

    // Use this for initialization
    private void Start()
    {
        CameraPositions[0] = new Vector3((float)4.5, (float)7.5, -9);
        CameraPositions[1] = new Vector3((float)17.5, (float)7.5, (float)4.5);
        CameraPositions[2] = new Vector3((float)4.5, (float)7.5, 18);
        CameraPositions[3] = new Vector3( -9, (float) 7.5, (float) 4.5);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if ((Input.GetKeyDown(KeyCode.Q)||Input.GetKeyDown(KeyCode.JoystickButton4)) && Time.time - _lastMove >= moveBuffer &&
            MenuInGaming.Paused != true)
        {
            PositionCount = (PositionCount + 3)%4;
            transform.position = CameraPositions[PositionCount];
            transform.Rotate(0, 90, 0);
            _lastMove = Time.time;
        }else if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton5)) && Time.time - _lastMove >= moveBuffer &&
            MenuInGaming.Paused != true)
        {
            PositionCount = (PositionCount + 1)%4;
            transform.position = CameraPositions[PositionCount];
            transform.Rotate(0, -90, 0);
            _lastMove = Time.time;
        }

    }
}