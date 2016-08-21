using UnityEngine;
using System.Collections;

public abstract class Triggerable : MonoBehaviour
{
	public virtual void Trigger() { }
	public virtual bool GetIsTriggered() { return false; }
}
