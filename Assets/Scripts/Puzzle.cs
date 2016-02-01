using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//all triggers active
//kinds of triggers

[ExecuteInEditMode]
public class Puzzle : MonoBehaviour {

    private Platform[] platforms;
	public List<GameObject> rewards;
	private bool rewarded = false;

    // Use this for initialization
    [ExecuteInEditMode]
    void Awake()
    {
        UpdateAllPlatforms();
    }

	void Update()
	{
		if (AllActive() && !rewarded)
		{
			rewarded = true;
			DropRewards();
		}
	}

    //auto seek all Platform objects in 
    void UpdateAllPlatforms()
    {
		List<Platform> platformsList = new List<Platform>();
        foreach (Transform t in transform)
        {
            Platform platform = t.GetComponent<Platform>();
            if (platform != null)
            {
                platformsList.Add(platform);
				platform.id = platformsList.Count;
            }
        }
        platforms = platformsList.ToArray();
    }

	bool AllActive()
	{
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
		foreach (GameObject reward in rewards){
			DropEachReward(reward);
		}
	}

	private void DropEachReward(GameObject reward)
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
