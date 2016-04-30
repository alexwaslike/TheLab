using UnityEngine;

public class Flip : StateMachineBehaviour {
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.transform.localScale = new Vector3(-1f, 1f, 1f);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
