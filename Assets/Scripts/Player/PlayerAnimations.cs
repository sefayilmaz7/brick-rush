using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : SingletonBehaviour<PlayerAnimations>
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Ragdoll ragdoll;
    [SerializeField] private GameObject tray;

    public void BackToIdleAnim()
    {
        playerAnimator.SetBool(AnimationVariables.Run , false);
    }

    public void SwitchToRunAnim()
    {
        playerAnimator.SetBool(AnimationVariables.Run , true);
    }

    public void EnableRagdoll()
    {
        var meshRenderer = tray.GetComponent<MeshRenderer>();
        //meshRenderer.enabled = false;
        playerAnimator.gameObject.GetComponent<Collider>().enabled = false;
        ragdoll.Open();
    }
}
