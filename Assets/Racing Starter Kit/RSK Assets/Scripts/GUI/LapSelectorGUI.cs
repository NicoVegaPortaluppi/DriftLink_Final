using UnityEngine;
/// <summary>
/// uses the base SelectorGUI and overrides QuantityUpdated to refresh RaceData on how many laps will the player race
/// </summary>
namespace SpinMotion
{
    public class LapSelectorGUI : SelectorGUI
    {
        [Range(1, 99)] public int maxLaps;
        void Start()
        {
            WebInputRouter.Instance.lapsInput = this;
        }
        private void Awake()
        {
            min = 1;
            max = maxLaps;  
        }

        protected override void QuantityUpdated()
        {
            Debug.Log($"laps quantity updated");
            RaceData.LapsSelected = quantity;
        }
    }
}