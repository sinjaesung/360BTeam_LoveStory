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

    //말풍선(숨은그림찾기명령 말풍선)이 보이기 시작할 Conversation index값
    [SerializeField] private int ViewThinkCloud_startIndex;
    [SerializeField] private bool isCompleted = false;

    [SerializeField] public ClickEvent targetactive_ClickEvent;//클릭활성화 시킬 오브젝트
    // Start is called before the first frame update
    void Start()
    {
        dialogueCnt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isNext) //활성화가 되었을 때만 대사가 진행되도록
        {
            if(dialogueCnt >= ViewThinkCloud_startIndex)
            {
                thinkCloud.gameObject.SetActive(true);
            }
            if (dialogueCnt > dialogue.Length) // 수정된 조건
            {
                ONOFF(false); // 대사가 끝남
                return; // 더 이상 진행하지 않도록 반환
            }
        }
    }

    public void ShowDialogue()
    {
        ONOFF(true); //대화가 시작됨
        //dialogueCnt = 0;
        NextDialogue(); //호출되자마자 대사가 진행될 수 있도록 
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
            //각 Dialogue 분기의 마지막에만 한번 false로 감추는 역할
            thinkCloud.gameObject.SetActive(_flag);
        }

        isNext = _flag;
    }

    private void NextDialogue()
    {
        if (dialogueCnt < dialogue.Length) // 대화 인덱스가 범위를 벗어나지 않도록 체크
        {
            Debug.Log("Conversation dialogueIndex>>"+dialogueCnt);
            // 첫번째 대사와 첫번째 cg부터 계속 다음 cg로 진행되면서 화면에 보이게 된다.
            txt_Dialogue.text = dialogue[dialogueCnt].dialogue;
            txt_Name.text = dialogue[dialogueCnt].name;
            if (dialogue[dialogueCnt].cg != null)
            {
                sprite_CG.sprite = dialogue[dialogueCnt].cg;
                sprite_CG.gameObject.SetActive(true); // 스프라이트가 있으면 활성화
            }
            else
            {
                sprite_CG.gameObject.SetActive(false); // 스프라이트가 없으면 비활성화
                thinkCloud.gameObject.SetActive(false);
            }
            dialogueCnt++; // 다음 대사와 cg가 나오도록
            cameracontroll.isMove = false;
        }
        else if ( dialogueCnt == dialogue.Length)
        {
            ONOFF(false); // 대사가 끝났다면 UI를 비활성화
            cameracontroll.isMove = true;
            isCompleted = true;
            if (targetactive_ClickEvent != null)
            {
                targetactive_ClickEvent.CanClick = true;
            }
        }
    }
}
