using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iHealthChange
{
	void HealthChange(int current, int delta, float ratio);
}
