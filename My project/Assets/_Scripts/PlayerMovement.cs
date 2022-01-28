
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	public  float velocity;
	public float rotationVelocity;
	Rigidbody rb;
	KeyCode code;
	Vector3 aimDirection;
	
        
	
	private void Start()
	{
		
		rb = GetComponent<Rigidbody>();
		
		//win = FindObjectOfType<TMP_Text>();
	}
	private void Update()
	{
		
			if (Input.GetKey(KeyCode.W))
			{
				code = KeyCode.W;
				rb.velocity = Vector3.RotateTowards(transform.forward, aimDirection, 0.1f, 0.0f) * velocity * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.S))
			{
				code = KeyCode.S;
				rb.velocity = Vector3.RotateTowards(-transform.forward, aimDirection, 0.1f, 0.0f) * velocity/2 * Time.deltaTime;
			}


			if (Input.GetKey(KeyCode.D))
			{
				code = KeyCode.D;
				rb.velocity = Vector3.RotateTowards(transform.right, aimDirection, 0.1f, 0.0f) * velocity * Time.deltaTime;
			}

			if (Input.GetKey(KeyCode.A))
			{
				code = KeyCode.A;
				rb.velocity = Vector3.RotateTowards(-transform.right, aimDirection, 0.1f, 0.0f) * velocity * Time.deltaTime;
			}

                
			if (Input.GetKeyUp(code)) { rb.velocity = Vector3.zero; }
			
		
		 	
		 transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * Time.deltaTime * rotationVelocity, 0));
	}
	

}