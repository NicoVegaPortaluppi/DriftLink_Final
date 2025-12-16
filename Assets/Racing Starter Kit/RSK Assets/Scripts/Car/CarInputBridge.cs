using UnityEngine;
using SpinMotion;   

public class CarInputBridge : MonoBehaviour
{
    [Header("Assign the PlayerCar CarController here")]
    public CarController car;

    // Last received values
    private float steer = 0f;
    private float accel = 0f;
    private float brake = 0f;
    private float joyX = 0f;

    private float handbrake = 0f;

    void Start()
    {
        // Register this instance in the router when the car spawns
        WebInputRouter.Instance.carInput = this;
    }

    public void OnSerialInput(string line)
    {
        string[] p = line.Split(',');
        if (p.Length < 5)
        {
            Debug.LogWarning("Bad input line: " + line);
            return;
        }

        int tilt = int.Parse(p[0]);
        int touch = int.Parse(p[1]);
        int joyX = int.Parse(p[2]);
        int joyBtn = int.Parse(p[4]);

        // --------- STEERING ----------
        float norm = Mathf.InverseLerp(0, 1023, joyX);
        steer = (norm - 0.5f) * 2f;

        if (Mathf.Abs(steer) < 0.1f)
            steer = 0f;

        // --------- ACCELERATION ----------
        accel = (tilt == 1 ? 1f : 0f);

        // --------- BRAKE ----------
        brake = (touch == 0 ? 1f : 0f);

        // --------- HAND BRAKE ----------
        handbrake = (joyBtn == 0 ? 1f : 0f);

        Debug.Log($"ARDUINO INPUT → steer={steer:F2}, accel={accel}, brake={brake}, handbrake={handbrake}");
    }

    void FixedUpdate()
    {
        if (car == null)
        {
            Debug.LogError("CarInputBridge ERROR: CarController reference not assigned!");
            return;
        }

        // CarController.Move(steer, accel, footbrake, handbrake)
        float footbrake = -brake;

        car.Move(steer, accel, footbrake, handbrake);

        Debug.Log($"MOVE() → steer={steer:F2}, accel={accel}, footbrake={footbrake}, handbrake={handbrake}");
    }
}


