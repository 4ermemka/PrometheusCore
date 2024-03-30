using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

public class Stats
{
    [JsonIgnore]
    public Action<Buff> OnBuffAdded;
    [JsonIgnore]
    public Action<Buff> OnBuffUpdated;
    [JsonIgnore]
    public Action<Buff> OnBuffRemoved;

    [JsonIgnore]
    public Action<Buff> OnDebuffAdded;
    [JsonIgnore]
    public Action<Buff> OnDebuffUpdated;
    [JsonIgnore]
    public Action<Buff> OnDebuffRemoved;

    [JsonIgnore]
    public Action<Injury> OnInjuryAdded;
    [JsonIgnore]
    public Action<Injury> OnInjuryUpdated;
    [JsonIgnore]
    public Action<Injury> OnInjuryRemoved;

    public SPECIAL Special;
    private List<Buff> _buffs;
    private List<Buff> _debuffs;
    private List<Injury> _injuries;

    public List<Buff> Buffs 
    {
        get => _buffs;
        set
        {
            var addedBuffs = value?.Where(b=>!_buffs.Contains(b));
            var removedBuffs = _buffs?.Where(b=>!value.Contains(b));
            var updatedBuffs = _buffs?.Where(b=>value.Contains(b));

            if(addedBuffs != null)
                foreach (var b in addedBuffs) 
                { 
                    OnBuffAdded?.Invoke(b);
                }

            if(removedBuffs != null)
                foreach (var b in removedBuffs)
                {
                    OnBuffRemoved?.Invoke(b);
                }

            if(updatedBuffs != null)
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
            var addedDebuffs = value?.Where(b => !_debuffs.Contains(b));
            var removedDebuffs = _debuffs?.Where(b => !value.Contains(b));
            var updatedDebuffs = _debuffs?.Where(b => value.Contains(b));

            if (addedDebuffs != null)
                foreach (var b in addedDebuffs)
                {
                    OnDebuffAdded?.Invoke(b);
                }

            if (removedDebuffs != null)
                foreach (var b in removedDebuffs)
                {
                    OnDebuffRemoved?.Invoke(b);
                }

            if(updatedDebuffs != null)
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
            var addedInjuries = value?.Where(b => !_injuries.Contains(b));
            var removedInjuries = _injuries?.Where(b => !value.Contains(b));
            var updatedInjuries = _injuries?.Where(b => value.Contains(b));

            if(addedInjuries != null)
                foreach (var b in addedInjuries)
                {
                    OnInjuryAdded?.Invoke(b);
                }

            if (removedInjuries != null)
                foreach (var b in removedInjuries)
                {
                    OnInjuryRemoved?.Invoke(b);
                }

            if(updatedInjuries != null)
                foreach (var b in updatedInjuries)
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
        foreach (Buff debuff in Debuffs)
        {
            debuff.DecreaseDuration();
        }
        foreach (Injury injury in Injuries)
        {
            injury.DecreaseDuration();
        }
    }

    public void UpdateState(Stats updatedState)
    {
        Special = updatedState.Special;
        Buffs = updatedState.Buffs;
        Debuffs = updatedState.Debuffs;
        Injuries = updatedState.Injuries;
    }

    public SPECIAL CountStats() // подсчет текущих параметров с учетом дебафов и бафов
    {
        return Special;
    }
}
