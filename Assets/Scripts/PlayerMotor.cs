using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMotor : MonoBehaviour
{
	private bool isMoving;
	public float moveSpeed = 0.2f;
	public Tilemap obstacles;

	private Vector2 movement;
	private Vector3 moveToPosition;

	// Start is called before the first frame update
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
        if (!isMoving)
        {
			movement.x = Input.GetAxisRaw("Horizontal");
			movement.y = Input.GetAxisRaw("Vertical");

			// To block diagonal movement
			if (movement.x != 0)
			{
				movement.y = 0;
			}

			moveToPosition = transform.position + new Vector3(movement.x, movement.y, 0); // +- 1
			Vector3Int obstaclesMap = obstacles.WorldToCell(moveToPosition);

			if(obstacles.GetTile(obstaclesMap) == null)
			{
				StartCoroutine(MovePlayer(moveToPosition));
			}
		}
	}

	private IEnumerator MovePlayer(Vector3 targetPos)
    {
		isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon) 
        {
			transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.fixedDeltaTime);
			yield return null;
        }
		transform.position = targetPos;
		isMoving = false;
	}
}
