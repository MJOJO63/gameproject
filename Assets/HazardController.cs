using UnityEngine;

public class HazardController : MonoBehaviour
{
    public Rigidbody2D RB;
    public PlayerScript Player;

    public float Speed = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vel = new Vector2(0, 0);

        if (transform.position.x < Player.transform.position.x)

        { 
            vel.x = Speed; 
        }

        if (transform.position.x > Player.transform.position.x)
        {
            vel.x = -Speed;
        }

        if (transform.position.y < Player.transform.position.y)

        {
            vel.y = Speed;
        }

        if (transform.position.y > Player.transform.position.y)
        {
            vel.y = -Speed;
        }


        RB.linearVelocity = vel;    
    }
}
