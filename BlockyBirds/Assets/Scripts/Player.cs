using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    
    [SerializeField] Vector2 Jump = Vector2.up; // 0,1

    Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")){
           rb.velocity = Jump;
        }
    }

   

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag(Constants.DEATH_TAG)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
