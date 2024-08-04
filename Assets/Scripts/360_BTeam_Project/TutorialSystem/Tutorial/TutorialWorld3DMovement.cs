using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialWorld3DMovement : TutorialBase
{
    public enum CompletedAction { Greeting = 0, Talk }

    [SerializeField]
    private Transform pos;
    [SerializeField]
    private Vector3 endPosition;//이동할 공간상의 위치(플레이어의 바로 앞으로)
    private bool isCompleted = false;
    private Animator animator;

    public CompletedAction completeAction;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public override void Enter()
    {
        Debug.Log("TutorialWorld3DMovement Enter>>");
        StartCoroutine(nameof(Movement));
    }

    public override void Execute(TutorialController controller)
    {
        if (isCompleted == true)
        {
            if (completeAction == CompletedAction.Greeting)
            {
                Debug.Log("TutorialMovement3D CompletedAction Greeting>>");
                Greeting();
            }
            controller.SetNextTutorial();
        }
    }

    public void Greeting()
    {
        animator.SetTrigger("Greeting");
    }
    public override void Exit()
    {
        Debug.Log("TutorialWorld3DMovement Exit>>");
    }
    private IEnumerator Movement()
    {
        float current = 0;
        float percent = 0;
        float moveTime = 1.2f;
        Vector3 start = pos.position;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / moveTime;

            pos.position = Vector3.Lerp(start, endPosition, percent);

            yield return null;
        }

        isCompleted = true;
    }
}
