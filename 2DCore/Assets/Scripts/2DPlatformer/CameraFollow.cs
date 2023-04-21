using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [Header("Target")]
    [SerializeField] GameObject FollowObject;
    private Rigidbody2D _followObjectRB;

     [Header("Main Properties")]
    [SerializeField] Vector2 OffsetSize;
    [SerializeField] float Speed = 5f;
    [SerializeField] Vector3 OffsetPosition;

    private Vector2 _threshold;

    // Start is called before the first frame update
    void Start()
    {
        _followObjectRB = FollowObject.GetComponent<Rigidbody2D>();
        _threshold = CalculateThreshold();
    }

    Vector3 CalculateThreshold(){
        // Tam del umbral
        // Tam de la camara / pantalla
        Rect aspect = Camera.main.pixelRect;
        Debug.Log($"Aspect: {aspect.width} {aspect.height}");

        float cameraSize = Camera.main.orthographicSize;

        Vector2 threshold = new Vector2(cameraSize * aspect.width/ aspect.height, cameraSize);
        threshold -= OffsetSize;
        return threshold;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 follow = FollowObject.transform.position - OffsetPosition;

        // (1,0)
        float xDifference = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * follow.x);// A B => A-B
        float yDifference = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * follow.y);// A B => A-B

        Vector3 newPosition = transform.position;
        if(Mathf.Abs(xDifference) >= _threshold.x){
            newPosition.x = follow.x;
        }
        if(Mathf.Abs(yDifference) >= _threshold.y){
            newPosition.y = follow.y;
        }

        float cameraMoveSpeed = _followObjectRB.velocity.magnitude > Speed ? _followObjectRB.velocity.magnitude : Speed;
        transform.position = Vector3.MoveTowards(transform.position,newPosition, Time.deltaTime*cameraMoveSpeed);
    }

    void OnDrawGizmos(){
        Gizmos.color = Color.green;
        Vector2 rect = CalculateThreshold();
        Gizmos.DrawWireCube(transform.position + OffsetPosition, new Vector3(rect.x*2, rect.y*2,1));
    }
}
