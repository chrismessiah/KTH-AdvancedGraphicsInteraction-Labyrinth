using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour 
{
    public static GameController instance;       //A reference to our game control script so we can access it statically.
	public GameObject player;
	private MazeGenerator mazeGenerator;
    float startTime;
    float endTime;
    public bool mapEnded = false;
    public Text endText;
    public Text scoreText;
    Ranking ranking {
        get { return scoreText.GetComponent<Ranking>();}
    }


    void Awake()
    {
        //If we don't currently have a game control...
        if (instance == null)
            //...set this one to be it...
            instance = this;
        //...otherwise...
        else if(instance != this)
            //...destroy this one because it is a duplicate.
            Destroy (gameObject);

        ranking.readRank();        
    }

    void Update()
    {
		// Reset on space key
        if (Input.GetButton("ENTER_BUTTON")) 
        {
            //...reload the current scene.
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void onStartMap(){
        
        startTime = Time.time;
    }

    public void onFinishMap(){
        if(!mapEnded){
            mapEnded = true;
            endTime = Time.time - startTime;
            endText.text = "Congratulations!\nYou made it in\n" + Mathf.Round(endTime) + " seconds";

            ranking.saveRank(endTime);
            ranking.showHighscore();
        }
    }
    
}