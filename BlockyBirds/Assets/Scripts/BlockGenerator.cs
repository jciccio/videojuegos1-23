using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{

    [SerializeField] GameObject Block;

    // Start is called before the first frame update
    void Start()
    {

        for(int i = 0 ; i < 10; i++){
            GameObject newElement = GameObject.Instantiate(Block);
            newElement.name = i+"Mi Bloque Instanciado";
            newElement.transform.position = new Vector3(i,i,0);
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
