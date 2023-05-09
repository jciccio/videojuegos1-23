using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] public TextMeshProUGUI LivesText;
    [SerializeField] public TextMeshProUGUI PointsText;
    [SerializeField] public TextMeshProUGUI HighScoreText;
    [SerializeField] public Transform BlocksContainer;
    [SerializeField] public LevelLoader LevelManager;

    [SerializeField] private int _currentLevel = 1;
    
    public int Lives {set; get;}

    public int Points {set; get;}
    public int Highscore {set; get;}
    
    public static GameManager instance = null;


    void Awake(){
        if(instance == null){
            instance = this;
        }
        else if(instance != this){
            Destroy(gameObject);
        }
    }
    

    
    // Start is called before the first frame update
    void Start()
    {
       Lives = 1;
       LivesText.text = $"Lives: {Lives}";
       PointsText.text = "Points: 0";
       HighScoreText.text = $"HS: {PlayerPrefs.GetInt("Highscore", 0)}";
       
    }

    void Update(){
        if(BlocksContainer.childCount == 0){
            // Gano :)
            Debug.Log("Ganó :)");
            _currentLevel++;
            bool loaded = LevelManager.LoadLevel($"Assets/Levels/Level{_currentLevel}.txt");
            if(!loaded){
                // Cambiar de escena y finalizar el juego
                SceneManager.LoadScene("BreakoutWon");
            }

        }
        if(Lives <= 0){
            // Perdió :(
            Debug.Log("Perdió :(");
            Points = 0;
            PointsText.text = $"Points: {Points}";
            SceneManager.LoadScene("BreakoutWon");
        }
    }

    public void UpdateLives(int lives){
        Lives = lives;
        LivesText.text = $"Lives: {lives}";
    }

    public void AddPoints(int pts = 1){
        Points += pts;
        PointsText.text = $"Points: {Points}";

        if(Points > Highscore){
            HighScoreText.text =  $"HS: {Points}";
            PlayerPrefs.SetInt("Highscore", Points);
        }
    }

  
}
