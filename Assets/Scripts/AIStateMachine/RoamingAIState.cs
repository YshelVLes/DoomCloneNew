using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoamingAIState : AIState
{
    public AIController AIController { get; }
    public RoamingAIState(AIController aIController, AIStateMachine stateMachine) : base(stateMachine)
    {
        AIController = aIController;
    }

    public override void Enable()
    {
        AIController.MoveTo(GetRandomPosInRadius(10), HandleMoveToCompleted);
        AIController.Sence.TargetChanged += HandleTargetChanged;
    }

    public override void Disable()
    {
        AIController.Sence.TargetChanged -= HandleTargetChanged;
    }

    void HandleTargetChanged(DamagableComponent target)
    {
        if (target != null)
        {
            AIController.AbortMoveTo();
            ChangeState("Chasing");
        }
    }

    //Handle - обработчик, а не просто функция 
    void HandleMoveToCompleted(MoveToCompletedReason reason)
    {
        if(reason != MoveToCompletedReason.Succsess)
            return;

        ChangeState("Roaming");
    }

    Vector3 GetRandomPosInRadius(float radius)
    {
       Vector3 randomDirection = Random.insideUnitSphere * radius;
       Vector3 targetPos = AIController.transform.position + randomDirection;

        if (NavMesh.SamplePosition(targetPos, out NavMeshHit hit, radius, NavMesh.AllAreas))
            return hit.position;
        else 
            return AIController.transform.position;
    }
}
