using System;
using System.Collections.Generic;
using System.Linq;

public class Stats
{
    public Action<Buff> OnBuffAdded;
    public Action<Buff> OnBuffUpdated;
    public Action<Buff> OnBuffRemoved;

    public Action<Buff> OnDebuffAdded;
    public Action<Buff> OnDebuffUpdated;
    public Action<Buff> OnDebuffRemoved;

    public Action<Buff> OnInjuryAdded;
    public Action<Buff> OnInjuryUpdated;
    public Action<Buff> OnInjuryRemoved;

    public SPECIAL Special;
    private List<Buff> _buffs;
    private List<Buff> _debuffs;
    private List<Injury> _injuries;

    public List<Buff> Buffs 
    {
        get => _buffs;
        set
        {
            var addedBuffs = value.Where(b=>!_buffs.Contains(b));
            var removedBuffs = _buffs.Where(b=>!value.Contains(b));
            var updatedBuffs = _buffs.Where(b=>value.Contains(b));

            foreach (var b in addedBuffs) 
            { 
                OnBuffAdded?.Invoke(b);
            }
            foreach (var b in removedBuffs)
            {
                OnBuffRemoved?.Invoke(b);
            }
            foreach (var b in updatedBuffs)
            {
                OnBuffUpdated?.Invoke(b);
            }

            _buffs = value;
        }
    }

    public List<Buff> Debuffs
    {
        get => _debuffs;
        set
        {
            var addedDebuffs = value.Where(b => !_debuffs.Contains(b));
            var removedDebuffs = _debuffs.Where(b => !value.Contains(b));
            var updatedDebuffs = _debuffs.Where(b => value.Contains(b));

            foreach (var b in addedDebuffs)
            {
                OnDebuffAdded?.Invoke(b);
            }
            foreach (var b in removedDebuffs)
            {
                OnDebuffRemoved?.Invoke(b);
            }
            foreach (var b in updatedDebuffs)
            {
                OnDebuffUpdated?.Invoke(b);
            }

            _debuffs = value;
        }
    }

    public List<Injury> Injuries
    {
        get => _injuries;
        set
        {
            var addedDebuffs = value.Where(b => !_injuries.Contains(b));
            var removedDebuffs = _injuries.Where(b => !value.Contains(b));
            var updatedDebuffs = _injuries.Where(b => value.Contains(b));

            foreach (var b in addedDebuffs)
            {
                OnInjuryAdded?.Invoke(b);
            }
            foreach (var b in removedDebuffs)
            {
                OnInjuryRemoved?.Invoke(b);
            }
            foreach (var b in updatedDebuffs)
            {
                OnInjuryUpdated?.Invoke(b);
            }

            _injuries = value;
        }
    }

    public Stats()
    {
        Special = new SPECIAL();
        Buffs = new List<Buff>();
        Debuffs = new List<Buff>();
        Injuries = new List<Injury>();
        SubscribeToEffects();
    }

    public Stats(int s, int p, int e, int c, int i, int a, int l)
    {
        Special = new SPECIAL(s,p,e,c,i,a,l);
        Buffs = new List<Buff>();
        Debuffs = new List<Buff>();
        Injuries = new List<Injury>();
        SubscribeToEffects();
    }

    public Stats(SPECIAL special)
    {
        Special = special;
        Buffs = new List<Buff>();
        Debuffs = new List<Buff>();
        Injuries = new List<Injury>();
        SubscribeToEffects();
    }

    private void SubscribeToEffects()
    {
        foreach (var b in Buffs) 
        {
            b.OnExpired += new Action(() => 
            { 
                Buffs.Remove(b);
                OnBuffRemoved?.Invoke(b);
            });
        }

        foreach (var d in Debuffs)
        {
            d.OnExpired += new Action(() =>
            {
                Debuffs.Remove(d);
                OnDebuffRemoved?.Invoke(d);
            });
        }

        foreach (var i in Injuries)
        {
            i.OnExpired += new Action(() =>
            {
                Injuries.Remove(i);
                OnInjuryRemoved?.Invoke(i);
            });
        }
    }

    public void AddBuff(Buff buff) 
    { 
        Buffs.Add(buff);
        OnBuffAdded?.Invoke(buff);
    }

    public void AddDebuff(Buff debuff)
    {
        Debuffs.Add(debuff);
        OnDebuffAdded?.Invoke(debuff);
    }

    public void AddInjury(Injury injury)
    {
        Injuries.Add(injury);
        OnInjuryAdded?.Invoke(injury);
    }

    public void Turn()
    { 
        foreach (Buff buff in Buffs) 
        { 
            buff.DecreaseDuration();
        }
    }

    public void UpdateState(Stats updatedState)
    {
        Special = updatedState.Special;
        Buffs = updatedState.Buffs;
        Debuffs = updatedState.Debuffs;
        Injuries = updatedState.Injuries;
    }

    public string ToString()
    {
        string special = Special.ToString();
        string buffs = String.Join("*", Buffs.Select(b=>b.ToString()).ToList());
        string debuffs = String.Join("*", Debuffs.Select(b=>b.ToString()).ToList());
        string injuries = String.Join("*", Injuries.Select(b=>b.ToString()).ToList());

        string result = $"{special}&{buffs}&{debuffs}&{injuries}";
        return result;
    }
}
