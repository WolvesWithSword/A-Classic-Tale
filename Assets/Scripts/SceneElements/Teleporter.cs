using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
	public string teleporterTag;
	public GameObject spawnPoint;
    public string targetScene = "Scene1";
	public EPlayerPosition enterPosition;
	
	private LevelLoader levelLoader;

	private void Start()
	{
		levelLoader = FindObjectOfType<LevelLoader>();
	}

	private void OnTriggerEnter2D(Collider2D collided)
	{
		if (collided.tag == "Player")
		{
			PlayerManager.Instance.BlockPlayerMovementFor(0.3f);
			GameManager.Instance.teleporterTag = teleporterTag;// For next level teleportation
			levelLoader.LoadLevel(targetScene);
		}
	}
}
