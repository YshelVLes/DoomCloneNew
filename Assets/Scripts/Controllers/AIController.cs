using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : BaseCharacterController
{
    bool isMoveToCompleted = true;
    NavMeshPath path;
    int pathPointIndex;

    protected override void Awake()
    {
        base.Awake();
        path = new NavMeshPath();
    }

    protected bool MoveTo(Vector3 targetPos)
    {
      bool hasPath =  NavMesh.CalculatePath(transform.position, targetPos, NavMesh.AllAreas, path);

    if(hasPath)
            pathPointIndex = 1;
  
        isMoveToCompleted = !hasPath;

      return hasPath;
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
                isMoveToCompleted = true;
                print("Done");
                return;
                
            }
            pathPointIndex++;
            targetPos = path.corners[pathPointIndex];
            targetPos.y = 0;
        }

        Vector3 direction = (targetPos - soursePos).normalized;

        MoveWorld(direction.x, direction.z);
    }
}
