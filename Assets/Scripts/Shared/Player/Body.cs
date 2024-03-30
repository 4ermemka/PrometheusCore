using Assets.Scripts.Shared.Constants;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;


public class Body
{
    [JsonIgnore]
    public Action<Part> OnBodyPartHealed;
    [JsonIgnore]
    public Action<Part> OnBodyPartDamaged;

    private List<Part> _parts;

    public List<Part> Parts 
    {
        get => _parts;
        set
        { 
            var DamagedParts = value.Where(x=> x.DamageLevel != 0 && !_parts.Any(p=>p.BodyPart == x.BodyPart && p.DamageLevel == 0));
            var HealedParts = value.Where(x => x.DamageLevel == 0 && !_parts.Any(p => p.BodyPart == x.BodyPart && p.DamageLevel != 0));

            foreach (var part in DamagedParts)
            { 
                OnBodyPartDamaged?.Invoke(part);
            }
            foreach (var part in HealedParts)
            {
                OnBodyPartHealed?.Invoke(part);
            }

            _parts = value;
        }
    }

    public Body()
    {
        _parts = new List<Part>();

        foreach (BodyPart bodyPart in (BodyPart[])Enum.GetValues(typeof(BodyPart)))
        {
            Parts.Add(new Part()
            {
                BodyPart = bodyPart,
                DamageLevel = 0
            });
        }
    }

    public void UpdateState(Body newState)
    { 
        Parts = newState.Parts;
    }
}
