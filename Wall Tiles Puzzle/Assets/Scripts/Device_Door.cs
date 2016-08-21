using UnityEngine;
using System.Collections;

public class Device_Door : Device
{
	private bool isDoorOpening = false;
	private bool isDoorOpen = false;
	private Vector3 doorOrigin;
	private Vector3 doorTarget;
	private GameObject door;
	private float doorSlideProgress = 0.0f;

	void Start()
	{
		door = transform.GetChild(0).gameObject;
		doorOrigin = door.transform.position;
		doorTarget = doorOrigin + door.transform.right;
	}

	void Update()
	{
		if (isDoorOpening && !isDoorOpen)
		{
			OpenDoor();
		}
	}

	private void OpenDoor()
	{
		doorSlideProgress += Time.deltaTime;

		if (doorSlideProgress > 1.0f)
		{
			doorSlideProgress = 1.0f;
			isDoorOpen = true;
		}

		door.transform.position = Vector3.Lerp(doorOrigin, doorTarget, doorSlideProgress);
	}

	public override void Trigger()
	{
		isDoorOpening = true;
	}

	public override bool GetIsTriggered()
	{
		return (isDoorOpening || isDoorOpen);
	}
}
