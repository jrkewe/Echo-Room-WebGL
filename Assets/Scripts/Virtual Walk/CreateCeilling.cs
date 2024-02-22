using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCeilling : MonoBehaviour
{
    private GameObject floor;
    private GameObject wall;

    // Start is called before the first frame update
    void Start()
    {
        floor = GameObject.FindGameObjectWithTag("Floor");
        wall = GameObject.FindGameObjectWithTag("WallX");

        transform.position = new Vector3(floor.transform.position.x, wall.transform.localScale.y + 0.08f, floor.transform.position.z);
        transform.localScale = floor.transform.localScale;

    }

}
