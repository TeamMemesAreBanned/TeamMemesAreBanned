using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{
    public Vector3 offset;          // The offset at which the Health Bar follows the player.
    public bool isFollowing = false;

    private GameObject player;       // Reference to the player.


    void Awake()
    {
        // Setting up the reference.
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (isFollowing)
        {
            // Set the position to the player's position with the offset.
            transform.position = player.transform.position + offset;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isFollowing = true;
            GameManager.instance.itemCollected = true;
        }
    }
}
