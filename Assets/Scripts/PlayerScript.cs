using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerScript : MonoBehaviour
{
    //These are the player's Variables, the raw info that defines them
    
    //The Rigidbody2D is a component that gives the player physics, and is what we use to move
    public Rigidbody2D RB;
    public SpriteRenderer SR;
    public SpriteRenderer Monster;

    //TextMeshPro is a component that draws text on the screen.
    //We use this one to show our score.
    public TextMeshPro ScoreText;
    public TextMeshPro HealthText;
    public TextMeshPro ControlText;

    //This will control how fast the player moves
    public float Speed = 4;
    
    //This is how many points we currently have
    public int Score = 0;

    public int Health = 3;
    
    public Vector3 swapPosition;
    
    //Start automatically gets triggered once when the objects turns on/the game starts
    void Start()
    {
        //During setup we call UpdateScore to make sure our score text looks correct
        UpdateScore();
        UpdateHealth();
    }

    //Update is a lot like Start, but it automatically gets triggered once per frame
    //Most of an object's code will be called from Update--it controls things that happen in real time
    void Update()
    {
        //The code below controls the character's movement
        //First we make a variable that we'll use to record how we want to move
        Vector2 vel = new Vector2(0,0);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Speed = 10;
        }
        else
        {  
            Speed = 4;
        }
        //Then we use if statement to figure out what that variable should look like
        
        //If I hold the right arrow key, the player should move right. . .
        if (Input.GetKey(KeyCode.RightArrow))
        {
            vel.x = Speed;
        }
        //If I hold the left arrow, the player should move left. . .
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            vel.x = -Speed;
        }
        //If I hold the up arrow, the player should move up. . .
        if (Input.GetKey(KeyCode.UpArrow))
        {
            vel.y = Speed;
        }
        //If I hold the down arrow, the player should move down. . .
        if (Input.GetKey(KeyCode.DownArrow))
        {
            vel.y = -Speed;
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            swapPosition = RB.transform.position;
            RB.transform.position = SR.transform.position;
            SR.transform.position = swapPosition;
            
        }

        if (Score >= 1)
        {
            // Monster.transform.position += new Vector3(vel.x * Time.deltaTime, vel.y * Time.deltaTime, 0);
            Monster.transform.position =
                Vector3.MoveTowards(Monster.transform.position, transform.position, (Score/2) * Time.deltaTime);
        }


        //Finally, I take that variable and I feed it to the component in charge of movement
        RB.linearVelocity = vel;
    }

    //This gets called whenever you bump into another object, like a wall or coin.
    private void OnCollisionEnter2D(Collision2D other)
    {
        //This checks to see if the thing you bumped into had the Hazard tag
        //If it does...
        if (other.gameObject.CompareTag("Hazard"))
        {
            //Run your 'you lose' function!
            Health--;
            UpdateHealth();
            if (Health == 0)
            {
                Die();
            }
            
        }

        if (other.gameObject.CompareTag("SadVampire"))
        {
            Die();
        }



        if (other.gameObject.CompareTag("LevelUp"))
        {
            if(Score >= 10){
                WinGame();
            }
        }
        
        
        //This checks to see if the thing you bumped into has the CoinScript script on it
        CoinScript coin = other.gameObject.GetComponent<CoinScript>();
        //If it does, run the code block belows
        if (coin != null)
        {
            ControlText.text = "";
            //Tell the coin that you bumped into them so they can self destruct or whatever
            coin.GetBumped();
            //Make your score variable go up by one. . .
            Score++;
            //And then update the game's score text
            UpdateScore();
        }
        
        
    }

    //This function updates the game's score text to show how many points you have
    //Even if your 'score' variable goes up, if you don't update the text the player doesn't know
    public void UpdateScore()
    {
        ScoreText.text = "Score: " + Score;
        
    }
    
    public void UpdateHealth()
    {
        HealthText.text = "Health: " + Health;
    }

    //If this function is called, the player character dies. The game goes to a 'Game Over' screen.
    public void Die()
    {
        SceneManager.LoadScene("Game Over");
    }
    
    public void WinGame()
    {
        SceneManager.LoadScene("You Win");
    }
}
