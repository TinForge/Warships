using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectLifetime : MonoBehaviour {

	new private Light light;
	new private AudioSource audio;
	private ParticleSystem particle;

	private bool lightEnded;
	private bool audioEnded;
	private bool particleEnded;

	void Awake()
	{
		light = GetComponent<Light>();
		audio = GetComponent<AudioSource>();
		particle = GetComponent<ParticleSystem>();
	}

	void OnEnable ()
	{
		lightEnded = false;
		audioEnded = false;
		particleEnded = false;
		StartCoroutine(Lifetime());
	}

	private IEnumerator Lifetime()
	{
		if (light != null)
			StartCoroutine(FlashLight(0, 3, 0.1f));
		else
			lightEnded = true;

		if (audio != null)
			StartCoroutine(PlayAudio());
		else
			audioEnded = true;

		if (particle != null)
			StartCoroutine(PlayParticle());
		else
			particleEnded = true;

		while (!lightEnded || !audioEnded || !particleEnded)
			yield return null;

		gameObject.SetActive(false);

	}

	private IEnumerator FlashLight(float start, float end, float rate)
	{
		float t;
		t = 0;
		while (t <= 1) {
			light.intensity = Mathf.Lerp(start, end, t * t);
			t += Time.deltaTime * (1 / rate);
			yield return null;
		}
		t = 0;
		while (t <= 1) {
			light.intensity = Mathf.Lerp(end, start, t * t);
			t += Time.deltaTime * (1 / rate);
			yield return null;
		}
		lightEnded = true;
	}

	private IEnumerator PlayAudio()
	{
		audio.Play();
		while (audio.isPlaying)
			yield return null;
		audioEnded = true;
	}

	private IEnumerator PlayParticle()
	{
		particle.Play();
		while (particle.isPlaying)
			yield return null;
		particleEnded = true;
	}

}
