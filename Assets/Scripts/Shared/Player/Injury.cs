using Assets.Scripts.Shared.Constants;
using System.Collections.Generic;
using Unity.VisualScripting;

public class InjuryJsonModel
{
    public string Name;
    public List<InjurySource> Sources;
    public List<BodyPart> BodyParts;
    public string Special;
    public int Duration;
}

public class Injury : Buff
{
    List<InjurySource> Sources;
    public BodyPart BodyPart;

    public Injury() : base()
    {

    }

    public Injury(
        string name,
        int s = 0,
        int p = 0,
        int e = 0,
        int c = 0,
        int i = 0,
        int a = 0,
        int l = 0,
        int duration = 15,
        BodyPart bodyPart = BodyPart.Head
        ) : base(name, s, p, e, c, i, a, l, duration)
    {
        BodyPart = bodyPart;
    }

    public Injury(
        string name,
        string special,
        List<InjurySource> sources,
        int duration = 15,
        BodyPart bodyPart = BodyPart.Head
        ) : base(name, special, duration)
    {
        BodyPart = bodyPart;
        Sources = sources;
    }

    public override string ToString()
    {
        string result = $"{Name}/{base.ToString()}/{Duration}/{BodyPart}";
        return result;
    }
}
