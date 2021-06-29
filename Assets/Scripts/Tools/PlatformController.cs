using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [Header("Way Points")]
    public Vector3[] localWayPoints;
    private Vector3[] globalWayPoints;
    [HideInInspector]
    public int fromWayPointIndex;
    private int toWayPointIndex;
    [HideInInspector]
    public float percentWayPoints;
    private float distanceWayPoints;

    [Header("Movement")]
    public float moveSpeed;
    public float waitTime;
    public bool cyclic;
    private Vector3 velocity;

    [Header("Flipper")]
    public bool flipVer;
    public bool flipHor;

    [Header("Easing")]
    [Range(1, 5)]
    public float easeAmount = 1;
    private float nextMoveTime;
    private float easedPercentWayPoints;

    void Start()
    {
        globalWayPoints = new Vector3[localWayPoints.Length];
        for (int i = 0; i < localWayPoints.Length; i++)
        {
            globalWayPoints[i] = localWayPoints[i] + transform.position;
        }
    }

    void FixedUpdate()
    {
        velocity = CalculateMovement();
        transform.Translate(velocity);
    }

    Vector3 CalculateMovement()
    {
        if (Time.fixedTime < nextMoveTime)
        {
            return Vector2.zero;
        }

        fromWayPointIndex %= globalWayPoints.Length;
        toWayPointIndex = (fromWayPointIndex + 1) % globalWayPoints.Length;
        distanceWayPoints = Vector3.Distance(globalWayPoints[fromWayPointIndex], globalWayPoints[toWayPointIndex]);
        percentWayPoints += Time.fixedDeltaTime * moveSpeed / distanceWayPoints;
        percentWayPoints = Mathf.Clamp01(percentWayPoints);
        easedPercentWayPoints = Ease(percentWayPoints);

        Vector3 newPos = Vector3.Lerp(globalWayPoints[fromWayPointIndex], globalWayPoints[toWayPointIndex], easedPercentWayPoints);

        if (percentWayPoints >= 1)
        {
            percentWayPoints = 0;
            fromWayPointIndex++;

            if (!cyclic)
            {
                if (fromWayPointIndex >= globalWayPoints.Length - 1)
                {
                    Vector2 localScale = gameObject.transform.localScale;
                    if (flipVer)
                    {
                        localScale.y *= -1;
                    }
                    else if (flipHor)
                    {
                        localScale.x *= -1;
                    }
                    transform.localScale = localScale;
                    fromWayPointIndex = 0;
                    System.Array.Reverse(globalWayPoints);
                }
            }
            nextMoveTime = Time.fixedTime + waitTime;
        }

        return newPos - transform.position;
    }

    float Ease(float x)
    {
        return Mathf.Pow(x, easeAmount) / (Mathf.Pow(x, easeAmount) + Mathf.Pow(1 - x, easeAmount));
    }

    void OnDrawGizmos()
    {
        if (localWayPoints != null)
        {
            Gizmos.color = Color.yellow;
            float size = 0.1f;

            for (int i = 0; i < localWayPoints.Length; i++)
            {
                Vector3 globalWayPointPos = (Application.isPlaying) ? globalWayPoints[i] : localWayPoints[i] + transform.position;
                Gizmos.DrawLine(globalWayPointPos - Vector3.up * size, globalWayPointPos + Vector3.up * size);
                Gizmos.DrawLine(globalWayPointPos - Vector3.left * size, globalWayPointPos + Vector3.left * size);
            }
        }
    }
}
