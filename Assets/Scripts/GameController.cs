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
        endTime = Time.time - startTime;
        endText.text = "Congratulations! \n Your time was: " + Mathf.Round(endTime);
    }
}