using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour {

	Transform target;
	NavMeshAgent agent;
    Animator animator;

    // Use this for initialization
    void Start () {
		agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        agent.isStopped = false;
		if (target != null) {
            if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Idle" || 
                animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Walk" || 
                animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Run" || 
                animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Grip_left" || 
                animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Grip_right" ||
                animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Combat_idle") {
                agent.SetDestination(target.position);
            } else {
                agent.isStopped = true;
                StopFollowingTarget();
                agent.SetDestination(transform.position);     
            }
            FaceTarget();
            
        }
	}
	
	public void MoveToPoint(Vector3 point) {
		agent.SetDestination(point);
	}
	
	public void FollowTarget(Interactable newTarget) {
		agent.stoppingDistance = newTarget.radius * .8f;
		agent.updateRotation = false;
		target = newTarget.interactionTransform;
	}
	
	public void StopFollowingTarget() {
		agent.stoppingDistance = 0;
		agent.updateRotation = true;
		target = null;
	}
	
	void FaceTarget() {
        if (target != null) {
            Vector3 direction = (target.position - transform.position).normalized;
            Vector3 test = new Vector3(direction.x, 0f, direction.z);
            if (test != Vector3.zero) {
                Quaternion lookRotation = Quaternion.LookRotation(test);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 9f);
            }
        }
            
	}
}
