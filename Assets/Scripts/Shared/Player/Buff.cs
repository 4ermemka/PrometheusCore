using System;

public class Buff : SPECIAL
{
    public int Duration;

    public Buff() : base()
    { 
    
    }

    public Buff(
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
        Duration = duration;
    }

    public void DecreaseDuration()
    {
        if (Duration > 0)
        {
            Duration--;
        }
    }
}
