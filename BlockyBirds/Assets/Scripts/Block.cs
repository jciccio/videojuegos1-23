using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private float Velocity = 1f;
    [SerializeField] private float VelocityChange = 0.25f;

    private float StartY;

    public static int Score = 0;
    

    
    //Start is called before the first frame update
    void Start()
    {
        StartY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * Time.deltaTime * Velocity;  

        // Si la posicion del bloque es < -10 => 21f
        if(transform.position.x < -10f){
            float yPos = StartY + UnityEngine.Random.Range(-1f,1f);

            transform.position = new Vector3(transform.position.x + 21f, yPos, transform.position.z);
            Velocity += VelocityChange;
            Score++;
        }
    }

}
