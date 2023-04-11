using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    
    [SerializeField] Vector2 Velocity = new Vector2(1f,3f);
    
    [SerializeField] float _collisionFloat = 0.45f;

    
    Rigidbody2D _ballRigidbody;

    
    
    // Start is called before the first frame update
    void Start()
    {
        _ballRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
        _ballRigidbody.velocity = Velocity;
    }

    void OnHorizontalCollision(){
        Debug.Log("Choque Horizontal");
        Velocity = new Vector2(Velocity.x, Velocity.y * -1);
    }

    void OnVerticalCollision(){
        Debug.Log("Choque Vertical");
        Velocity = new Vector2(Velocity.x * -1, Velocity.y);
    }

    void OnBlockCollision(Collision2D element){
        //https://docs.unity3d.com/ScriptReference/ContactPoint2D.html
        Vector2 collision = element.GetContact(0).point;
        float xColPoint = collision.x - element.transform.position.x;
        float yColPoint = collision.y - element.transform.position.y;
        Debug.Log("Punto Colision x: " + xColPoint + " y col point: " + yColPoint);

        if(Mathf.Abs(yColPoint) > _collisionFloat){
            Velocity = new Vector2(Velocity.x, Velocity.y * -1);
        }
        else if (Mathf.Abs(xColPoint) > _collisionFloat){
            Velocity = new Vector2(Velocity.x * -1, Velocity.y);
        }


    }

    void OnCollisionEnter2D(Collision2D element){
        // Contra quién colisiona?
        // Pared
            // - Horizontal
            // - Vertical
        // Caja (bloques a destruir)
        // Paleta
        Debug.Log("Colisiona con: "+ element.gameObject.name);
        string collisionTag = element.gameObject.tag;
        if(collisionTag == Constants.HORIZONTAL_WALL){
            OnHorizontalCollision();
        }
        if(collisionTag == Constants.VERTICAL_WALL){
            OnVerticalCollision();
        }
        if (collisionTag == Constants.BLOCK){
            OnBlockCollision(element);
        }
        if (collisionTag == Constants.PADDLE){
            // Tarea -> agregar aqui el rebote contra el pad del jugador
        }
        

        
    }
}
