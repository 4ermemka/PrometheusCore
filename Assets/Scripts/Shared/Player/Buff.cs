using System;

public class BuffJsonModel
{
    public string Name { get; set; }
    public string Special { get; set; }
    public int Duration { get; set; }
}

public class Buff : SPECIAL
{
    public Action OnExpired;

    public string Name { get; private set; }
    public int Duration { get; private set; }

    public Buff() : base()
    { 
    
    }

    public Buff(
        string name,
        int s = 0,
        int p = 0,
        int e = 0,
        int c = 0,
        int i = 0,
        int a = 0,
        int l = 0,
        int duration = 3
        ) : base(s,p,e,c,i,a,l)
    { 
        Name = name;
        Duration = duration;
    }

    public Buff(
        string name,
        string special,
        int duration = 3
        ) : base(special)
    {
        Name = name;
        Duration = duration;
    }

    public Buff Clone()
    {
        return new Buff(Name,S,P,E,C,I,A,L,Duration);
    }

    public void DecreaseDuration()
    {
        if (Duration > 0)
        {
            Duration--;
        }
        else
        {
            OnExpired?.Invoke();
        }
    }

    public static bool operator ==(Buff a, Buff b)
    {
        return a.Name == b.Name &&
            a.S == b.S &&
            a.P == b.P &&
            a.E == b.E &&
            a.C == b.C &&
            a.I == b.I &&
            a.A == b.A &&
            a.L == b.L;
    }

    public static bool operator !=(Buff a, Buff b)
    {
        return a.Name != b.Name ||
            a.S != b.S ||
            a.P != b.P ||
            a.E != b.E ||
            a.C != b.C ||
            a.I != b.I ||
            a.A != b.A ||
            a.L != b.L;
    }

    public override string ToString()
    {
        string result = $"{Name}|{base.ToString()}|{Duration}";
        return result;
    }
}
