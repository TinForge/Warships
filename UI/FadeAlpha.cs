using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FadeAlpha : MonoBehaviour, iShipDisable
{

	public bool onAwake;
	public float lifetime;
	private TextMeshProUGUI tm;
	private CanvasGroup cg;
	private float t;
	private Color c;

    void Start()
	{
		cg = GetComponent<CanvasGroup>();
		tm = GetComponent<TextMeshProUGUI>();
		if(tm!=null)
			c = tm.color;
    }

	void Update()
	{
		if (!onAwake)
			return;
		if (t <= lifetime) {
			t += Time.deltaTime;
			if (cg != null) {
				cg.alpha = Mathf.Lerp(1, 0, t / lifetime);
			}
			if (tm != null) {
				c.a = Mathf.Lerp(1, 0, t / lifetime);
				c.a *= c.a;
				tm.color = c;
			}
		}
		else {
		//	if (GetComponent<Lifetime>() != null)
		//		Destroy(this);
		//	else
				Destroy(gameObject);
		}
    }
	public void Activate()
	{
		onAwake = true;
	}

	public void Disable()
	{
		onAwake = true;
	}

}
