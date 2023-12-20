using System;

public class Level
{
    //<int> == <newValue>
    public Action<int> OnLevelUp { get; set; }

    //<int,int> == <amount, newValue>
    public Action<int, int> OnExpirienceGet { get; set; }

    private int _level;
    private int _xp;

    public int Lvl 
    {
        get 
        {
            return _level;
        }
        private set
        {
            _level = value;
            OnLevelUp?.Invoke(_level);
        }
    }

    public int Experience 
    {
        get
        { 
            return _xp;
        }
        private set
        {
            OnExpirienceGet?.Invoke(value-_xp, value);
            _xp = value;
        }
    }

    public Level()
    {
        Lvl = 0;
        Experience = 0;
    }

    public Level(int lvl, int experience = 0)
    {
        Lvl = lvl;
        Experience = experience;
    }

    public void UpdateState(Level updatedState)
    { 
        Lvl = updatedState.Lvl;
        Experience = updatedState.Experience;
    }
}
