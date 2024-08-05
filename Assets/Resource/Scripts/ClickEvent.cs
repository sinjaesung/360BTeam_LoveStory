using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickEvent : MonoBehaviour, IPointerEnterHandler
    , IPointerExitHandler
{
    [SerializeField] GameObject Target_Dialogue;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    [SerializeField] CameraControll camera;
    public bool CanClick = false;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }
    public void ClickSlime()
    {
        if (CanClick)
        {
            //���ʿ� �ѹ��� Ŭ���Ҽ��ְ��ϰ�,Ŭ���ϸ� ��ȭâ �߰�(ī�޶� ���۾ȵǰ�)
            Target_Dialogue.SetActive(true);
            camera.isMove = false;
            CanClick = false;
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (CanClick)
        {
            //���� ���� �� Ŭ���� ��쿡�� ȣ��ȿ���߰�
            GetComponent<SpriteRenderer>().color = new Color(200f / 255f, 250f / 255f, 250f / 255f);
            Debug.Log("Enter");
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<SpriteRenderer>().color = originalColor;
        Debug.Log("Exit");
    }
}
