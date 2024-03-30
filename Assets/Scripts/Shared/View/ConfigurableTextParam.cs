using System;
using UnityEngine;
using UnityEngine.UI;

public class ConfigurableTextParam : TextParam
{
    public Action OnParamIncreased;
    public Action OnParamDecreased;

    [SerializeField]
    private Button IncreaseButton;

    [SerializeField]
    private Button DecreaseButton;

    bool _canIncrease;
    bool _canDecrease;

    bool CanIncrease {
        get 
        { 
            return _canIncrease; 
        }
        set
        {  
            _canIncrease = value;
            if (!value)
                BlockIncreasing();
        }
    }

    bool CanDecrease
    {
        get
        {
            return _canDecrease;
        }
        set
        {
            _canDecrease = value;
            if (!value)
                BlockDecreasing();
        }
    }

    public void Start()
    {
        IncreaseButton.onClick.AddListener(Increase); 
        DecreaseButton.onClick.AddListener(Decrease);
    }

    private void Increase()
    {
        if (CanIncrease && NumericValue < MaxValue)
        {
            SetNumericValue(NumericValue + 1);
            OnParamIncreased?.Invoke();
            if (NumericValue == MaxValue)
            { 
                BlockIncreasing();
            }
            if (NumericValue > MinValue && CanDecrease)
            { 
                UnlockDecreasing();
            }
        }
    }

    private void BlockIncreasing()
    { 
        IncreaseButton.enabled = false;
    }

    private void UnlockIncreasing()
    {
        IncreaseButton.enabled = true;
    }

    private void Decrease() 
    {
        if (CanDecrease && NumericValue > MinValue)
        {
            SetNumericValue(NumericValue - 1);
            OnParamDecreased?.Invoke();
            if (NumericValue == MinValue)
            {
                BlockDecreasing();
            }
            if (NumericValue < MaxValue && CanIncrease)
            {
                UnlockIncreasing();
            }
        }
    }

    private void BlockDecreasing()
    {
        DecreaseButton.enabled = false;
    }

    private void UnlockDecreasing()
    {
        DecreaseButton.enabled = true;
    }
}
