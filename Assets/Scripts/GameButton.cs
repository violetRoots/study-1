using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class GameButton : MonoBehaviour
{
    public readonly ReactiveProperty<bool> IsClicked = new ReactiveProperty<bool>();

    private void Awake()
    {
        IsClicked.Subscribe(OnClickedHandler);
    }

    private void OnDestroy()
    {
        IsClicked.Dispose();
    }

    public void ChangeClickedState()
    {
        Debug.Log($"{gameObject.name} IsClicked: {IsClicked.Value}");

        IsClicked.Value = !IsClicked.Value;
    }

    public virtual bool IsClickSuccessful()
    {
        return true;
    }

    private void OnClickedHandler(bool value)
    {
        if (value)
            SetActiveVisual();
        else
            SetInactiveVisual();
    }

    protected virtual void SetActiveVisual() { }

    protected virtual void SetInactiveVisual() { }
}
