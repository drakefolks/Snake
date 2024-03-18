using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class food : MonoBehaviour
{
    public BoxCollider2D gridArea;

    private void Start()
    {
        RandomizedPosition();
    }

    private void RandomizedPosition()
    {
        Bounds bounds = this.gridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f); //making random position for food
    }

    private void OnTriggerEnter2D(Collider2D other) //will get called automatically when a game object collides with another
    {
        if (other.tag == "Player") //our snake is tagged in unity as Player, so this makes sure that the snake collides with the food
        {
            RandomizedPosition();
        }
    }
}
