using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum Speaker_ { Rico = 0, Narration }

public class DialogSystem_ : MonoBehaviour
{
    [SerializeField]
    private Dialog[] dialogs;                       // ���� �б��� ��� ���
    [SerializeField]
    private Image[] imageDialogs;                   // ��ȭâ Image UI
    [SerializeField]
    private TextMeshProUGUI[] textNames;                        // ���� ������� ĳ���� �̸� ��� Text UI
    [SerializeField]
    private TextMeshProUGUI[] textDialogues;					// ���� ��� ��� Text UI
    [SerializeField]
    private GameObject[] objectArrows;                  // ��簡 �Ϸ�Ǿ��� �� ��µǴ� Ŀ�� ������Ʈ
    [SerializeField]
    private float typingSpeed;                  // �ؽ�Ʈ Ÿ���� ȿ���� ��� �ӵ�
    [SerializeField]
    private KeyCode keyCodeSkip = KeyCode.Space;	// Ÿ���� ȿ���� ��ŵ�ϴ� Ű

    private int currentIndex = -1;
    private bool isTypingEffect = false;
    private Speaker_ currentSpeaker = Speaker_.Rico;

    public void Setup()
    {
        for (int i = 0; i < 2; i++)
        {
            //��� ��ȭ ���� ���ӿ�����Ʈ ��Ȱ��ȭ
            InActiveObjects(i);
        }

        SetNextDialog();
    }

    public bool UpdateDialog()
    {
        if (Input.GetKeyDown(keyCodeSkip) || Input.GetMouseButtonDown(0))
        {
            //�ؽ�Ʈ Ÿ���� ȿ���� ������϶� ���콺 ���� Ŭ���ϸ� Ÿ���� ȿ�� ����
            if (isTypingEffect == true)
            {
                //Ÿ���� ȿ���� �����ϰ�, ���� ��� ��ü�� ����Ѵ�
                StopCoroutine("TypingText");
                isTypingEffect = false;
                textDialogues[(int)currentSpeaker].text = dialogs[currentIndex].dialogue;
                //��簡 �Ϸ�Ǿ��� �� ��µǴ� Ŀ�� Ȱ��ȭ
                objectArrows[(int)currentSpeaker].SetActive(true);

                return false;
            }

            //���� ��� ����
            if (dialogs.Length > currentIndex + 1)
            {
                SetNextDialog();
            }
            //��簡 �� �̻� ���� ��� true ��ȯ
            else
            {
                //��� ĳ���� �̹����� ��Ӱ� ����
                for (int i = 0; i < 2; i++)
                {
                    //��� ��ȭ ���� ���ӿ�����Ʈ ��Ȱ��ȭ
                    InActiveObjects(i);
                }

                return true;
            }
        }

        return false;
    }

    private void SetNextDialog()
    {
        //���� ȭ���� ��ȭ ���� ������Ʈ ��Ȱ��ȭ
        InActiveObjects((int)currentSpeaker);

        currentIndex++;

        //���� ȭ�� ����
        currentSpeaker = dialogs[currentIndex].speaker;

        //��ȭâ Ȱ��ȭ
        imageDialogs[(int)currentSpeaker].gameObject.SetActive(true);

        // ���� ȭ�� �̸� �ؽ�Ʈ Ȱ��ȭ �� ����
        textNames[(int)currentSpeaker].gameObject.SetActive(true);
        textNames[(int)currentSpeaker].text = dialogs[currentIndex].speaker.ToString();

        //ȭ���� ��� �ؽ�Ʈ Ȱ��ȭ �� ���� (Typing Effect)
        textDialogues[(int)currentSpeaker].gameObject.SetActive(true);
        StartCoroutine(nameof(TypingText));
    }

    private void InActiveObjects(int index)
    {
        imageDialogs[index].gameObject.SetActive(false);
        textNames[index].gameObject.SetActive(false);
        textDialogues[index].gameObject.SetActive(false);
        objectArrows[index].SetActive(false);
    }

    private IEnumerator TypingText()
    {
        int index = 0;

        isTypingEffect = true;

        //�ؽ�Ʈ�� �ѱ�¥�� Ÿ����ġ�� ���
        while (index < dialogs[currentIndex].dialogue.Length)
        {
            textDialogues[(int)currentSpeaker].text = dialogs[currentIndex].dialogue.Substring(0, index);

            index++;

            yield return new WaitForSeconds(typingSpeed);
        }

        isTypingEffect = false;

        //��簡 �Ϸ�Ǿ��� �� ��µǴ� Ŀ�� Ȱ��ȭ
        objectArrows[(int)currentSpeaker].SetActive(true);
    }
}

[System.Serializable]
public struct Dialog
{
    public Speaker_ speaker; // ȭ��
    [TextArea(3, 5)]
    public string dialogue;	// ���
}