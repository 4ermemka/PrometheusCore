using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public enum ParamDisplayMethod
{ 
    Points,
    Number
}

public class TextParam : MonoBehaviour
{
    [SerializeProperty("Name")]
    public string _name = "Param";

    [SerializeProperty("PointStyle")]
    public string _pointStyle = "|";

    [SerializeProperty("NumericValue")]
    public int _numericValue = 3;

    [SerializeProperty("DisplayMethod")]
    public ParamDisplayMethod _displayMethod = ParamDisplayMethod.Points;

    [SerializeField]
    public int MaxValue;

    [SerializeField]
    public int MinValue;

    public string _textValue;

    [SerializeField]
    TextMeshProUGUI NameLabel;

    [SerializeField]
    TextMeshProUGUI ValueLabel;

    [SerializeField]
    List<Color> Colors;

    [ExecuteInEditMode]
    [property : SerializeField]
    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            this.gameObject.name = _name + "Param";
            UpdateText();
        }
    }

    [ExecuteInEditMode]
    [SerializeField]
    public int NumericValue
    {
        get => _numericValue;
        set 
        {
            if(value < 0)
                value = 0;
            _numericValue = value;
            UpdateText();
        }
    }

    [ExecuteInEditMode]
    [SerializeField]
    public string PointStyle
    {
        get => _pointStyle;
        set
        { 
            _pointStyle = value;
            UpdateText();
        }
    }

    [ExecuteInEditMode]
    [SerializeField]
    public ParamDisplayMethod DisplayMethod
    {
        get => _displayMethod;
        set
        {
            _displayMethod = value;
            UpdateText();
        }
    }

    public void SetNumericValue(int value)
    {
        NumericValue = value;
    }

    private void UpdateText()
    {
        //Debug.Log("Changed");
        NameLabel.text = Name;

        _textValue = string.Empty;
        
        int colorIndex = 0;
        if(NumericValue > 0) 
        {
            int pointsPerColor = MaxValue / (Colors.Count - 1);
            colorIndex = NumericValue / pointsPerColor;
            if (NumericValue % pointsPerColor != 0) 
                colorIndex++;
        }

        string points = string.Empty;
        for (int i = 0; i < NumericValue; i++)
            points += _pointStyle;
        string emptyPoints = string.Empty;
        for (int i = 0; i < MaxValue - NumericValue; i++)
            emptyPoints += _pointStyle;

        _textValue += ColorText(Colors[colorIndex], points);
        _textValue += ColorText(Colors[0], emptyPoints);

        switch (_displayMethod)
        {
            case ParamDisplayMethod.Number:
                ValueLabel.text = NumericValue.ToString();
                break;
            case ParamDisplayMethod.Points:
                ValueLabel.text = _textValue;
                break;
        }
    }

    private string ColorText(Color color, string text)
    {
        return $"<color=#{color.ToHexString()}>{text}</color>";
    }
}
