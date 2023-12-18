using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

public enum MoveToCompletedReason
{
    Succsess,
    Failure, 
    Aborted
}

[RequireComponent(typeof(AISense))]
public class AIController : BaseCharacterController
{
    bool isMoveToCompleted = true;
    AISense sence;
    NavMeshPath path;
    int pathPointIndex;

    public AISense Sence => sence;

    Action<MoveToCompletedReason> moveToCompleted;

    protected override void Awake()
    {
        base.Awake();
        sence = GetComponent<AISense>();
        path = new NavMeshPath();
    }

    public bool MoveTo(Vector3 targetPos, Action<MoveToCompletedReason> completed = null)
    {
      AbortMoveTo();

      moveToCompleted = completed;

      bool hasPath =  NavMesh.CalculatePath(transform.position, targetPos, NavMesh.AllAreas, path);

    if (hasPath)
        {
            if(path.corners.Length == 1)
            {
                InvokeMoveToCompleted(MoveToCompletedReason.Succsess);
                return true;
            }
            pathPointIndex = 1;
        }
          

        isMoveToCompleted = false;

    if(!hasPath)

            InvokeMoveToCompleted(MoveToCompletedReason.Failure);

        return hasPath;
    }

    public void AbortMoveTo()
    {
        InvokeMoveToCompleted(MoveToCompletedReason.Aborted);
    }

    protected virtual void Update()
    {
        if(path.status != NavMeshPathStatus.PathInvalid)
        {
            for(int i = 0; i < path.corners.Length - 1; i++)
            {
                Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
            }
          
        }

        if (isMoveToCompleted)
            return;
        Vector3 targetPos = path.corners[pathPointIndex];
        Vector3 soursePos = transform.position;

        targetPos.y = 0;
        soursePos.y = 0;

        if(Vector3.Distance(soursePos, targetPos) < 1)
        {
            if(pathPointIndex + 1 >= path.corners.Length)
            {
                InvokeMoveToCompleted(MoveToCompletedReason.Succsess);
                return;
                
            }
            pathPointIndex++;
            targetPos = path.corners[pathPointIndex];
            targetPos.y = 0;
        }

        Vector3 direction = (targetPos - soursePos).normalized;

        SetRotation(Quaternion.LookRotation(direction, transform.up).eulerAngles.y);
        MoveWorld(direction.x, direction.z);
    }

    void InvokeMoveToCompleted(MoveToCompletedReason reason)
    {
        isMoveToCompleted = true;

        Action<MoveToCompletedReason> action = moveToCompleted;
        moveToCompleted = null;
        action?.Invoke(reason);
    }
}
