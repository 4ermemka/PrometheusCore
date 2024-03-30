using Assets.Scripts.Shared.Tools;
using Assets.Scripts.Shared.View;
using System;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class ViewPanel : MonoBehaviour
{
    public string Name;

    private CanvasGroup �anvasGroup;

    public virtual void Start()
    {
        try
        {
            Name = name;
            �anvasGroup = GetComponent<CanvasGroup>();
            ConsoleLogger.LogInformation($"{Name}", $"CanvasGroup = {�anvasGroup}");
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
            �anvasGroup.alpha = 1.0f;
            �anvasGroup.blocksRaycasts = true;
        });
    }

    public void Hide()
    {
        UnityMainThread.wkr.AddJob(() =>
        {
            �anvasGroup.alpha = 0f;
            �anvasGroup.blocksRaycasts = false;
        });

    }
}
