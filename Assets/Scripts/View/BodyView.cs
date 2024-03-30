using Assets.Scripts.Shared.Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BodyView : MonoBehaviour
{
    [SerializeField]
    List<Color> StatusColors;

    [SerializeField]
    Image HeadImage;

    [SerializeField]
    Image TorsoImage;

    [SerializeField]
    Image LeftArmImage;

    [SerializeField]
    Image RightArmImage;

    [SerializeField]
    Image LeftLegImage;

    [SerializeField]
    Image RightLegImage;

    private void Start()
    {
        foreach (BodyPart bodyPart in (BodyPart[])Enum.GetValues(typeof(BodyPart)))
        {
            HealBodyPart(bodyPart);
        }
    }

    public void DamageBodyPart(BodyPart bodyPart)
    {
        switch (bodyPart)
        {
            case BodyPart.Head:
                HeadImage.color = StatusColors[StatusColors.Count-1];
                break;
            case BodyPart.Torso:
                TorsoImage.color = StatusColors[StatusColors.Count - 1];
                break;
            case BodyPart.LeftArm:
                LeftArmImage.color = StatusColors[StatusColors.Count - 1];
                break;
            case BodyPart.RightArm:
                RightArmImage.color = StatusColors[StatusColors.Count - 1];
                break;
            case BodyPart.LeftLeg:
                LeftLegImage.color = StatusColors[StatusColors.Count - 1];
                break;
            case BodyPart.RightLeg:
                RightLegImage.color = StatusColors[StatusColors.Count - 1];
                break;
        }
    }

    public void HealBodyPart(BodyPart bodyPart)
    {
        switch (bodyPart)
        {
            case BodyPart.Head:
                HeadImage.color = StatusColors[0];
                break;
            case BodyPart.Torso:
                TorsoImage.color = StatusColors[0];
                break;
            case BodyPart.LeftArm:
                LeftArmImage.color = StatusColors[0];
                break;
            case BodyPart.RightArm:
                RightArmImage.color = StatusColors[0];
                break;
            case BodyPart.LeftLeg:
                LeftLegImage.color = StatusColors[0];
                break;
            case BodyPart.RightLeg:
                RightLegImage.color = StatusColors[0];
                break;
        }
    }

    public void UpdateView(Body body)
    {
        foreach (Part part in body.Parts)
        {
            if (part.DamageLevel == 0)
            {
                HealBodyPart(part.BodyPart);
            }
            if (part.DamageLevel == 0)
            {
                HealBodyPart(part.BodyPart);
            }
        }
    }
}
