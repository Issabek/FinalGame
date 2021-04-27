using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class animationStateController : MonoBehaviour
{
    Animator animator;
    void Start()
    {
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        //gameObject.GetComponent<NetworkAnimator>().send(0, true);
    }
    public  void GotHit()
    {
        animator.SetBool("isFired",true);
        animator.SetBool("isFired",false);
    }
    void Update()
    {
        bool Forward = Input.GetKey("w");
        bool ForwardUp = Input.GetKeyUp("w");
        bool Left = Input.GetKey("a");
        bool LeftUp = Input.GetKeyUp("a");
        bool Right = Input.GetKey("d");
        bool RightUp = Input.GetKeyUp("d");

        bool Backwards = Input.GetKey("s");
        bool BackwardsUp = Input.GetKeyUp("s");

        bool Idle = Input.anyKey;
        
        if (Forward && !ForwardUp)
        {
            animator.SetBool("sideWalkRight", false);
            animator.SetBool("sideWalkLeft", false);
            animator.SetBool("backWalk", false);
            animator.SetBool("turnRight", false);
            animator.SetBool("turnLeft", false);
            animator.SetBool("isWalking", true);
        }
        else if (Backwards && !BackwardsUp)
        {
            animator.SetBool("sideWalkRight", false);
            animator.SetBool("sideWalkLeft", false);
            animator.SetBool("backWalk", true);
            animator.SetBool("turnRight", false);
            animator.SetBool("turnLeft", false);
            animator.SetBool("isWalking", false);
        }
        if (Left && !LeftUp)
        {
            animator.SetBool("sideWalkRight", false);
            animator.SetBool("sideWalkLeft", true);
            animator.SetBool("backWalk", false);
            animator.SetBool("turnRight", false);
            animator.SetBool("turnLeft", false);
            animator.SetBool("isWalking", false);

        }
        else if(Right && !RightUp)
        {
            animator.SetBool("sideWalkRight", true);
            animator.SetBool("sideWalkLeft", false);
            animator.SetBool("backWalk", false);
            animator.SetBool("turnRight", false);
            animator.SetBool("turnLeft", false);
            animator.SetBool("isWalking", false);
        }
        else if(!Forward && !Backwards && !Right && !Left && Input.GetAxis("Mouse X")==0)
        {
            animator.SetBool("turnRight", false);
            animator.SetBool("turnLeft", false);
            animator.SetBool("isWalking", false);
            animator.SetBool("backWalk", false);
            animator.SetBool("sideWalkRight", false);
            animator.SetBool("sideWalkLeft", false);
        }

        if (Input.GetAxis("Mouse X") > 0 && !Forward)
        {
            animator.SetBool("turnLeft", false);
            animator.SetBool("turnRight", true);
        }
        else if (Input.GetAxis("Mouse X") < 0 && !Forward )
        {
            animator.SetBool("turnRight", false);
            animator.SetBool("turnLeft", true);
        }

        if (Input.GetMouseButtonDown(0) && !Input.GetMouseButtonUp(0))
            animator.SetBool("isFired", true);
        else
            animator.SetBool("isFired", false);
    }
}
