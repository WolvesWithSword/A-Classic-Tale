using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMotor : MonoBehaviour
{
	public float moveSpeed = 0.2f;
	public Tilemap obstacles;

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
        if (!isMoving && canMove)
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
			Vector3Int obstaclesMap = obstacles.WorldToCell(moveToPosition);

			if (obstacles.GetTile(obstaclesMap) == null)// Detect walls or obstacles
			{
				AudioManager.Instance.GrassStep();
				StartCoroutine(MovePlayer(moveToPosition));
			}
		}
	}

	private IEnumerator MovePlayer(Vector3 targetPos)
    {
		isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon) 
        {
			transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
			yield return null;
        }
		transform.position = targetPos;// To be sure
		isMoving = false;
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

	public void SetPlayerPosition(EPlayerPosition playerPos)
	{
		switch (playerPos)
		{
			case EPlayerPosition.LIE:
				animator.Play("BaseLayer.Player_lie");
				break;
			case EPlayerPosition.RIGHT:
				animator.Play("BaseLayer.Player_walk_right");
				break;
			case EPlayerPosition.LEFT:
				animator.Play("BaseLayer.Player_walk_left");
				break;
			case EPlayerPosition.UP:
				animator.Play("BaseLayer.Player_walk_up");
				break;
			case EPlayerPosition.DOWN:
				animator.Play("BaseLayer.Player_walk_down");
				break;
			case EPlayerPosition.DIE:
				animator.Play("BaseLayer.Player_die");
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
