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
				rb.velocity += velocity;
				if (movementDirection != Vector3.zero)
				{
					Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

					transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
				}
			}
			else
			{
				movementTimer = 0;
			}
		}

		private bool IsGrounded()
		{
			return Physics.Raycast(transform.position, Vector3.down,.7f);
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