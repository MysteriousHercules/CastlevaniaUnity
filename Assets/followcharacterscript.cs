using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followcharacterscript : MonoBehaviour {

    public float range;
    private PlayerScript currentPlayer;
    private float distanceToPlayer;
    public float speedOfEnemy;
	// Use this for initialization
	void Start () {
        currentPlayer = FindObjectOfType<PlayerScript>();
	}
	
	// Update is called once per frame
	void Update () {
        if(currentPlayer == null)
        {
            currentPlayer = FindObjectOfType<PlayerScript>();
        }
        if (currentPlayer != null)
        {
            distanceToPlayer = Vector2.Distance(transform.position, currentPlayer.transform.position);
        }
        if(distanceToPlayer < range)
        {
            followPlayer();
        }
	}
    void followPlayer()
    {
        if (currentPlayer != null)
        {
            if (currentPlayer.transform.position.x < transform.position.x)
            {
                transform.position -= new Vector3(speedOfEnemy / 10, 0, 0);
                transform.SetPositionAndRotation(transform.position, new Quaternion(transform.rotation.x, 0, transform.rotation.z, transform.rotation.w));


            }
            else if(currentPlayer.transform.position.x > transform.position.x)
            {
                transform.position += new Vector3(speedOfEnemy / 10, 0, 0);
                transform.SetPositionAndRotation(transform.position, new Quaternion(transform.rotation.x, 180, transform.rotation.z, transform.rotation.w));

            }
        }
    }
    
}
