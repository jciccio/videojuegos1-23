using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    
    [Header("Physics")]
    [SerializeField] Vector2 Velocity = new Vector2(1f,3f);
    [SerializeField] float _collisionFloat = 0.45f;
    [SerializeField] float XMultiplier = 1f;

    [Header("References")]
    [SerializeField] Pad Paddle;
    [SerializeField] private bool _playing = false; 
    


    Rigidbody2D _ballRigidbody;

    
    
    // Start is called before the first frame update
    void Start()
    {
        _ballRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        LockToPaddle();
        LaunchBall();
    }

    void LockToPaddle(){
        if(!_playing){
            Vector2 paddlePosition = Paddle.transform.position;
            transform.position = paddlePosition + new Vector2(0,1);   
        }
    }

    void LaunchBall(){
        if(!_playing && Input.GetMouseButtonDown(0)){
            _playing = true;
            _ballRigidbody.velocity = Vector2.up * 4; 
        }
    }

    void FixedUpdate(){
        if(_playing)
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

        AudioManager.instance.PlaySfx(Constants.SFX_BOX_BREAK);

        GameManager.instance.AddPoints();
    }

    void OnPaddleCollision(Collision2D other){
        float xCollisionPoint = other.contacts[0].point.x - other.transform.position.x;
        Velocity = new Vector2(xCollisionPoint * XMultiplier, Velocity.y * -1);
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
            OnPaddleCollision(element);
        }  
    }

    void OnPlayerLost(){
        GameManager.instance.UpdateLives(GameManager.instance.Lives - 1);
        _playing = false;
    }

    void OnTriggerEnter2D(Collider2D collider){
        Debug.Log(collider.tag);
        if(collider.tag == Constants.LOST){
            OnPlayerLost();
        }
    }


}
