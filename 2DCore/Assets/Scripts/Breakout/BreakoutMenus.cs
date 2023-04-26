using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class BreakoutMenus : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI Points;

    void Start(){
        UpdateWinText();
    }
    public void LoadScene(string name){
        SceneManager.LoadScene(name);
    }

    public void LoadScene(int index){
        SceneManager.LoadScene(index);
    }

    public void UpdateWinText(){
        int pts = PlayerPrefs.GetInt("Highscore", 0);

        Points.text = $"Felicidades! Ha Ganado el juego.\n\n El puntaje máximo ha sido: {pts}";

    }
}
