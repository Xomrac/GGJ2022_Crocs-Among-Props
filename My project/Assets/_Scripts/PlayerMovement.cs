using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using XInputDotNetPure;

namespace GGJ 
{
	public class PlayerMovement : MonoBehaviour {
		public AudioClip jump;
		public AudioClip move;
		public AudioClip bonk;
		[SerializeField] private Transform playerCamera;
		[SerializeField] private float rotationSpeed;
		[SerializeField] private float jumpSpeed;
		[SerializeField] private float maxSpeed;
		[SerializeField] private AnimationCurve speedCurve;
		[SerializeField] private int maxJumps;
		private Rigidbody rb;
		private bool isPausing;
		private float movementTimer;
		public float maxDistanceToRaycast;
		
		private int timesJumped;
		


		private void Start()
		{
			rb = GetComponent<Rigidbody>();
			playerCamera = Camera.main.transform;
			Cursor.lockState = CursorLockMode.Confined;
			Cursor.visible = false;
			timesJumped = 0;
		}

		private void FixedUpdate() 
		{
			float horizontalInput = Input.GetAxis("Horizontal");
			float verticalInput = Input.GetAxis("Vertical");
			var movementDirection = new Vector3(horizontalInput, 0, verticalInput);
			
			
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
			Debug.DrawRay(transform.position, -transform.up*maxDistanceToRaycast,Color.blue);
			return Physics.Raycast(transform.position, -transform.up,maxDistanceToRaycast);
		}

		private void Update() {
			if (!isPausing) {
				{
					if ((IsGrounded() && Input.GetButtonDown("Jump")) || (!IsGrounded() && timesJumped < maxJumps && Input.GetButtonDown("Jump"))) {
						TimerManager.Instance.source.PlayOneShot(jump, 0.3f);
						rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
						timesJumped++;
					}
				}
				if (IsGrounded()) {
					timesJumped = 0;
				}
			}
			if (Input.GetButtonDown("Cancel")&& !TimerManager.Instance.IsGameOver) 
			{
				isPausing = !UiManager.Instance.optionPanel.activeSelf;
				UiManager.Instance.optionPanel.SetActive(!UiManager.Instance.optionPanel.activeSelf);
				if (UiManager.Instance.optionPanel.activeSelf)
				{
					EventSystem.current.SetSelectedGameObject(UiManager.Instance.optionsFirstElement);
				}
				Time.timeScale = Mathf.Clamp(Convert.ToInt32(!UiManager.Instance.optionPanel.activeSelf), 0, 1f);
				Cursor.lockState = CursorLockMode.Confined;
				Cursor.visible = UiManager.Instance.optionPanel.activeSelf;
			}
		}

		private void OnCollisionEnter(Collision collision) {
			if (collision.gameObject.CompareTag("Throwable")) {
				Debug.Log(rb.velocity.magnitude);
				if (rb.velocity.magnitude>10) {
					Debug.Log("bonk");
					TimerManager.Instance.source.PlayOneShot(bonk,0.4f);
				}
			}
			
		}
		
		
	}

}

//  private void Update() {
//       dxmotor = findIfLFet(player.transform.forward, transform.position, player.transform.up);
//       FindMultiplier();
//    }
//
//    private void FindMultiplier() {
//       float temp;
//       temp = Vector3.Distance(player.transform.position, transform.position);
//       Debug.Log(temp);
//       if (temp<maxDistanceTreshold) {
//          multiplier = 0f;
//       }
//       if (temp<lessThanMaxDistanceTreshold) {
//          multiplier = 0.25f;
//       }
//       if (temp<mediumDistanceTreshold) {
//          multiplier = 0.5f;
//       }
//       if (temp<lessThanMediumDistanceTreshold) {
//          multiplier = 0.75f;
//       }
//       if (temp<minDistanceTreshold) {
//          multiplier = 1f;
//       }
//    }
//    private float findIfLFet(Vector3 fwd, Vector3 targetDir, Vector3 up) {
//       Vector3 perp = Vector3.Cross(fwd, targetDir);
//       float dir = Vector3.Dot(perp, up);
// 		
//       if (dir > 0f) {
//          return 1f;
//       } else if (dir < 0f) {
//          return -1f;
//       } else {
//          return 0f;
//       }
//    }
//    
//    IEnumerator vibra() {
//       Debug.Log("brrrr");
//       GamePad.SetVibration(playerIndex,-dxmotor*multiplier,dxmotor*multiplier);
//       yield return new WaitForSeconds(timeVibration);
//       GamePad.SetVibration(playerIndex,0,0);
//       coru = null;
//
//    }
//    private void Start() {
//       StartCoroutine(soundplay());
//       player = FindObjectOfType<GGJ.PlayerMovement>().gameObject;
//    }
//
//    private IEnumerator soundplay() {
//       yield return new WaitForSeconds( timeToReproduceSound);
//       while (true) {
//          GetComponent<AudioSource>().Play();
//          if (coru==null) {
//             Debug.Log("vibra");
//             coru = StartCoroutine(vibra());
//          }
//          yield return new WaitForSeconds( timeToReproduceSound);
//       }
//    }
//    private void OnTriggerEnter(Collider other) {
//       if (other.CompareTag("Player")) {
//          TimerManager.Instance.IsGameOver = true;
//          TimerManager.Instance.StopAllCoroutines();
//          TimerManager.Instance.OSTSource.Stop();
//          TimerManager.Instance.source.PlayOneShot(winSound);
//          UiManager.Instance.winText.text += $"{TimerManager.Instance.minutes:00}:{TimerManager.Instance.seconds:00}";
//          UiManager.Instance.WinPanel.gameObject.SetActive(true);
//          Cursor.visible = true;
//          Time.timeScale = 0f;
//       }
//    }
// }
