using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;
    

public class FollowCircuit : MonoBehaviour
{
    [SerializeField] WaypointCircuit circuit;

    [SerializeField] float speed;

    Vector3 target;
    int currentWaypoint;
    // Start is called before the first frame update
    void Start()
    {
        currentWaypoint = 0;
        target = circuit.Waypoints[currentWaypoint].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, target) <= 0.25f)
        {
            currentWaypoint++;
            if (currentWaypoint >= circuit.Waypoints.Length) currentWaypoint = 0;

            target = circuit.Waypoints[currentWaypoint].position;
        }
        transform.position = Vector3.Lerp(transform.position, target, speed * Time.deltaTime);
    }
}
