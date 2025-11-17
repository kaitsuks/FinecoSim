using UnityEngine;
using System.Collections;

[AddComponentMenu("Playground/Movement/Follow Target")]
[RequireComponent(typeof(Rigidbody2D))]
public class FollowTarget : Physics2DObject
{
	// This is the target the object is going to move towards
	[HideInInspector]
	public Transform target;
	HairDressers hairDressers;
	//public GameObject targetObject;
	Vector2 direction;
	bool isStopped;

	[Header("Movement")]
	// Speed used to move towards the target
	float speed = 0.2f;

	// Used to decide if the object will look at the target while pursuing it
	public bool lookAtTarget = false;

	// The direction that will face the target
	public Enums.Directions useSide = Enums.Directions.Up;

    private void Awake()
    {
		//target = new Transform()
		//target = targetObject.transform;
		hairDressers = GameObject.Find("Targets").GetComponent<HairDressers>();
		target = hairDressers.targets[Random.Range(0, 10)];
		//Debug.Log("TARGET = " + target);
		rigidbody2D = this.GetComponent<Rigidbody2D>();

	}

    // FixedUpdate is called once per frame
    void FixedUpdate ()
	{
		direction = target.position - transform.position;
		//do nothing if the target hasn't been assigned or it was detroyed for some reason
		if (target == null)
			return;
		//look towards the target
		if(lookAtTarget)
		{
			Utils.SetAxisTowards(useSide, transform, target.position - transform.position);
		}
		
		//Move towards the target
		if (!gameObject.GetComponent<Person>().isWandering && !gameObject.GetComponent<Person>().isStopped)
		{			
			rigidbody2D.AddForce(direction * speed);
			//rigidbody2D.MovePosition(Vector2.Lerp(transform.position, target.position, Time.fixedDeltaTime * speed));
		}
        if (!gameObject.GetComponent<Person>().isWandering && gameObject.GetComponent<Person>().isStopped)
        {
            rigidbody2D.AddForce(-direction * speed);
            rigidbody2D.velocity = Vector2.zero;
        }


    }
}
