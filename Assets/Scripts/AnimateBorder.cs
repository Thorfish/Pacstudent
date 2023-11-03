using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateBorder : MonoBehaviour
{
    [SerializeField]
    public List<Transform> stops;
    [SerializeField]
    public float speed;
    [SerializeField]
    public int pelletCount;
    [SerializeField]
    public GameObject pelletPrefab;
    [SerializeField]
    public GameObject canvas;


    private List<GameObject> pellets;
    private List<float> cumulativeDistance;
    private List<float> pelletPos;

    private float distanceSum;
    private Vector2 velocity;

    // Start is called before the first frame update
    void Start()
    {
        distanceSum = 0;
        cumulativeDistance = new List<float>(stops.Count);
        cumulativeDistance.Add(0);
        //Debug.Log("Cumu Distance: " + cumulativeDistance[0]);
        for (int i = 1; i < stops.Count; i++)
        {
            distanceSum += Vector3.Distance(stops[i].position, stops[i - 1].position);
            cumulativeDistance.Add(distanceSum);
            //Debug.Log("Cumu Distance: " + cumulativeDistance[i]);
        }
        distanceSum += Vector3.Distance(stops[stops.Count - 1].position, stops[0].position);
        //Debug.Log("Distance Sum: " + distanceSum);

        pellets = new List<GameObject>(pelletCount);
        pelletPos = new List<float>(pelletCount);

        for (int i = 0; i < pelletCount; i++)
        {
            pelletPos.Add((distanceSum / pelletCount) * i);
            pellets.Add(Instantiate(pelletPrefab));
            pellets[i].transform.SetParent(gameObject.transform, false);
            pellets[i].transform.position = setPosition(pelletPos[i]);

        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < pellets.Count; i++)
        {
            pelletPos[i] += speed;
            if (pelletPos[i] >= distanceSum)
            {
                pelletPos[i] -= distanceSum;
            }
            pellets[i].transform.position = setPosition(pelletPos[i]);
        }
    }

    Vector3 setPosition(float x)
    {
        int segment = 0, nextSegment = 0;
        //Find Segment
        for (int i = 0; i < stops.Count; i++)
        {
            if (x >= cumulativeDistance[i])
            {
                segment = i;
            }
            else
            {
                nextSegment = i;
                break;
            }
        }

        

        float percentProgress = (x - cumulativeDistance[segment]) / Vector3.Distance(stops[segment].position, stops[nextSegment].position);
        return Vector3.Lerp(stops[segment].position, stops[nextSegment].position, percentProgress);
    }
}
