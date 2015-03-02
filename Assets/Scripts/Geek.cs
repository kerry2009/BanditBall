using UnityEngine;
using System.Collections;

public class Geek : MonoBehaviour {
	public Transform floor;
	public ArenaGameManager gameManager;

	private Vector3 moveVect = new Vector3();

	public float speedX;
	public float speedY;
	public float friction;
	private Animator animator;

	public bool paused = false;

	CircleCollider2D circleCollider2d;

	void Start () {
		paused = false;
		speedX = 0f;
		speedY = 0f;
		friction = 0.6f;

		circleCollider2d = GetComponent<CircleCollider2D>();
		animator = GetComponent<Animator> ();
	}

	void OnTriggerEnter2D(Collider2D other) {
		Enemy enemy = other.gameObject.GetComponent<Enemy> ();

		if (enemy && !enemy.isDead) {
			enemy.OnEnemyDead();

			if (enemy.gameObject.tag == "FloorEnemy") {
				enemy.moveYSpeed = -0.1f;
				enemy.moveXSpeed = gameManager.geek.speedX * 0.8f;
			}

			// lift up geek
			speedY += 0.4f;
		}
	}

	void FixedUpdate() {
		if (paused) {
			return;
		}

		moveVect.x = transform.position.x;
		moveVect.y = transform.position.y;
		moveVect.z = transform.position.z;
		
		speedY += gameManager.gravity;

		// check hit floor
		if (moveVect.y - circleCollider2d.radius < floor.position.y) {
			moveVect.y = floor.position.y + circleCollider2d.radius;

			speedX *= friction * 0.5f;
			speedY *= -friction;
			// Debug.Log("spX:" + speedX + ", spY:" + speedY + ", friction:" + friction);

			moveVect.x += speedX;
			moveVect.y += speedY;

			// check is dead
			if (speedX < 0.005f) {
				moveVect.y = floor.position.y + circleCollider2d.radius;
				if (!gameManager.gameOvered) {
					gameManager.gameOver();
					playDeadAnimation();
				}
			} else {
				playFlyAnimation(Random.Range(2, 5));
			}
		} else {
			moveVect.x += speedX;
			moveVect.y += speedY;
		}

		transform.position = moveVect;
	}

	public void playFlyAnimation(int state) {
		animator.Play ("GeekFly" + state);
	}

	public void playDeadAnimation() {
		animator.Play ("GeekDead" + Random.Range(1, 3));
	}

}
