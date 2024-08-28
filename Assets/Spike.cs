using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Kill the player
            PlayerRespawn playerRespawn = collision.GetComponent<PlayerRespawn>();
            if (playerRespawn != null)
            {
                playerRespawn.HandlePlayerDeath();
            }
        }
    }
}
