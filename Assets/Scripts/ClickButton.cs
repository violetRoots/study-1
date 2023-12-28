using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickButton : GameButton
{
    [SerializeField] private MeshRenderer buttonVisual;

    [SerializeField] private Material activeMaterial;
    [SerializeField] private Material inactiveMaterial;

    public override bool IsClickSuccessful()
    {
        return true;
    }

    protected override void SetActiveVisual()
    {
        buttonVisual.material = activeMaterial;
    }

    protected override void SetInactiveVisual()
    {
        buttonVisual.material = inactiveMaterial;
    }
}
