using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
  
    [SerializeField] ParticleSystem Particles;
    SpriteRenderer Sprite;
    BoxCollider2D BoxCollider;

    void Awake(){
        Sprite = GetComponent<SpriteRenderer>();
        BoxCollider = GetComponent<BoxCollider2D>();
        //Particles = GetComponentInChildren<ParticleSystem>();
    }

    void OnCollisionEnter2D(Collision2D other){
        //Destroy(this.gameObject);
        StartCoroutine(DeleteObject());
    }

    IEnumerator DeleteObject(){
        Sprite.enabled = false;
        BoxCollider.enabled = false;
        Particles.Play();
        yield return new WaitForSeconds(Particles.main.startLifetime.constantMax);
        Destroy(this.gameObject);
    }
}
