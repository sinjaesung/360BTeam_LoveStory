using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoveGameManager : MonoBehaviour
{
    private Player3d_Planet player;

    [SerializeField] private Image Slime_Conversation_stateImage; 
    [SerializeField] private Image LoveMonsterState;

    [SerializeField] private Sprite[] images;

    TutorialController tController;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player3d_Planet>();

        tController = FindObjectOfType<TutorialController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.LoveScore <= -10)
        {
            Debug.Log("슬라임화남");
            Slime_Conversation_stateImage.sprite = images[0];
            LoveMonsterState.sprite = images[0];
        }
        else if(player.LoveScore >-10 && player.LoveScore< 0)
        {
            Debug.Log("슬라임놀람"); 
            Slime_Conversation_stateImage.sprite = images[1];
            LoveMonsterState.sprite = images[1];
        }
        else if(player.LoveScore >=0 && player.LoveScore < 10)
        {
            Debug.Log("슬라임평상시");
            Slime_Conversation_stateImage.sprite = images[2];
            LoveMonsterState.sprite = images[2];
        }
        else if (player.LoveScore >= 10 && player.LoveScore < 26)
        {
            Debug.Log("슬라임지긋이");
            Slime_Conversation_stateImage.sprite = images[3];
            LoveMonsterState.sprite = images[3];
        }
        else if (player.LoveScore >= 26)
        {
            Debug.Log("슬라임기쁨");
            Slime_Conversation_stateImage.sprite = images[4];
            LoveMonsterState.sprite = images[4];
        }

        if (tController.isCompleted)
        {
            if (player.LoveScore >= 10)
            {
                Debug.Log("슬라임기쁨 결말 기쁨 결말 씬으로 이동!!!");
            }
            else if(player.LoveScore < -8)
            {
                Debug.Log("슬라임화남 결말 화남 결말 씬으로 이동!!!");
            }
            else
            {
                Debug.Log("슬라임평범 결말 평범 노말 씬으로 이동!!!");
            }
        }
    }
}
