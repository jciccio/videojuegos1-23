using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{

    [Header("Patrol Config")]
	[SerializeField] protected Vector3 PatrolArea;
	[SerializeField] protected float RestTimeInPoint;
	[SerializeField] protected Transform PatrolContainer;
   // [SerializeField] protected List<Transform> PatrolTransform;
    [SerializeField] protected List<Vector3> PatrolPoints;

    [Header("Stats")]
	[SerializeField] protected float Health = 100f;

	[SerializeField] protected float PatrolSpeed = 4f;
	[SerializeField] protected float ChaseSpeed = 4f;
	[SerializeField] public float KnockBackForce = 6f;
	[SerializeField] public float KnockbackTime = 0.10f;
	[SerializeField] public float AttackCooldown = 0.10f;


    // Internal
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected int  MovingTo;
    [SerializeField] protected float CurrentRestTime;
	[SerializeField] private FieldOfView _fov;

	[Header("Debug Zone - States")]
	[SerializeField] protected bool Dead = false;
	[SerializeField] protected bool Chasing = false;
	[SerializeField] protected bool IsPerformingAware = false;
	[SerializeField] protected bool IsInAttackCooldown = false;
	[SerializeField] protected bool IsKnockingBack = false;


    // Start is called before the first frame update
    void Start()
    {
        PatrolPoints = new List<Vector3>();
		_fov = GetComponent<FieldOfView>();

        foreach(Transform child in PatrolContainer){
			//Vector3 elPos = element.position;
            Debug.Log($"Elements: {child.name}");
			PatrolPoints.Add(new Vector3(child.position.x, 0, child.position.z));
		}
		agent = GetComponent<NavMeshAgent>();
    }

    protected void Patrol(){
		agent.speed = PatrolSpeed;
		Vector2 pp = new Vector2(transform.position.x, transform.position.z);
		Vector2 mt = new Vector2(PatrolPoints[MovingTo].x, PatrolPoints[MovingTo].z);
		if(Mathf.Abs(Vector3.Distance(pp,mt)) > 0.1f){
			//AnimatorLift.SetBool(Constants.ENEMY_RUNNING_BOOL, true);
			//float distance = Mathf.Max(Vector3.Distance(pp,mt), 0.75f);
			//float speed = Mathf.Min(distance * PatrolSpeed, PatrolSpeed);
			//float step = speed * Time.deltaTime; // calculate distance to move
			//transform.position = Vector3.MoveTowards(transform.position, PatrolPoints[MovingTo], step);
			agent.SetDestination(PatrolPoints[MovingTo]);
		}
		else{
			//AnimatorLift.SetBool(Constants.ENEMY_RUNNING_BOOL, false);
			if(CurrentRestTime < RestTimeInPoint){
				CheckElapsedTime();
			}
			else{
				MovingTo = (MovingTo + 1) % PatrolPoints.Count;
				CurrentRestTime = 0;
			}
		}
	}

    protected void CheckElapsedTime(){
		this.CurrentRestTime += Time.deltaTime;
	}

	protected void WatchAround(float dir){
		//float angle = Mathf.MoveTowardsAngle(CurrentRotation, CachedRotation+30, 2 );
	}

	protected void Aware(){
		
	}
	
	/*protected void Chase(){
		//agent.isStopped = false;
		if(agent.enabled){
			if(!Chasing){
				AnimatorLift.SetTrigger(Constants.ENEMY_CHASING_TRIGGER);
				IsPerformingAware = true;
				StartCoroutine(PerformAware());
			}
			Chasing = true;
			if(!IsPerformingAware){
				Vector3 direction = transform.position - EnemyView.PlayerRef.transform.position;
				direction.y = 0;
				agent.SetDestination(EnemyView.PlayerRef.transform.position+direction.normalized);
			}
		}	
	}*/

	protected bool RotateToPosition(){
		Vector3 targetDir =  (PatrolPoints[MovingTo] - transform.position).normalized;
		targetDir.y = 0;
		if(targetDir != Vector3.zero){
			Quaternion lookRotation = Quaternion.LookRotation(targetDir);
			//transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
		}
		return true;
	}

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Posicion del enemigo " + transform.position);
    }

	public virtual void Attack(){
	
	}

    void FixedUpdate(){
		if(!_fov.CanSeePlayer) // If can't see player can patrol aka go through all checkpoints
		{
			Patrol();
		}
		else{ // If can see player, should perform attack or chase or whatever we want.
			Attack();
		}

				
	}
}
