using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [Header("Configs")]
    [SerializeField] public float Radius;
    [SerializeField] [Range(0,360)] public float Angle;

    [Header("References")]
    [SerializeField] public GameObject PlayerRef;
    [SerializeField] public LayerMask TargetMask;
    [SerializeField] public LayerMask ObstructionMask;

    [Header("- - - Debug Zone - - -")]
    [SerializeField] public bool CanSeePlayer = false;
    [SerializeField] public float CurrentViewAngle;

    private float Delay = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
	    StartCoroutine(FOVRoutine());
        CurrentViewAngle = Angle;
    }

    private IEnumerator FOVRoutine(){
	    WaitForSeconds wait = new WaitForSeconds(Delay);
        while(true){
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck(){
        Collider [] rangeChecks = Physics.OverlapSphere(transform.position, Radius, TargetMask);
        if(rangeChecks.Length > 0){
            Transform target = rangeChecks[0].transform; // Solo hay un jugador, pero si hay varios jugadores, hay que cambiar esto
            Vector3 directionToTarget = (target.position - transform.position).normalized; 
            if(Vector3.Angle(transform.forward, directionToTarget) < CurrentViewAngle/2){ // ver si el angulo es < que el angulo que ve la persona / 2 (media a la der e izq)
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if(!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, ObstructionMask)){
                    // Raycast desde el enemigo hacia el jugador. Viendo hacia el jugador con distancia y excluyendo obstaculos
                    CanSeePlayer = true;
                    PlayerRef = rangeChecks[0].gameObject;
                    CurrentViewAngle = 360f;
                }
                else{
                   LosePlayerSight();
                }
            }
            else{
               LosePlayerSight();
            }
        }
        else if (CanSeePlayer){
          LosePlayerSight();
        }
    }

    private void LosePlayerSight(){
        CanSeePlayer = false;
        PlayerRef = null;
        CurrentViewAngle = Angle;
    }
}