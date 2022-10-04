using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class FollowCircuit : MonoBehaviour
{

    [SerializeField] WaypointCircuit circuit;

    [SerializeField] float speed, maxRotation;

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
        if (Vector3.Distance(transform.position, target) <= 1f)
        {
            currentWaypoint++;
            if (currentWaypoint >= circuit.Waypoints.Length) currentWaypoint = 0;

            target = circuit.Waypoints[currentWaypoint].position;
        }
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        Vector3 direction = Vector3.RotateTowards(transform.forward, (target - transform.position).normalized, maxRotation * Time.deltaTime, 0);
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
