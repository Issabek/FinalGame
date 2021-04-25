using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = animator.GetBool("isWalking");
        bool Forward = Input.GetKey("w");
        bool Left = Input.GetKey("a");
        bool Right = Input.GetKey("d");
        bool Backwards = Input.GetKey("s");
        bool Idle = Input.anyKeyDown;
        if (Forward)
            animator.SetBool("isWalking", true);
        else if ((Left && Forward) || Left)
        {
            animator.SetBool("sideWalkLeft", true);
        }
        else if ((Right && Forward) || Right)
        {
            animator.SetBool("sideWalkRight", true);
        }
        else if (Backwards)
            animator.SetBool("backWalk", true);
        else if (!Idle)
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("backWalk", false);
            animator.SetBool("sideWalkRight", false);
            animator.SetBool("sideWalkLeft", false);
        }

    }
}
