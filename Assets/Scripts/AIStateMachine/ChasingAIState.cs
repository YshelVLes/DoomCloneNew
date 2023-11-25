using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingAIState : AIState
{
    AIController AIController { get; }

    IEnumerator chasingCoroutine;
    public ChasingAIState(AIController aiController, AIStateMachine stateMachine) : base(stateMachine)
    {
        AIController = aiController; 
    }

    public override void Enable()
    {
       
        Coroutines.StartCoroutine(chasingCoroutine = ChasingCoroutine());
    }

    public override void Disable()
    {
        Coroutines.StopCoroutine(chasingCoroutine);
    }

    IEnumerator ChasingCoroutine()
    {
        Vector3 targetPos = Vector3.zero;

        while(true)
        {
            if(AIController.Sence.Target != null)
            {
                targetPos = AIController.Sence.Target.transform.position;
                AIController.MoveTo(targetPos);
            }

            yield return null;
        }
    }
   
}
