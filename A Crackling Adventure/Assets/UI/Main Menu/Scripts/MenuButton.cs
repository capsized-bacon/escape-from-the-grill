using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    [SerializeField] public MenuButtonController menuButtonController;
    [SerializeField] public Animator animator;
    [SerializeField] public AnimatorFunctions animatorFunctions;
    [SerializeField] public int thisIndex;

    // Update is called once per frame
    void Update()
    {
        if (menuButtonController.index == thisIndex)
        {
            animator.SetBool("selected", true);
            if (Input.GetAxis("Submit") == 1)
            {
                animator.SetBool("pressed", true);
            }
            else if (animator.GetBool("pressed"))
            {
                animator.SetBool("pressed", false);
                animatorFunctions.disableOnce = true;
            }
        }
        else
        {
            animator.SetBool("selected", false);
        }
    }
}

// REFERENCES

/*“Make A Gorgeous Start Menu(Unity UI Tutorial)! - YouTube”. [Online].
Available: https://www.youtube.com/watch?v=vqZjZ6yv1lA.
	[Accessed: 29-Jun.-2020].*/
