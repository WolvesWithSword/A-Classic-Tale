using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMotor : MonoBehaviour
{
	public float moveSpeed = 4f;
	public Tilemap obstacles;

	private EDirection playerLookAt;
	private Animator animator;
	private Vector2 movement;
	private Vector3 moveToPosition;
	private bool isMoving;
	private bool canMove;


	// Start is called before the first frame update
	void Awake()
	{
		animator = this.gameObject.GetComponent<Animator>();
	}

	private void Start()
	{
		canMove = true;
	}

	// Update is called once per frame
	void Update()
	{
		if (canMove)
		{
			if (Input.GetKeyUp(KeyCode.A))
			{
				Interact();
			}

			if (!isMoving)
			{
				movement.x = Input.GetAxisRaw("Horizontal");
				movement.y = Input.GetAxisRaw("Vertical");

				// To block diagonal movement
				if (movement.x != 0)
				{
					movement.y = 0;
				}

				animator.SetFloat("Horizontal", movement.x);
				animator.SetFloat("Vertical", movement.y);

				if (movement.x == 0 && movement.y == 0) return;// Stop the loop

				moveToPosition = transform.position + new Vector3(movement.x, movement.y, 0); // +- 1
				UpdatePlayerLookAt(movement.x, movement.y);
				Vector3Int obstaclesMap = obstacles.WorldToCell(moveToPosition);

				if (obstacles.GetTile(obstaclesMap) == null)// Detect walls or obstacles
				{
					AudioManager.Instance.GrassStep();
					StartCoroutine(MovePlayer(moveToPosition, moveSpeed));
				}
			}
		}
	}

	private IEnumerator MovePlayer(Vector3 targetPos, float speed)
    {
		isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon) 
        {
			transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
			yield return null;
        }
		transform.position = targetPos;// To be sure
		isMoving = false;
		canMove = true; // For after some teleportation
	}

	public void Die()
    {
		animator.SetBool("Dead", true);
		StopMoving();// To not teleport player if coroutine continue
		canMove = false;
	}

	public void Revive()
    {
		canMove = true;
		animator.SetBool("Dead", false);
	}

	public void StopMoving()
    {
		isMoving = false;
		StopAllCoroutines();
	}

	public void MovePlayerTo(Vector3 position, float speed)
	{
		StopMoving();
		canMove = false;
		StartCoroutine(MovePlayer(position, speed));
	}

	private void UpdatePlayerLookAt(float x, float y)
	{
		if (x == 1) playerLookAt = EDirection.RIGHT;
		else if (x == -1) playerLookAt = EDirection.LEFT;
		if (y == -1) playerLookAt = EDirection.DOWN;
		else if (y == 1) playerLookAt = EDirection.UP;
	}

	private Vector2 LookDirectionVector()
	{
		switch (playerLookAt)
		{
			case EDirection.RIGHT:
				return Vector2.right;
			case EDirection.LEFT:
				return Vector2.left;
			case EDirection.UP:
				return Vector2.up;
			case EDirection.DOWN:
				return Vector2.down;
			default:
				return Vector2.zero;
		}
	}

	private void Interact()
	{
		int layer = LayerMask.NameToLayer("Interactable");
		RaycastHit2D hit = Physics2D.Raycast(transform.position, LookDirectionVector(), 1.1f, 1 << layer);// layer MASK ! it's in the name T_T

		if (hit)
		{
			PlayerManager.Instance.InteractWith(hit.transform.gameObject);
		}
	}

	public void SetPlayerPosition(EPlayerPosition playerPos)
	{
		switch (playerPos)
		{
			case EPlayerPosition.LIE:
				animator.Play("BaseLayer.Player_lie");
				playerLookAt = EDirection.DOWN;
				break;
			case EPlayerPosition.RIGHT:
				animator.Play("BaseLayer.Player_walk_right");
				playerLookAt = EDirection.RIGHT;
				break;
			case EPlayerPosition.LEFT:
				animator.Play("BaseLayer.Player_walk_left");
				playerLookAt = EDirection.LEFT;
				break;
			case EPlayerPosition.UP:
				animator.Play("BaseLayer.Player_walk_up");
				playerLookAt = EDirection.UP;
				break;
			case EPlayerPosition.DOWN:
				animator.Play("BaseLayer.Player_walk_down");
				playerLookAt = EDirection.DOWN;
				break;
			case EPlayerPosition.DIE:
				animator.Play("BaseLayer.Player_die");
				playerLookAt = EDirection.DOWN;
				break;
			default:
				break;
		}
	}

	private void OnTriggerEnter2D(Collider2D collided)
	{
		if (collided.tag == "Ennemy")
		{
			PlayerManager.Instance.PlayerDie();
		}
	}

}
