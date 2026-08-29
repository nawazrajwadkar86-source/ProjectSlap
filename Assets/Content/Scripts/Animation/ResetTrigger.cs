using UnityEngine;

public class ResetTrigger : StateMachineBehaviour
{
    public string triggerName;

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger(triggerName);
    }
}
