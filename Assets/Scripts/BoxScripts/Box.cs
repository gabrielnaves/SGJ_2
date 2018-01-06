using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {

    public BoxType type;

    Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
        if (type == BoxType.RED)
            animator.SetTrigger("toRed");
        else if (type == BoxType.GREEN)
            animator.SetTrigger("toGreen");
        else if (type == BoxType.BLUE)
            animator.SetTrigger("toBlue");
        else
            animator.SetTrigger("toDefault");
    }

    void OnCollisionEnter2D(Collision2D other) {
        var otherBox = other.gameObject.GetComponent<Box>();
        if (otherBox) {
            if (type == BoxType.RED && otherBox.type == BoxType.GREEN)
                Debug.Log("Red on green collision");
            else if (type == BoxType.GREEN && otherBox.type == BoxType.BLUE)
                Debug.Log("Green on blue collision");
            else if (type == BoxType.BLUE && otherBox.type == BoxType.RED)
                Debug.Log("Blue on red collision");
        }
    }
}

public enum BoxType {
    GREEN,
    BLUE,
    RED,
    DEFAULT
}
