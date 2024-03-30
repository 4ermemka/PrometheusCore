using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialStatusView : MonoBehaviour
{
    [SerializeProperty("Special")]
    private SPECIAL _special;

    [ExecuteInEditMode]
    [SerializeField]
    public SPECIAL Special 
    { 
        get => _special;
        set
        { 
            _special = value;
            UpdateView();
        }
    }

    [SerializeField] TextParam SParam;
    [SerializeField] TextParam PParam;
    [SerializeField] TextParam EParam;
    [SerializeField] TextParam CParam;
    [SerializeField] TextParam IParam;
    [SerializeField] TextParam AParam;
    [SerializeField] TextParam LParam;

    private void UpdateView()
    {
        if(_special != null)
        SParam.SetNumericValue(_special.S);
        PParam.SetNumericValue(_special.P);
        EParam.SetNumericValue(_special.E);
        CParam.SetNumericValue(_special.C);
        IParam.SetNumericValue(_special.I);
        AParam.SetNumericValue(_special.A);
        LParam.SetNumericValue(_special.L);
    }
}
