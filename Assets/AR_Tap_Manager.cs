using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-100)]

public class AR_Tap_Manager : MonoBehaviour
{
    public static int LastTapConsumedFrame = -1;
    public static bool TapConsumedThisFrame => LastTapConsumedFrame == Time.frameCount;

    private Rigidbody rb;
    public float bounceForce;

    Camera m_Cam;

    void Start(){
        m_Cam = Camera.main;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!TryGetTap(out Vector2 screenPos, out int fingerId))
            return;

        // Ignore les taps sur l'UI
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(fingerId))
            return;

        Ray ray = m_Cam.ScreenPointToRay(screenPos);
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform.IsChildOf(transform))
            OnTapped();
    }

    bool TryGetTap(out Vector2 pos, out int id)
    {
        pos = default; id = -1;

        var touch = Touchscreen.current?.primaryTouch;
        if (touch != null && touch.press.wasPressedThisFrame)
        {
            pos = touch.position.ReadValue();
            id = touch.touchId.ReadValue();
            return true;
        }

        // Test dans l'éditeur (souris)
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            pos = Mouse.current.position.ReadValue();
            return true;
        }
        return false;
    }

    void OnTapped()
    {
        LastTapConsumedFrame = Time.frameCount;
        rb.AddForce(Vector2.up * bounceForce, ForceMode.Impulse);
    }
}