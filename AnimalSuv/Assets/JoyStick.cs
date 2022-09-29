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

        // vec값을 m_fRadius 이상이 되지 않도록 합니다.
        vec = Vector2.ClampMagnitude(vec, radius);

        rectJoystick.anchoredPosition = vec;

        // 조이스틱 배경과 조이스틱과의 거리 비율로 이동합니다.
        float fSqr = rectJoystick.anchoredPosition.sqrMagnitude / (radius * radius);

        // 터치위치 정규화
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
        // 원래 위치로 되돌립니다.
        rectJoystick.anchoredPosition = Vector2.zero;
        touch = false;
    }
}
