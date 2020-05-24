using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BarScript : MonoBehaviour {

    
    public float fillAmount;

    [SerializeField]
    private Image content;

    public float MaxValue { get; set; }

    private float _value;

    public float Value
    {
        get
        {
            return _value;
        }
        set
        {
            _value = value;
            fillAmount = Map(value, 0, MaxValue, 0, 1);
            Debug.Log(gameObject.name+ "value: " + value + " ; " + fillAmount);
        }
    }

	// Use this for initialization
	void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {

        HandleBar();
	
	}

    private void HandleBar()
    {
        if (fillAmount != content.fillAmount)
        {
            content.fillAmount = fillAmount;
        }
        
    }

    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;  
    }
}
