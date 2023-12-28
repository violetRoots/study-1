using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private GameButton[] buttons;
    [SerializeField] private Cage cage;

    private IReadOnlyCollection<GameButton> ClickedButtons => buttons.Where(b => b.IsClicked.Value).ToList();

    public bool IsLevelComplete()
    {
        return buttons.Length == ClickedButtons.Count;
    }

    public void CompleteLevel()
    {
        foreach(var button in buttons)
        {
            button.gameObject.SetActive(false);
        }

        cage.DestroyCage();
    }
}
