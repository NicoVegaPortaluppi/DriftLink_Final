using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// a base class to select an integer with - and + buttons. used for laps and ai bots (inherited)
/// </summary>
namespace SpinMotion
{
    public class SelectorGUI : MonoBehaviour
    {
        public Button buttonDown, buttonUp;
        public TMP_Text quantityTMP;
        public int defaultQuantity = 0;


        protected int min;
        protected int quantity;
        protected int max;

        private void Start()
        {
           WebInputRouter.Instance.lapsInput = this;
            

            buttonDown.onClick.AddListener(OnClickDown);
            buttonUp.onClick.AddListener(OnClickUp);

            if (defaultQuantity < min)
                Debug.LogError("default quantity lower than minimum");
            if (defaultQuantity >= max)
                Debug.LogError("default quantity higer than maximum");

            quantity = defaultQuantity;
            UpdateQuantity(0);
        }

        public void OnClickDown()
        {
            UpdateQuantity(-1);
        }

        public void OnClickUp()
        {
            UpdateQuantity(1);
        }

        private void UpdateQuantity(int change)
        {
            quantity = Mathf.Clamp(quantity + change, min, max);
            quantityTMP.text = quantity.ToString();
            buttonUp.interactable = quantity < max;
            buttonDown.interactable = quantity > min;
            QuantityUpdated();
        }


        protected virtual void QuantityUpdated() {}
    }
}