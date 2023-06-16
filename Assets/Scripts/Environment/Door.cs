using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    
    [SerializeField] private float closedTime;

    private Animator animator;
    private BoxCollider2D coll;
    
    void Start() {
        animator = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        StartCoroutine(OpenDoor());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator OpenDoor() {
        yield return new WaitForSeconds(closedTime);            
        animator.SetTrigger("Open");
    }

    public void UpdateDoorClosed() {
        StartCoroutine(OpenDoor());
        coll.enabled = true;
    }

    public void UpdateDoorOpen() {
        coll.enabled = false;
    }
}
