using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IPointerDownHandler, IDragHandler,IPointerUpHandler
{
    RectTransform rectBack;
    RectTransform rectJoystick;

    Transform target;
    float radius = 0f;
    float speed = 5.0f;
    float sqr = 0f;

    Vector3 vecMove;

    bool touch = false;

    private void Start()
    {
        rectBack = transform.Find("Joystickback").GetComponent<RectTransform>();
        rectJoystick = transform.Find("Joystickback/Joystick").GetComponent<RectTransform>();

        radius = rectBack.rect.width * 0.5f;
    }

    private void Update()
    {
        if (touch)
        {
            target.position += vecMove;
        }
    }

    public void SetTarget(Transform tr)
    {
        target = tr;
    }

    void OnTouch(Vector2 vecTouch)
    {
        Vector2 vec = new Vector2(vecTouch.x - (rectBack.position.x + radius), vecTouch.y - (rectBack.position.y + radius));

        // vec���� m_fRadius �̻��� ���� �ʵ��� �մϴ�.
        vec = Vector2.ClampMagnitude(vec, radius);

        rectJoystick.anchoredPosition = vec;

        // ���̽�ƽ ���� ���̽�ƽ���� �Ÿ� ������ �̵��մϴ�.
        float fSqr = rectJoystick.anchoredPosition.sqrMagnitude / (radius * radius);

        // ��ġ��ġ ����ȭ
        Vector2 vecNormal = vec.normalized;

        vecMove = new Vector3(vecNormal.x * speed * Time.deltaTime * fSqr, 0f, vecNormal.y * speed * Time.deltaTime * fSqr);
        target.eulerAngles = new Vector3(0f, Mathf.Atan2(vecNormal.x, vecNormal.y) * Mathf.Rad2Deg, 0f);
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnTouch(eventData.position);
        touch = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnTouch(eventData.position);
        touch = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // ���� ��ġ�� �ǵ����ϴ�.
        rectJoystick.anchoredPosition = Vector2.zero;
        touch = false;
    }
}
