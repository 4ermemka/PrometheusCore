using System.Collections.Generic;

public class Stats
{
    public SPECIAL Special;
    public List<Buff> Buffs;
    public List<Buff> Debuffs;
    public List<Injury> Injuries;

    public Stats()
    {
        Special = new SPECIAL();
        Buffs = new List<Buff>();
        Debuffs = new List<Buff>();
        Injuries = new List<Injury>();
    }

    public Stats(int s, int p, int e, int c, int i, int a, int l)
    {
        Special = new SPECIAL(s,p,e,c,i,a,l);
        Buffs = new List<Buff>();
        Debuffs = new List<Buff>();
        Injuries = new List<Injury>();
    }
}
