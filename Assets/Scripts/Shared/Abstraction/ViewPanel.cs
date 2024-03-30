using Assets.Scripts.Shared.Tools;
using Assets.Scripts.Shared.View;
using System;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class ViewPanel : MonoBehaviour
{
    public string Name;

    private CanvasGroup ñanvasGroup;

    public virtual void Start()
    {
        try
        {
            Name = name;
            ñanvasGroup = GetComponent<CanvasGroup>();
            ConsoleLogger.LogInformation($"{Name}", $"CanvasGroup = {ñanvasGroup}");
            DontDestroyOnLoad(this);
        }
        catch (Exception ex)
        { 
            ConsoleLogger.LogException($"{Name} + ", ex);
        }
    }

    public void Show()
    {
        UnityMainThread.wkr.AddJob(() =>
        {
            ñanvasGroup.alpha = 1.0f;
            ñanvasGroup.blocksRaycasts = true;
        });
    }

    public void Hide()
    {
        UnityMainThread.wkr.AddJob(() =>
        {
            ñanvasGroup.alpha = 0f;
            ñanvasGroup.blocksRaycasts = false;
        });

    }
}
