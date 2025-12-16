using UnityEngine;
using SpinMotion;

public class WebInputRouter : MonoBehaviour
{
    public static WebInputRouter Instance;

    public CarInputBridge carInput;
    public WebInputBridge menuInput;
    public SelectorGUI lapsInput;
    public SelectorGUI botsInput;
    public MenuGUI playInput;

    private void Awake()
    {
        Instance = this;
    }

    public void OnWebInput(string line)
    {
        Debug.Log($"WebInputRouter recibio {line}");
        string[] p = line.Split(',');

        if (p.Length < 5)
        {
            Debug.LogWarning("Bad input line: " + line);
            return;
        }

        if (p.Length == 5)
        {
            Debug.Log($"Sending {line} to CarInputBrigde");
            carInput.OnSerialInput(line);
        }
        else if (p.Length == 6)
        {
            string key = p[5].Trim().Trim('\r', '\n');

            if (key == "4")
            {
                if (lapsInput != null)
                {
                    lapsInput.OnClickDown();
                    Debug.Log("Decreased laps");
                }
                else
                {
                    Debug.LogWarning("lapsInput is not assigned in WebInputRouter!");
                }
            }
            else if (key == "6")
            {
                if (lapsInput != null)
                {
                    lapsInput.OnClickUp();
                    Debug.Log("Increased laps");
                }
                else
                {
                    Debug.LogWarning("lapsInput is not assigned in WebInputRouter!");
                }
            }

            else if (key == "#")
            {
                Debug.Log($"Reproduciendo el play race {key}");
                playInput.OnClickPlayRace();
            }

            else
            {
                Debug.Log($"Mandando el {key}");
                Debug.Log($"Sending {line} to WebInputBrigde");
                menuInput.OnSerialInput(line);
            }
        }
    }
}
