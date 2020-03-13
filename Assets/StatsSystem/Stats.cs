using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour {
    /// <summary>
    //// Используется для создания полосок различных стат 
    /// (здоровье, мана и т.д., если понадобится)
    /// </summary>

    // картинка шкалы статы
    private Image bar;

    // текущая заполненность шкалы
    private float currentFill;

    // максимально возможное значение статы
    public float MaxValue { get; set; }

    // текущее значение статы
    private float currentValue;
    public float CurrentValue
    {
        get
        {
            return currentValue;
        }

        set
        {
            if (value > MaxValue)
            {
                currentValue = MaxValue;
            }
            else if (value < 0)
            {
                currentValue = 0;
            }
            else
            {
                currentValue = value;
            }
            currentFill = currentValue / MaxValue;
        }
    }
  
    // Use this for initialization
    void Start ()
    {
        bar = GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        // сглаживание изменений заполненности шкалы (Mathf.Lerp)
        if (currentFill != bar.fillAmount)
        {
            bar.fillAmount = Mathf.Lerp(bar.fillAmount, currentFill, Time.deltaTime * 10f);
        }
	}

    public void Initialize(float currentValue, float maxValue)
    {
        MaxValue = maxValue;
        CurrentValue = currentValue;
    }
}
