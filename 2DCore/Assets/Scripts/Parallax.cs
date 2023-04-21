using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
   
    [SerializeField] private float Velocity;
    SpriteRenderer Sprite;
    float XSize;

    void Start(){
        float size = Camera.main.orthographicSize;
        XSize = size * 16/9 * 2;
        Sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * Time.deltaTime * Velocity;
        if(transform.position.x < -(18)){
            transform.position = new Vector2(18, 0);
            //Sprite.bounds.size.x
        }
    }
}
