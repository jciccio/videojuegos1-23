using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LevelLoader : MonoBehaviour
{
    // Punto de inicio
    [SerializeField] Transform StartingPoint;

    // Prefab de las cajas
    [SerializeField] GameObject Block;
    // Referencia al contenedor de cajas
    [SerializeField] Transform BlocksContainer;
    // Movimiento en X y Y por cada elemento colocado
    [SerializeField] float xMovement;
    [SerializeField] float yMovement;



    // Start is called before the first frame update
    void Start()
    {
        LoadLevel("Assets/Levels/Level1.txt");   
    }

    void LoadLevel(string path){
        string data = LoadLevelFile(path);
        string [] line = data.Split("\n");
        Vector2 position = StartingPoint.position;
        int count = 1;
        for(int i = 0 ; i < line.Length; i++){ // Representa las filas
            for (int j = 0 ; j < line[i].Length; j++){ // Representa las columnas
                if(line[i][j] == 'X'){
                    // Instanciar el prefab
                    GameObject element = GameObject.Instantiate(Block);
                    // Colocarlo
                    //element.transform.position = position;
                    StartCoroutine(AnimateToPosition(element, position));
                    // Darle un nombre
                    element.name = "Block " + count;
                    // Asignarle el papá
                    element.transform.SetParent(BlocksContainer);
                    count++;
                }
                position.x +=xMovement;
            }
            position.y += yMovement;
            position.x = StartingPoint.position.x;
        }
    }

    IEnumerator AnimateToPosition(GameObject obj, Vector2 targetPosition){
        Transform objTransform = obj.transform;
        while(obj != null && Vector2.Distance(objTransform.position, targetPosition) > 0.1f){
            objTransform.position = Vector2.Lerp(objTransform.position, targetPosition, Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

    string LoadLevelFile(string path){
        string data = "";
        try{
            using (StreamReader sr = new StreamReader(path)){
                string line;
                while((line = sr.ReadLine()) != null){
                    data += line + "\n";
                }
            } 
        }
        catch(IOException e){
            Debug.LogError("File not found: " + e);
        }
        return data;
    }

}
