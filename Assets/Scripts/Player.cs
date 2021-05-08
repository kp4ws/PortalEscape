/*
* Copyright (c) Kp4ws
*
*/

using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] float moveSpeed = 5f;
	[SerializeField] float jumpSpeed = 5f;
	[SerializeField] BoxCollider2D groundDetection = default;
	[SerializeField] BoxCollider2D wallDetection = default;
	[SerializeField] GameObject deathParticles = default;

	private bool isAlive = true;

	private Rigidbody2D myRigidbody;
	private Animator animator;


	private void Start()
	{
		myRigidbody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}

	private void Update()
	{
		if (!isAlive)
		{
			return;
		}

		Run();
		Jump();
	}


	private void Run()
	{
		float horizontalInput = Input.GetAxis("Horizontal") * moveSpeed;
		Vector2 playerVelocity = new Vector2(horizontalInput, myRigidbody.velocity.y);

		myRigidbody.velocity = playerVelocity;
	}

	private void Jump()
	{
		bool notTouchingGround = !groundDetection.IsTouchingLayers(LayerMask.GetMask("Foreground", "Climb")) && !wallDetection.IsTouchingLayers(LayerMask.GetMask("Climb"));

		if (notTouchingGround)
		{
			return;
		}

		if (Input.GetButtonDown("Jump"))
		{
			Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
			myRigidbody.velocity += jumpVelocityToAdd;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (wallDetection.IsTouchingLayers(LayerMask.GetMask("Portal")))
		{
			animator.SetBool("isTeleporting", true);
			FindObjectOfType<LevelController>().LoadNextLevel();
		}
		else
		{
			animator.SetBool("isTeleporting", false);
		}

		if (wallDetection.IsTouchingLayers(LayerMask.GetMask("Enemy")))
		{
			Die();
		}
	}

	private void Die()
	{
		SpawnDeathParticles();

		isAlive = false;
		Destroy(gameObject);
		FindObjectOfType<GameSession>().ProcessPlayerDeath();
	}

	private void SpawnDeathParticles()
	{
		Instantiate(deathParticles, transform.position, transform.rotation);
	}
}

