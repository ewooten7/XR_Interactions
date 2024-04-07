using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events; 

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshRobot : MonoBehaviour
{
    public UnityEvent OnDestroyWallCube;
    [SerializeField] AudioClip collisionClip;
    public AudioClip GetCollisionClip()=> collisionClip;
    NavMeshAgent agent; 
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    public void MoveAgent(Vector3 move)
    {
        agent.destination = agent.transform.position + move;
    }
    public void StopAgent()
    {
        agent.ResetPath();
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.transform.CompareTag("WallCube"))
        {
            Destroy(other.gameObject);
            OnDestroyWallCube?.Invoke();
        }
    }
}

