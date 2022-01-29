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
		private RaycastHit hit;
		


		private void Start()
		{
			rb = GetComponent<Rigidbody>();
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
					// if (Physics.Raycast(transform.position, -transform.up,out hit,.7f)) {
					// 	transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.Cross(transform.right, hit.normal)), Time.fixedDeltaTime);
					// }
				}
			}
			else
			{
				movementTimer = 0;
			}
		}

		private bool IsGrounded()
		{
			return Physics.Raycast(transform.position, -transform.up,.7f);
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