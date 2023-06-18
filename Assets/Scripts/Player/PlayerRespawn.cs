using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour {

    [SerializeField] private Transform startingRespawn;
    private Transform currentCheckpoint;
    private Health playerHealth;

    private void Start() {
        playerHealth = GetComponent<Health>();     
        currentCheckpoint = startingRespawn;   
    }

    public void RespawnPlayer() {
        transform.position = currentCheckpoint.position;
        playerHealth.Respawn();
    }

    public void ActivateCheckpoint(Transform checkPointPosition) {
        currentCheckpoint = checkPointPosition;
    }
}
