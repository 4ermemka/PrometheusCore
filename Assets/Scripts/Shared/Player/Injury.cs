public enum BodyPart
{
    Head = 0,
    Torso = 1,
    LeftArm = 2,
    RightArm = 3,
    LeftLeg = 4,
    RightLeg = 5
}

public class Injury : Buff
{
    public BodyPart BodyPart;

    public Injury() : base()
    {

    }

    public Injury(
        int s = 0,
        int p = 0,
        int e = 0,
        int c = 0,
        int i = 0,
        int a = 0,
        int l = 0,
        int duration = 3,
        BodyPart bodyPart = 0
        ) : base(s, p, e, c, i, a, l, duration)
    {
        BodyPart = bodyPart;
    }
}
