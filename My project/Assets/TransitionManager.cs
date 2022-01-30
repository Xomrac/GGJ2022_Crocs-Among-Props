using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
	public static TransitionManager instance;

	public Animator animator;
	private void Awake()
	{
		instance = this;
	}
	// Start is called before the first frame update
    void Start()
    {
	    animator = GetComponent<Animator>();
    }
}
