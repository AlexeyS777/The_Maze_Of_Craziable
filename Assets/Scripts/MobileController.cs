using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MobileController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image joystickBg;
    private Image joystick;
    private Vector2 inputVector;

    private void Start()
    {
        joystickBg = GetComponent<Image>();
        joystick = transform.GetChild(0).GetComponent<Image>();
    }    

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        joystick.rectTransform.anchoredPosition = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBg.rectTransform, eventData.position, eventData.pressEventCamera, out pos))
        {
            pos.x = (pos.x / joystickBg.rectTransform.sizeDelta.x); // ��������� ��������� ������� ������� �� ��������
            pos.y = (pos.y / joystickBg.rectTransform.sizeDelta.x); // ��������� ��������� ������� ������� �� ��������

            inputVector = new Vector2(pos.x * 2, pos.y * 2); // ��������� ������ ��������� �� �������
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized /*������ �������� � ������ �������*/ : inputVector;
            joystick.rectTransform.anchoredPosition = new Vector2(inputVector.x *(joystickBg.rectTransform.sizeDelta.x / 3 /*�� ������� ������ �������� ������� �� ������� �������*/), inputVector.y * (joystickBg.rectTransform.sizeDelta.y / 3));
        }
    }

    public float inputVertical()
    {        
        return inputVector.y;
    }

    public float inputHorizontal()
    {        
        return inputVector.x;
    }
}
