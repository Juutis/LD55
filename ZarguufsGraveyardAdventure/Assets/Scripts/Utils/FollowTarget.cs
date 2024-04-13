using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private bool followX = false;
    [SerializeField]
    private bool followY = false;
    [SerializeField]
    private bool followZ = false;

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    void Update()
    {
        if (target == null)
        {
            return;
        }
        Vector3 newPos = transform.position;
        if (followX)
        {
            newPos.x = target.position.x;
        }
        if (followY)
        {
            newPos.y = target.position.y;
        }
        if (followZ)
        {
            newPos.z = target.position.z;
        }
        transform.position = newPos;
    }
}
