using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//all triggers active
//kinds of triggers

public class Puzzle : MonoBehaviour {

    public Platform[] platforms;
	public Reward[] rewards;
	private bool rewarded = false;

    void Start()
    {
		if (platforms.Length == 0)
		{
			Debug.Log("Puzzle on |" + transform.name + "| requires at least 1 platform attached.");
			return;
		}
	}

	void Update()
	{
		if (AllActive() && !rewarded)
		{
			rewarded = true;
			DropRewards();
		}
	}

	bool AllActive()
	{
		if (platforms.Length == 0) return false;
		foreach (Platform platform in platforms)
		{
			if (!platform.active) {
				return false;
			}
		}
		return true;
	}

	public void DropRewards()
	{
		int count = 0;

		foreach (Reward reward in rewards)
		{
			if (reward != null)
			{
				count++;
				DropEachReward(reward);
			}
		}
		if (rewards.Length == 0 || count == 0)
		{
			Debug.Log("Puzzle on |" + transform.name + "| has no rewards.");
			return;
		}
	}

	private void DropEachReward(Reward reward)
	{
		Vector3 dropLocation;
		Debug.Log(transform.name);

		Transform playerTR = transform.FindChild("Player").transform;
		foreach (Transform t in playerTR) {
			Debug.Log(t.name);
		}
		Player player = null;
		if (playerTR != null) player = transform.GetComponent<Player>();
		Tile tile = gameObject.GetComponent<Tile>();
		if (player != null) {
			Debug.Log("QUACK");
			dropLocation = player.transform.position += new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), transform.position.z);
		}
		else {
			Debug.Log("MOO");
			dropLocation = transform.position;
		}
		Instantiate(reward, dropLocation, Quaternion.identity);
	}

	public int numPlatforms()
	{
		return platforms.Length;
	}
}
