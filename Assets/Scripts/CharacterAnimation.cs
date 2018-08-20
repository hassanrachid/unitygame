using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimation : MonoBehaviour {

    public AnimationClip replaceableAttackAnim;
    public AnimationClip[] defaultAttackAnimSet;
    // Array of animation clips
    protected AnimationClip[] currentAttackAnimSet;

	const float aniTime = .1f;
	protected Animator animator;
    protected CharacterCombat combat;
    protected CharacterSkills skills;
    public AnimatorOverrideController overrideController;
	NavMeshAgent agent;
	
	// Use this for initialization
	protected virtual void Start () {
		agent = GetComponent<NavMeshAgent>();
		animator = GetComponentInChildren<Animator>();
        combat = GetComponent<CharacterCombat>();
        skills = GetComponent<CharacterSkills>();

        // Allows us to swap animator with other clips (depending on what weapon)
        if (overrideController == null) {
            overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        }
        
        animator.runtimeAnimatorController = overrideController;
        //  Example : overrideController["Punch"] = "Sword"

        currentAttackAnimSet = defaultAttackAnimSet;
        combat.OnAttack += OnAttack;
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		float speedPercent = agent.velocity.magnitude / agent.speed;
		animator.SetFloat("speedPercent", speedPercent, aniTime, Time.deltaTime);
        animator.SetBool("inCombat", combat.InCombat);
	}


    protected virtual void OnAttack() {
        animator.SetTrigger("attack");
        int attackIndex = Random.Range(0, currentAttackAnimSet.Length);
        overrideController[replaceableAttackAnim.name] = currentAttackAnimSet[attackIndex];
    }
}
