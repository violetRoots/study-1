using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public event Action<GameButton> ButtonClicked;
    public event Action WKeyDown;
    public event Action SKeyDown;
    public event Action AKeyDown;
    public event Action DKeyDown;

    private Camera mainCamera;
    private float dist;
    private Vector3 pos;

    private void Awake()
    {
        mainCamera = Camera.main;
        dist = Vector3.Distance(Vector3.zero, mainCamera.transform.position);
        pos = mainCamera.transform.position;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mousePos = Input.mousePosition;
            var worldMousePos = mainCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, -dist));
            Debug.DrawRay(worldMousePos, pos - worldMousePos, Color.red, 5);

            if (!Physics.Raycast(worldMousePos, pos - worldMousePos, out RaycastHit hit, 10000, LayerMask.GetMask("Buttons"))) return;

            if (!hit.collider.TryGetComponent(out GameButton gameButton)) return;

            if (!gameButton.IsClickSuccessful()) return;

            gameButton.ChangeClickedState();

            ButtonClicked?.Invoke(gameButton);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            WKeyDown?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SKeyDown?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            AKeyDown?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            DKeyDown?.Invoke();
        }
    }
}
