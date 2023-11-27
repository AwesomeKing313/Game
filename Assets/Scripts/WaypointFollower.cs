using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private Transform waypointTransform;

    private int currentwaypointIndex = 0;

    [SerializeField] private float speed = 2f;
    
    void Update()
    {
        waypointTransform = waypoints[currentwaypointIndex].transform;

        if (Vector2.Distance(waypointTransform.position, transform.position) < .1f) 
        { 
        currentwaypointIndex++;
        }

        if(currentwaypointIndex >= waypoints.Length) 
        {
            currentwaypointIndex = 0;
        }

        transform.position = Vector2.MoveTowards(transform.position, waypointTransform.position, Time.deltaTime * speed);
    }

}
