using UnityEngine;
using UnityEngine.InputSystem;

public class ResetManager : MonoBehaviour
{
    public GameObject resetUI;          // the reset panel
    public InputActionReference menuButton;  // which button triggers reset UI

    void OnEnable()
    {
        menuButton.action.Enable();
        menuButton.action.performed += OpenResetUI;
    }

    void OnDisable()
    {
        menuButton.action.performed -= OpenResetUI;
        menuButton.action.Disable();
    }

    private void OpenResetUI(InputAction.CallbackContext ctx)
    {
        resetUI.SetActive(true);
    }

    public void CloseResetUI()
    {
        resetUI.SetActive(false);
    }
}

