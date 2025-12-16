using UnityEngine;
using SpinMotion;

public class WebInputBridge : MonoBehaviour
{
    public GameManager gameManager;

    void Start()
    {
        WebInputRouter.Instance.menuInput = this;
    }


    public void OnSerialInput(string line)
    {
        string[] p = line.Split(',');
        Debug.Log($"Line {line} arrive WebInputBridge");
        if (p.Length < 6)
        {
            Debug.LogWarning("Bad keypad input: " + line);
            return;
        }

        // Sixth value = keypad
        string keyPad = p[5].Trim().Trim('\r', '\n');
        Debug.Log($"KeyPad: '{keyPad}' length={keyPad.Length}");
        // ------ PAUSE MENU (*) ------
        if (keyPad == "*")
        {
            Debug.Log($"Supuestamente pause");
            gameManager.gameEvents.OnClickTogglePauseEvent.Invoke();
        }

        // ------ IGNORE (#, 1, 2, 3 FOR NOW) ------
        else if (keyPad == "#")
        {
            // ignore for now
        }
        else if (keyPad == "1")
        {
            Debug.Log($"Resuming Race");
            gameManager.gameEvents.OnClickTogglePauseEvent.Invoke();
        }
        else if (keyPad == "2")
        {
            Debug.Log($"Restart Race");
            gameManager.gameEvents.OnClickTogglePauseEvent.Invoke();
            gameManager.gameEvents.OnClickRestartRaceEvent.Invoke();
        }
        else if (keyPad == "3")
        {
            Debug.Log($"Quit Game");
            gameManager.gameEvents.OnClickTogglePauseEvent.Invoke();
            gameManager.gameEvents.OnClickRestartGameEvent.Invoke();
        }

        Debug.Log($"KeyPad received = {keyPad}");
    }
}
