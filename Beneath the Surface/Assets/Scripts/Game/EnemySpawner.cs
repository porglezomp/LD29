using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {

	public static EnemySpawner spawner;
	public GameObject enemy;
	public List<Enemy> enemies;
	public float frequency = 10;
	public int maxCount = 6;
	float timer = 0;

	// Use this for initialization
	void Start () {
		spawner = this;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer >= frequency && enemies.Count < maxCount) {
			timer -= frequency;
			Vector2 vec = Quaternion.Euler(0, 0, Random.Range (0, 360)) * new Vector2(0, 1);
			Enemy newEnemy = (GameObject.Instantiate(enemy, vec, Quaternion.identity) as GameObject).GetComponent<Enemy>();
			newEnemy.velocity = new Vector2d(vec.x, vec.y) * Universe.scale * 4;
			//newEnemy.position = gameObject.GetComponent<FixedBody>().position + newEnemy.velocity * 5;
		}
	}
}
