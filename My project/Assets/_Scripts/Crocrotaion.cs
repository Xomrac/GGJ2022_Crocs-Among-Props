using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crocrotaion : MonoBehaviour {
	private RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
         if (Physics.Raycast(transform.position, -transform.up,out hit,.7f)) {
         	transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.Cross(transform.right, hit.normal)), Time.fixedDeltaTime);
         }
    }
}
