using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatLevelGenerator : MonoBehaviour
{
    public GameObject platformPrefab;

	public int numberOfPlatforms = 10;
	public float levelWidth = 2.5f;
	public float minY = 1.5f;
	public float maxY = 1.5f;

	void OnEnable () {

		Vector3 spawnPosition = new Vector3();

		for (int i = 0; i < numberOfPlatforms; i++)
		{
			spawnPosition.y += Random.Range(minY, maxY);
			spawnPosition.x = Random.Range(-levelWidth, levelWidth);
			Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
		}
	}
}
