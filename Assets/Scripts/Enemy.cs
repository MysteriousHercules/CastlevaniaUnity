using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	float speed = 4.0f;

	void Update() {
		var move = new Vector3(Input.GetAxis("EnemyHorizontalTesting"), Input.GetAxis("EnemyVerticalTesting"), 0);
		transform.position += move * speed * Time.deltaTime;
	}
}