using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;

    private List<Transform> _segments = new List<Transform>(); //list of tail segments

    public Transform segmentPrefab; //segment game object that is a prefab in unity

    public int initialSize = 4;

    private void Start()
    {
        ResetState();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            _direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            _direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _direction = Vector2.right;
        }
    }

    private void FixedUpdate() //moving our snake
    {
        for(int i = _segments.Count - 1; i > 0; i--) //must iterate in reverse order so that each segment follows the next one until the head
        {
            _segments[i].position = _segments[i - 1].position; //assigning each segment position to the position of the one in front of it
        }

        this.transform.position = new Vector3(                     //transform is changing the position of the snake sprite on screen
            Mathf.Round(this.transform.position.x) + _direction.x, //x movement, mathf.round for rounding to whole number to keep snake on grid
            Mathf.Round(this.transform.position.y) + _direction.y, //y movement
            0.0f //z movement set to 0, since 2D
        );
    }

    private void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab); //this is duplicating our prefab and labeling it segment
        segment.position = _segments[_segments.Count - 1].position; //setting position for this duplicate to be at the end of the list

        _segments.Add(segment); //this is add the segment to the head and the segment list
    }

    private void ResetState()
    {
        for(int i = 1; i < _segments.Count; i++ ) //starting at 1 instead of 0, because 0 is main head
        {
            Destroy(_segments[i].gameObject);
        }

        _segments.Clear();
        _segments.Add(this.transform);

        for (int i = 1; i < initialSize; i++)
        {
            _segments.Add(Instantiate(this.segmentPrefab));
        }

        this.transform.position = Vector3.zero; //resetting snake back to start
    }

    private void OnTriggerEnter2D(Collider2D other) //function that detects collisions
    {
        if(other.tag == "Food")
        {
            Grow();
        }

        else if (other.tag == "Obstacle")
        {
            ResetState();
        }

    }
}

