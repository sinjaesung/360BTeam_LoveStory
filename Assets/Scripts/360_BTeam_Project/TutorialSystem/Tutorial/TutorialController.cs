using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{
    [SerializeField]
    private List<TutorialBase> tutorials;
    [SerializeField]
    private string nextSceneName = "";

    private TutorialBase currentTutorial = null;
    public int currentIndex = -1;

    public bool isCompleted = false;

    public Player3d_Planet player;

    [SerializeField] private bool isDynamicScene = false;
    [SerializeField] private string HappyEndingSceneName;
    [SerializeField] private string SadEndingSceneName;
    [SerializeField] private string NormalEndingSceneName;
    private void Start()
    {
        player = FindObjectOfType<Player3d_Planet>();
        SetNextTutorial();
    }

    private void Update()
    {
        if (currentTutorial != null)
        {
            currentTutorial.Execute(this);
        }
    }

    public void SetNextTutorial()
    {
        Debug.Log("SetNextTutorial >> " + currentIndex);
        // 현재 튜토리얼의 Exit() 메소드 호출
        if (currentTutorial != null)
        {
            currentTutorial.Exit();
        }

        // 마지막 튜토리얼을 진행했다면 CompletedAllTutorials() 메소드 호출
        if (currentIndex >= tutorials.Count - 1)
        {
            CompletedAllTutorials();
            return;
        }

        // 다음 튜토리얼 과정을 currentTutorial로 등록
        currentIndex++;
        currentTutorial = tutorials[currentIndex];

        // 새로 바뀐 튜토리얼의 Enter() 메소드 호출
        currentTutorial.Enter();
    }

    public void CompletedAllTutorials()
    {
        currentTutorial = null;
        isCompleted = true;
        // 행동 양식이 여러 종류가 되었을 때 코드 추가 작성
        // 현재는 씬 전환

        Debug.Log("[CompletedAllTutorials]Complete All >> 다음 타깃 씬 이동>>>");//1회(맨 마지막 도달시 첫 한번만 실행)

        if (!nextSceneName.Equals(""))
        {
            SceneManager.LoadScene(nextSceneName);
        }

        if (isDynamicScene)
        {
            if(player.LoveScore >= 10)
            {
                Debug.Log("[CompletedAllTutorials]해피앤딩씬으로 이동,씬별로 지정해둔 씬Name(happyScene)로 이동" + HappyEndingSceneName);
                SceneManager.LoadScene(HappyEndingSceneName);
            }
            else if(player.LoveScore < -8)
            {
                Debug.Log("[CompletedAllTutorials]새드앤딩씬으로 이동,씬별로 지정해둔 씬Name(SadScene)로 이동" + SadEndingSceneName);
                SceneManager.LoadScene(SadEndingSceneName);
            }
            else
            {
                Debug.Log("[CompletedAllTutorials]노말앤딩씬으로 이동,씬별로 지정해둔 씬Name(NormalScene)로 이동" + NormalEndingSceneName);
            }
        }
    }
}


