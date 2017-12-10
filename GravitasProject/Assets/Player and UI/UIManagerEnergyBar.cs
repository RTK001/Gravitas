using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerEnergyBar : MonoBehaviour {

    public static UIManagerEnergyBar instance = null;          // Ensure this class is a singleton

    public float FullBarWidth;     // The width of the bar when full

    public const float FullBarHeight = 20;

    public RectTransform rectTransform;


    public void SetEnergyBar(float FractionFull)
        // sets how "full" the energy bar is, taking the float percentage full as an arguement.
    {
        rectTransform.sizeDelta = new Vector2 (FractionFull * FullBarWidth, FullBarHeight);
    }



    private void Awake()
    {
        if (instance == null)               // If the static value is null, set it to be this awakened instance.
        {
            instance = this;
        }
        else if (instance != null)          // if the value has been set to another instance of Playership, destroy this instance and preserve the other one.
        {
            Destroy(gameObject);
        }

        rectTransform = this.transform.GetComponent<RectTransform>();       // Get the rectTransform of the object attached to the script
 
        FullBarWidth = rectTransform.sizeDelta.x;                       // Set the full bar width

    }


}
