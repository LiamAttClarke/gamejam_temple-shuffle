using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//all triggers active
//kinds of triggers

public class Puzzle : MonoBehaviour {

    public Platform[] platforms;
	public Reward[] rewards;
	private bool rewarded = false;

	void Awake()
	{

	}

    void Start()
    {
		LockDoors();
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
			GiveRewards();
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

	public void GiveRewards()
	{
		foreach (Reward reward in rewards)
		{
			if (reward.GetType() == typeof(Door))
			{
				OpenDoor((Door)reward);
			}
			else if (reward.GetType() != typeof(Door))
			{
				DropEachReward(reward);
			}
			else if (reward != null)
			{

			}
		}
	}

	private void DropEachReward(Reward reward)
	{
		Vector3 dropLocation;
		Transform playerTransform = transform.FindChild("Player");
		Player player = (playerTransform != null) ? playerTransform.GetComponent<Player>() : null;
		
		Tile tile = gameObject.GetComponent<Tile>();

		if (player != null) {
			Vector3 pos = player.transform.position;
			dropLocation = pos += new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), transform.position.z);
		}
		else {
			dropLocation = transform.position;
		}
		Reward rewardGo = (Reward)Instantiate(reward, dropLocation, Quaternion.identity);
		rewardGo.transform.parent = transform;
		
		SpriteRenderer rewardSr = rewardGo.GetComponent<SpriteRenderer>();
		Debug.Log(rewardSr.sortingOrder);
		rewardSr = Util.SetTopOrder(transform, rewardSr);
		Debug.Log(rewardSr.sortingOrder);

	}

	private void OpenDoor(Door door)
	{
		door.State = Door.DoorState.Unlocked;
	}

	private void LockDoors()
	{
		foreach (Reward reward in rewards)
		{
			if (reward.GetType() == typeof(Door))
			{
				((Door)reward).State = Door.DoorState.Locked;
			}
		}
	}

}
