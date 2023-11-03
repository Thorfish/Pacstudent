using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    [SerializeField]
    private GameObject cherryPrefab;
    private float leftMax = -10.5f, rightMax = 20.5f, speed = 0.01f;

    void Start()
    {
        InvokeRepeating("Repeat", 10, 10);
    }

    void Repeat()
    {
        StartCoroutine(SpawnCherry());
    }

    IEnumerator SpawnCherry()
    {
        var cherry = Instantiate(cherryPrefab) as GameObject;
        float randomx = Random.Range(-2, 2);
        float randomy = Random.Range(-0.5f, 0.5f);

        if (randomx > 0)
        {
            cherry.transform.position = new Vector3(rightMax + randomx, -9.5f + randomy, 0);
            while (cherry.transform.position.x > leftMax)
            {
                cherry.transform.position = new Vector2(cherry.transform.position.x - speed, cherry.transform.position.y);
                yield return null;
            }
        }
        else
        {
            cherryPrefab.transform.position = new Vector3(leftMax + randomx, -9.5f + randomy, 0);
            while (cherry.transform.position.x < rightMax)
            {
                cherry.transform.position = new Vector2(cherry.transform.position.x + speed, cherry.transform.position.y);
                yield return null;
            }
        }

        Destroy(cherry);
    }
}
