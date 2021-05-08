/*
* Copyright (c) Kp4ws
*
*/

using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] float horizontalMoveSpeed = 1f;
	[SerializeField] float verticalMoveSpeed = 1f;
	[SerializeField] GameObject horizontalFlipDetection = default;
	[SerializeField] GameObject verticalFlipDetection = default;
	[SerializeField] GameObject groundFlipDetection = default;

	[Tooltip("Either moving horizontally or vertically")]
	[SerializeField] bool horizontalMove = default;
	[SerializeField] bool isGrounded = default;

	private Rigidbody2D myRigidbody2D;

	private void Start()
	{
		myRigidbody2D = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		if (horizontalMove)
		{
			verticalFlipDetection.SetActive(false);

			if (isGrounded)
			{
				horizontalFlipDetection.SetActive(false);
				groundFlipDetection.SetActive(true);
			}
			else
			{
				groundFlipDetection.SetActive(false);
				horizontalFlipDetection.SetActive(true);
			}

			MoveHorizontal();
		}
		else
		{
			verticalFlipDetection.SetActive(true);
			horizontalFlipDetection.SetActive(false);
			groundFlipDetection.SetActive(false);

			MoveVertical();
		}
	}

	private void MoveHorizontal()
	{
		if (IsFacingRight())
		{
			myRigidbody2D.velocity = new Vector2(horizontalMoveSpeed, myRigidbody2D.velocity.y);
		}
		else
		{
			myRigidbody2D.velocity = new Vector2(-horizontalMoveSpeed, myRigidbody2D.velocity.y);
		}
	}

	private void MoveVertical()
	{
		if (IsFacingUp())
		{
			myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, verticalMoveSpeed);
		}
		else
		{
			myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, -verticalMoveSpeed);
		}
	}

	private bool IsFacingRight()
	{
		if (isGrounded)
		{
			return groundFlipDetection.transform.localScale.x > 0;
		}
		else
		{
			return horizontalFlipDetection.transform.localScale.x > 0;
		}
	}

	private bool IsFacingUp()
	{
		return verticalFlipDetection.transform.localScale.y > 0;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (!isGrounded || collision.GetComponent<Player>())
		{
			return;
		}

		//Enemy can only move horizontally when grounded
		groundFlipDetection.transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody2D.velocity.x)), transform.localScale.y);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (isGrounded || collision.GetComponent<Player>())
		{
			return;
		}

		//Enemy can move both either horizontally and vertically in air
		if (horizontalMove)
		{
			horizontalFlipDetection.transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody2D.velocity.x)), transform.localScale.y);
		}
		else
		{
			verticalFlipDetection.transform.localScale = new Vector2(transform.localScale.x, -(Mathf.Sign(myRigidbody2D.velocity.y)));
		}
	}
}

