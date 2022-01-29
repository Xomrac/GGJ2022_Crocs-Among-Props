using System;
using UnityEngine;
namespace GGJ 
{
	public class PlayerMovement : MonoBehaviour {
		[SerializeField] private Transform playerCamera;
		[SerializeField] private float rotationSpeed;
		[SerializeField] private float jumpSpeed;
		[SerializeField] private float maxSpeed;
		[SerializeField] private AnimationCurve speedCurve;
		private Rigidbody rb;
		private float movementTimer;
		private bool isGrounded;
		public float maxDistanceToRaycast;
		


		private void Start()
		{
			rb = GetComponent<Rigidbody>();
			playerCamera = Camera.main.transform;
			Cursor.lockState = CursorLockMode.Confined;
			Cursor.visible = false;
		}

		private void FixedUpdate() 
		{
			float horizontalInput = Input.GetAxis("Horizontal");
			float verticalInput = Input.GetAxis("Vertical");
			var movementDirection = new Vector3(horizontalInput, 0, verticalInput);
			if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
			{
				rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
			}
			if (movementDirection != Vector3.zero)
			{
				movementTimer += Time.fixedDeltaTime;
				movementDirection = Quaternion.AngleAxis(playerCamera.rotation.eulerAngles.y, Vector3.up) * movementDirection;
				movementDirection.Normalize();
				GetComponent<Animator>().SetBool("isRunning",true);
				Vector3 velocity = movementDirection * (speedCurve.Evaluate(movementTimer)*maxSpeed)* Time.fixedDeltaTime;
				Vector3 rbVelocity = rb.velocity;
				rbVelocity += velocity;
				rb.velocity = rbVelocity;
				//Mathf.Clamp(rb.velocity.x, 0, maxSpeed);
				//Mathf.Clamp(rb.velocity.z, 0, maxSpeed);
				if (rb.velocity.magnitude > maxSpeed) {
					rb.velocity = rb.velocity.normalized * maxSpeed;
				}
				
				if (movementDirection != Vector3.zero)
				{
					Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

					transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
				}
			}
			else
			{
				movementTimer = 0;
				GetComponent<Animator>().SetBool("isRunning",false);
			}
		}

		private bool IsGrounded()
		{
			return Physics.Raycast(transform.position, -transform.up,maxDistanceToRaycast);
		}

		private void Update() {
			
			if (Input.GetKeyDown(KeyCode.Escape)) {
				UiManager.Instance.optionPanel.SetActive(!UiManager.Instance.optionPanel.activeSelf);
				Time.timeScale = Mathf.Clamp(Convert.ToInt32(!UiManager.Instance.optionPanel.activeSelf), 0, 1f);
				Cursor.lockState = CursorLockMode.Confined;
				Cursor.visible = UiManager.Instance.optionPanel.activeSelf;
			}
		}
	}

}