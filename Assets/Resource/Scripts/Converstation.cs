using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Dialogue
{
    [TextArea]
    public string name;
    public string dialogue;
    public Sprite cg;
}
public class Converstation : MonoBehaviour
{
    [SerializeField] Image sprite_CG;
    [SerializeField] Image sprite_Box;
    [SerializeField] TMP_Text txt_Name;
    [SerializeField] TMP_Text txt_Dialogue;
    [SerializeField] GameObject nameBox;
    [SerializeField] Image thinkCloud;
    [SerializeField] bool isNext = false;
    [SerializeField] int dialogueCnt = 0;
    [SerializeField] Dialogue[] dialogue;
    [SerializeField] CameraControll cameracontroll;

    //��ǳ��(�����׸�ã���� ��ǳ��)�� ���̱� ������ Conversation index��
    [SerializeField] private int ViewThinkCloud_startIndex;
    [SerializeField] private bool isCompleted = false;

    [SerializeField] public ClickEvent targetactive_ClickEvent;//Ŭ��Ȱ��ȭ ��ų ������Ʈ
    // Start is called before the first frame update
    void Start()
    {
        dialogueCnt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isNext) //Ȱ��ȭ�� �Ǿ��� ���� ��簡 ����ǵ���
        {
            if(dialogueCnt >= ViewThinkCloud_startIndex)
            {
                thinkCloud.gameObject.SetActive(true);
            }
            if (dialogueCnt > dialogue.Length) // ������ ����
            {
                ONOFF(false); // ��簡 ����
                return; // �� �̻� �������� �ʵ��� ��ȯ
            }
        }
    }

    public void ShowDialogue()
    {
        ONOFF(true); //��ȭ�� ���۵�
        //dialogueCnt = 0;
        NextDialogue(); //ȣ����ڸ��� ��簡 ����� �� �ֵ��� 
    }
    private void ONOFF(bool _flag)
    {
        sprite_Box.gameObject.SetActive(_flag);
        sprite_CG.gameObject.SetActive(_flag);
        txt_Dialogue.gameObject.SetActive(_flag);
        nameBox.gameObject.SetActive(_flag);
        thinkCloud.gameObject.SetActive(false);

        if (isCompleted)
        {
            //�� Dialogue �б��� ���������� �ѹ� false�� ���ߴ� ����
            thinkCloud.gameObject.SetActive(_flag);
        }

        isNext = _flag;
    }

    private void NextDialogue()
    {
        if (dialogueCnt < dialogue.Length) // ��ȭ �ε����� ������ ����� �ʵ��� üũ
        {
            Debug.Log("Conversation dialogueIndex>>"+dialogueCnt);
            // ù��° ���� ù��° cg���� ��� ���� cg�� ����Ǹ鼭 ȭ�鿡 ���̰� �ȴ�.
            txt_Dialogue.text = dialogue[dialogueCnt].dialogue;
            txt_Name.text = dialogue[dialogueCnt].name;
            if (dialogue[dialogueCnt].cg != null)
            {
                sprite_CG.sprite = dialogue[dialogueCnt].cg;
                sprite_CG.gameObject.SetActive(true); // ��������Ʈ�� ������ Ȱ��ȭ
            }
            else
            {
                sprite_CG.gameObject.SetActive(false); // ��������Ʈ�� ������ ��Ȱ��ȭ
                thinkCloud.gameObject.SetActive(false);
            }
            dialogueCnt++; // ���� ���� cg�� ��������
            cameracontroll.isMove = false;
        }
        else if ( dialogueCnt == dialogue.Length)
        {
            ONOFF(false); // ��簡 �����ٸ� UI�� ��Ȱ��ȭ
            cameracontroll.isMove = true;
            isCompleted = true;
            if (targetactive_ClickEvent != null)
            {
                targetactive_ClickEvent.CanClick = true;
            }
        }
    }
}
