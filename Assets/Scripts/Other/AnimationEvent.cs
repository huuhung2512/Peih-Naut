using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationEvent : MonoBehaviour
{
    private Animator animator;
    public Action endTransitionAction;
    private void Start() {
        animator = GetComponent<Animator>();
    }
    public void endTrans()
    {
        endTransitionAction?.Invoke();
        animator.SetBool("playAnim", false);
    }
    public void PlayAnimTransition()
    {
        animator.SetBool("playAnim", true);
    }
}
