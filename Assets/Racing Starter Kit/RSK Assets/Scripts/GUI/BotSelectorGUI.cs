using UnityEngine;
/// <summary>
/// uses the base SelectorGUI and overrides QuantityUpdated to refresh RaceData on how many AI bots will the race have
/// </summary>
namespace SpinMotion
{
    public class BotSelectorGUI : SelectorGUI
    {
        [Header("Ensure having enough AI car spawn points")]
        [Range(0, 13)]public int maxBots;

        void Start()
        {
            WebInputRouter.Instance.botsInput = this;
        }
        private void Awake()
        {
            min = 0;
            max = 13;
        }

        protected override void QuantityUpdated()
        {
            Debug.Log($"Bots quantity updated");
            RaceData.AiBotsSelected = quantity;
        }
    }
}