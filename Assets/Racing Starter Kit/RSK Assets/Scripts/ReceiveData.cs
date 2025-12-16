using UnityEngine;

public class InputManager : MonoBehaviour
{
    public int tilt, touch, joyX, joyY, joyBtn;

    public void ReceiveData(string csv)
    {
        string[] p = csv.Split(',');
        tilt = int.Parse(p[0]);
        touch = int.Parse(p[1]);
        joyX = int.Parse(p[2]);
        joyY = int.Parse(p[3]);
        joyBtn = int.Parse(p[4]);
    }
}
