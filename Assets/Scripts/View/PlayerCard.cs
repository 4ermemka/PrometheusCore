using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCard : MonoBehaviour
{
    [SerializeField] TextParam HPParam;

    [SerializeField] TextParam SParam;
    [SerializeField] TextParam PParam;
    [SerializeField] TextParam EParam;
    [SerializeField] TextParam CParam;
    [SerializeField] TextParam IParam;
    [SerializeField] TextParam AParam;
    [SerializeField] TextParam LParam;

    [SerializeField] TextParam Caps;
    [SerializeField] TextParam Level;
    [SerializeField] TextParam Experience;

    [SerializeField] BodyView BodyView;

    public Player _player;
    public Player Player
    {
        get => _player;
        set
        {
            _player = value;
            UpdatePlayer();
        }
    }

    private void UpdatePlayer()
    {
        HPParam.SetNumericValue(_player.HealthBar.Health);

        SParam.SetNumericValue(_player.Stats.Special.S);
        PParam.SetNumericValue(_player.Stats.Special.P);
        EParam.SetNumericValue(_player.Stats.Special.E);
        CParam.SetNumericValue(_player.Stats.Special.C);
        IParam.SetNumericValue(_player.Stats.Special.I);
        AParam.SetNumericValue(_player.Stats.Special.A);
        LParam.SetNumericValue(_player.Stats.Special.L);

        Caps.SetNumericValue(_player.Caps);
        Level.SetNumericValue(_player.Level.Lvl);
        Experience.SetNumericValue(_player.Level.Experience);

        BodyView.UpdateView(_player.Body);
    }
}