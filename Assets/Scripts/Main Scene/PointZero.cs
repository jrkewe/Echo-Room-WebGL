using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointZero : MonoBehaviour
{
    private GameObject floor;
    public Vector2 pointZero = Vector2.zero;
    public float frontBound;
    public float rightBound;

    // Update is called once per frame
    void Update()
    {
        if (floor == null)
        {
            floor = GameObject.FindWithTag("Floor");
        }

        if (floor != null)
        {
            float floorSizeX = floor.transform.localScale.x;
            float floorPositionX = floor.transform.position.x;
            float backBound = floorPositionX - (floorSizeX / 2) + 0.25f;
            frontBound = floorPositionX + (floorSizeX / 2) - 0.25f;

            float floorSizeZ = floor.transform.localScale.z;
            float floorPositionZ = floor.transform.position.z;
            float leftBound = floorPositionZ - (floorSizeZ / 2) + 0.25f;
            rightBound = floorPositionZ + (floorSizeZ / 2) - 0.25f;

            pointZero = new Vector2(backBound, leftBound);
        }
    }
}
