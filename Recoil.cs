using System.Collections;
using UnityEngine;

	//Simulate recoil effects by adjusting transform rotations on hull
	public class Recoil : MonoBehaviour
	{
		//Values
		[SerializeField] private float driveRecoilAmount = 8;
		[SerializeField] private float driveRecoilRate = 1;
		[SerializeField, Space] private float shootRecoilAmount = 8;
		[SerializeField] private float shootRecoilRateTo = 5;
		[SerializeField] private float shootRecoilRateFrom = 3;
		[SerializeField, Space] private float hitRecoilAmount = 20;
		[SerializeField] private float hitRecoilRateTo = 5;
		[SerializeField] private float hitRecoilRateFrom = 2;

		//private
		private float lerpRate = 0.5f;
		Quaternion driveRecoil = Quaternion.identity;
		Quaternion shootRecoil = Quaternion.identity;
		Quaternion hitRecoil = Quaternion.identity;

		//---

		private void Start()
		{
		//	gun_control.OnShoot += Shoot;
		//	hull_traverse.OnHullTraverse += Drive;
			//health.OnHitDirection += Hit;
		}

		private void OnDisable()
		{
			//gun_control.OnShoot -= Shoot;
			//hull_traverse.OnHullTraverse -= Drive;
			//health.OnHitDirection -= Hit;
		}

		//---

		private void Update()
		{
			//hull.localRotation = Quaternion.Lerp(driveRecoil, shootRecoil, lerpRate);
			//hull.localRotation = Quaternion.Lerp(hull.localRotation, hitRecoil, lerpRate);
		}

		//---

		public void Drive(float inputVertical, float inputHorizontal)
		{
			driveRecoil = Quaternion.Euler(inputVertical * (Mathf.PingPong(Time.time * driveRecoilRate, driveRecoilAmount) - driveRecoilAmount), 0, inputHorizontal * (Mathf.PingPong(Time.time * driveRecoilRate, driveRecoilAmount) - driveRecoilAmount));
		}

		//---
	}
