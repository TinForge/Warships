using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AlphaFader : MonoBehaviour
{

	public float lifetime;
	private TextMeshProUGUI tm;
	private float t;
	private Color c;

    void Start()
    {
		tm = GetComponent<TextMeshProUGUI>();
		c = tm.color;
    }

	void Update()
	{
		if (t <= lifetime) {
			t += Time.deltaTime;
			c.a = Mathf.Lerp(1, 0, t / lifetime);
			c.a *= c.a;
			tm.color = c;
		}
		else
			Destroy(this);


    }
}
