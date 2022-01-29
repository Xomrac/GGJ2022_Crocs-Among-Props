
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour {
	// public  float velocity;
	// public float rotationVelocity;
	// public CharacterController rb;
	// KeyCode code;
	// public Vector3 aimDirection;
	//
	// // public Rigidbody controller;
	// public float speed;
	//
	public Transform camera;
	[SerializeField]
	private float rotationSpeed;

	[SerializeField]
	private float jumpSpeed;

	[SerializeField]
	private float jumpButtonGracePeriod;

	[SerializeField] private float gravity;

	private Animator animator;
	//public CharacterController characterController;
	private float ySpeed;
	private float originalStepOffset;
	private float? lastGroundedTime;
	private float? jumpButtonPressedTime;

	private bool isGrounded;
	public float maxSpeed;
	// private void FixedUpdate()
	// {
	// 	float horizontal = Input.GetAxis("Horizontal");
	// 	float vertical = Input.GetAxis("Vertical");
	// 	Vector3 direction = new Vector3(horizontal, 0, vertical);
	// 	if (direction.magnitude>=.1f)
	// 	{
	// 		float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + camera.eulerAngles.y;
	// 		transform.rotation=Quaternion.Euler(0f,targetAngle,0f);
	// 		// controller.velocity = direction * speed * Time.deltaTime;
	// 		controller.velocity = Vector3.RotateTowards(transform.forward, aimDirection, 0.1f, 0.0f) * speed * Time.deltaTime;
	// 	}
	// }

	private void Start() {

		isGrounded = true;
	}
	// private void FixedUpdate()
	// {
	// 	
	// 		if (Input.GetKey(KeyCode.W))
	// 		{
	// 			code = KeyCode.W;
	// 			rb.velocity = Vector3.RotateTowards(transform.forward, camera.forward, 0.1f, 0.0f) * velocity * Time.deltaTime;
	// 		}
	// 		if (Input.GetKey(KeyCode.S))
	// 		{
	// 			code = KeyCode.S;
	// 			rb.velocity = Vector3.RotateTowards(-transform.forward, camera.forward, 0.1f, 0.0f) * velocity/2 * Time.deltaTime;
	// 		}
	// 		
	// 		if (Input.GetKey(KeyCode.D))
	// 		{
	// 			code = KeyCode.D;
	// 			rb.velocity = Vector3.RotateTowards(transform.right, camera.forward, 0.1f, 0.0f) * velocity * Time.deltaTime;
	// 		}
	//
	// 		if (Input.GetKey(KeyCode.A))
	// 		{
	// 			code = KeyCode.A;
	// 			rb.velocity = Vector3.RotateTowards(-transform.right, camera.forward, 0.1f, 0.0f) * velocity * Time.deltaTime;
	// 		}
	//
	//
	// 		if (Input.GetKeyUp(code))
	// 		{
	// 			rb.velocity = Vector3.zero;
	// 		}
	// 		
	// 	
	// 	 	
	// 	 transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * Time.deltaTime * rotationVelocity, 0));
	// }

	private void FixedUpdate()
	{
		float horizontalInput = Input.GetAxis("Horizontal");
		float verticalInput = Input.GetAxis("Vertical");

		
		Vector3 movementDirection = new Vector3(horizontalInput,0, verticalInput);
		
		
		movementDirection = Quaternion.AngleAxis(camera.rotation.eulerAngles.y, Vector3.up) * movementDirection;
		movementDirection.Normalize();

		//ySpeed += Physics.gravity.y * Time.deltaTime;

		if (isGrounded && Input.GetKeyDown(KeyCode.Space)) {
			isGrounded = false;
			GetComponent<Rigidbody>().AddForce(Vector3.up*jumpSpeed,ForceMode.Impulse);
		}

		Vector3 velocity = movementDirection *maxSpeed*Time.fixedDeltaTime;
		//velocity.y = ySpeed;

		//characterController.Move(velocity * Time.deltaTime);
		GetComponent<Rigidbody>().velocity += velocity;
		if (movementDirection != Vector3.zero)
		{
			Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

			transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
		}        
	}

	private void Update() {
		Debug.DrawRay(transform.position, -transform.up);
		Debug.DrawRay(transform.position, transform.forward);
		if (Physics.Raycast(transform.position, Vector3.down)) {
			isGrounded = true;

		}
	}

}