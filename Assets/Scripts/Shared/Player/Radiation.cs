using Newtonsoft.Json;
using System;

public class Radiation 
{
    [JsonIgnore]
    public Action<int, int> OnRadiationIncreased;
    [JsonIgnore]
    public Action<int, int> OnRadiationDecreased;
    [JsonIgnore]
    public Action OnRadiationDamage;

    private const int _turnsTillRadiationLevelIncreased = 15;

    private int _radiationLevel;
    private int _turnsCountTillDamage;

    public int TurnsTillRadDamageCounter;
    public int TurnsTillRadIncreaseCounter;

    public int RadiationLevel
    {
        get => _radiationLevel;
        set
        {
            if (value > _radiationLevel)
                OnRadiationIncreased?.Invoke(_radiationLevel, value);
            if (value < _radiationLevel)
                OnRadiationDecreased?.Invoke(_radiationLevel, value);

            _radiationLevel = value;
            UpdateTurnsCountTillDamage();
        }
    }

    public Radiation() 
    {
        _radiationLevel = 0;
        TurnsTillRadIncreaseCounter = 0;
        TurnsTillRadDamageCounter = 0;

        UpdateTurnsCountTillDamage();
    }

    public Radiation(int radiationLevel)
    {
        _radiationLevel = radiationLevel;
        TurnsTillRadIncreaseCounter = 0;
        TurnsTillRadDamageCounter = 0;

        UpdateTurnsCountTillDamage();
    }

    public Radiation(int radiationLevel, int turnsTillDamage, int turnsTillIncreaseLevel)
    {
        _radiationLevel = radiationLevel;
        TurnsTillRadIncreaseCounter = turnsTillIncreaseLevel;
        TurnsTillRadDamageCounter = turnsTillDamage;

        UpdateTurnsCountTillDamage();
    }

    private void UpdateTurnsCountTillDamage()
    {
        switch (_radiationLevel)
        { 
            case 0:
                _turnsCountTillDamage = -1;
                break;

            case 1:
                _turnsCountTillDamage = 5;
                break;

            case 2:
                _turnsCountTillDamage = 3;
                break;

            case 3:
                _turnsCountTillDamage = 1;
                break;

        }
        CheckForDamage();
        CheckForIncreasingLevel();
    }

    private void CheckForDamage()
    {
        if (TurnsTillRadDamageCounter > 0 && TurnsTillRadDamageCounter >= _turnsCountTillDamage)
        {
            OnRadiationDamage?.Invoke();
            TurnsTillRadDamageCounter = 0;
        }
    }

    private void CheckForIncreasingLevel()
    {
        if (TurnsTillRadIncreaseCounter >= _turnsTillRadiationLevelIncreased)
        {
            RadiationLevel++;
            TurnsTillRadIncreaseCounter = 0;
        }
    }

    public void Turn()
    {
        if (RadiationLevel == 0)
            return;

        TurnsTillRadDamageCounter ++;
        CheckForDamage();

        TurnsTillRadIncreaseCounter ++;
        CheckForIncreasingLevel();
    }

    public void Radiate()
    {
        if (RadiationLevel == 0)
            RadiationLevel = 1;

        else
        { 
            TurnsTillRadIncreaseCounter++;
        }
    }

    public void UpdateState(Radiation updatedState)
    { 
        TurnsTillRadIncreaseCounter = updatedState.TurnsTillRadIncreaseCounter;
        TurnsTillRadDamageCounter = updatedState.TurnsTillRadDamageCounter;

        RadiationLevel = updatedState.RadiationLevel;
    }
}
