using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WallController : MonoBehaviour
{
    public GameObject player;
    public float wallspeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        WallMovement();
    }

    public void WallMovement()
    {
        var vector = new Vector2(transform.position.x, player.transform.position.y);
        transform.position = vector;
        transform.Translate(Vector2.right * wallspeed * Time.deltaTime);
        
    }
    
}
