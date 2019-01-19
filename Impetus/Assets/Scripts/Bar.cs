using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bar : MonoBehaviour {

    [SerializeField]
    bool isVertical; //Tick this if bar is vertical
    bool initialize = true;

    [SerializeField]
    Text barText;
    RectTransform thisTransform;

    float maxValue;
    float barValue;

    public float MaxValue
    {
        get
        {
            return maxValue;
        }
        set
        {
            if (initialize)
                BarValue = value;
            else
            {
                maxValue = value;
                UpdateUI();
            }
        }
    }
    public float BarValue
    {
        get
        {
            return barValue;
        }
        set
        {
            if (initialize)
            {
                initialize = false;
                thisTransform = GetComponent<RectTransform>();
                maxValue = value;
            }
            barValue = Mathf.Clamp(value, 0, maxValue); //clamp currentValue
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        SetScale();
        barText.text = Mathf.RoundToInt(BarValue + 0.49999f) + " / " + maxValue;
    }

    void SetScale()
    {
        float ratio = BarValue / maxValue;
        if (isVertical)
            thisTransform.localScale = new Vector3(thisTransform.localScale.x, ratio, 1);
        else
            thisTransform.localScale = new Vector3(ratio, thisTransform.localScale.y, 1);
    }
}
