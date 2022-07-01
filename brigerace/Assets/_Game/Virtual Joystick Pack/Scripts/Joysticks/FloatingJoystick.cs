using UnityEngine;
using UnityEngine.EventSystems;

public class FloatingJoystick : Joystick
{
    Vector2 joystickCenter = Vector2.zero;

    void Start()
    {
        rate = 0;
        isActive = false;
        background.gameObject.SetActive(false);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        background.gameObject.SetActive(true);
        isActive = true;
        Vector2 direction = eventData.position - joystickCenter;
        inputVector = (direction.magnitude > background.sizeDelta.x / 2f) ? direction.normalized : direction / (background.sizeDelta.x / 2f);
        handle.anchoredPosition = (inputVector * background.sizeDelta.x / 2f) * handleLimit;
        rate = Mathf.Sqrt(inputVector.magnitude);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        background.position = eventData.position;
        handle.anchoredPosition = Vector2.zero;
        joystickCenter = eventData.position;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        isActive = false;
        rate = 0;
        background.gameObject.SetActive(false);
        inputVector = Vector2.zero;
    }
}