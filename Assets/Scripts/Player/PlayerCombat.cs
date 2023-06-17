using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour {

    private Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
    }

    void Update() {
        if (Input.GetButtonDown("Fire1")) {
            Attack();
        }
    }

    void Attack() {
        animator.SetTrigger("attack");
    }
}
