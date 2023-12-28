using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float rotationDuartion = 0.25f;
    [SerializeField] private float levelCompleteDuartion = 1.5f;

    [SerializeField] private Level[] Levels;

    [SerializeField] private InputManager inputManager;
    [SerializeField] private Transform cageTurnContainer;
    [SerializeField] private Transform cageContentContainer;

    private Tweener rotationTweener;
    private Level _currentLevel;

    private int CurrentLevelIndex
    {
        get => _currentLevelIndex;
        set => _currentLevelIndex = Mathf.Clamp(value, 0, Levels.Length - 1);
    }
    private int _currentLevelIndex;

    private void Awake()
    {
        inputManager.WKeyDown += TurnCageUp;
        inputManager.SKeyDown += TurnCageDown;
        inputManager.AKeyDown += TurnCageLeft;
        inputManager.DKeyDown += TurnCageRight;

        inputManager.ButtonClicked += CheckLevelComplete;

        InitLevel();
    }

    private void OnDestroy()
    {
        inputManager.WKeyDown -= TurnCageUp;
        inputManager.SKeyDown -= TurnCageDown;
        inputManager.AKeyDown -= TurnCageLeft;
        inputManager.DKeyDown -= TurnCageRight;

        inputManager.ButtonClicked -= CheckLevelComplete;
    }

    public void TurnCageDown() => TurnCage(GetVerticalAxis(), -90);
    public void TurnCageUp() => TurnCage(GetVerticalAxis(), 90);
    public void TurnCageLeft() => TurnCage(GetHorizontalAxis(), -90);
    public void TurnCageRight() => TurnCage(GetHorizontalAxis(), 90);

    private void TurnCage(Vector3 axis, float angleOffset)
    {
        if (rotationTweener != null)
            rotationTweener.Complete();

        rotationTweener = cageTurnContainer.DORotateQuaternion(cageTurnContainer.rotation * Quaternion.AngleAxis(angleOffset, axis), rotationDuartion);
        rotationTweener.OnComplete(() => rotationTweener = null);
    }

    private void ResetTurnCage()
    {
        if (rotationTweener != null)
            rotationTweener.Complete();

        rotationTweener = cageTurnContainer.DORotateQuaternion(Quaternion.identity, rotationDuartion);
    }

    private Vector3 GetVerticalAxis()
    {
        return new Vector3(Vector3.Dot(cageTurnContainer.right, Vector3.right), Vector3.Dot(cageTurnContainer.up, Vector3.right), Vector3.Dot(cageTurnContainer.forward, Vector3.right));
    }

    private Vector3 GetHorizontalAxis()
    {
        return new Vector3(Vector3.Dot(cageTurnContainer.right, Vector3.up), Vector3.Dot(cageTurnContainer.up, Vector3.up), Vector3.Dot(cageTurnContainer.forward, Vector3.up));
    }

    private void InitLevel()
    {
        SetLevel(0);
    }

    private void ClearLevel()
    {
        for(var i = 0; i < cageContentContainer.childCount; i++)
        {
            Destroy(cageContentContainer.GetChild(0).gameObject);
        }
    }

    private void SetLevel(int index)
    {
        CurrentLevelIndex = index;

        ClearLevel();
        _currentLevel = Instantiate(Levels[CurrentLevelIndex], cageContentContainer);
    }

    private void SetNextLevel()
    {
        SetLevel(++CurrentLevelIndex);
    }

    private void CheckLevelComplete(GameButton gameButton)
    {
        if (!_currentLevel.IsLevelComplete()) return;

        StartCoroutine(CompleteLevelProcess());
    }

    private IEnumerator CompleteLevelProcess()
    {
        _currentLevel.CompleteLevel();
        ResetTurnCage();

        yield return new WaitForSeconds(levelCompleteDuartion);

        SetNextLevel();
    }
}
