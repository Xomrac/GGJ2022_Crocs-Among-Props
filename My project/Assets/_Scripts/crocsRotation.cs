using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crocsRotation : MonoBehaviour
{
	private void FixedUpdate() {
		RaycastHit hit;
		if (Physics.Raycast(transform.position, -transform.up, out hit, 5))
		{
			Debug.DrawRay(transform.position, -transform.up * 5, Color.black);
			//transform.up = hit.normal;
			transform.rotation=Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.Cross(transform.right, hit.normal)), Time.fixedDeltaTime);
		}	}

	
}
