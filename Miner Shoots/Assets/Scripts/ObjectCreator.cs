using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectCreator : MonoBehaviour
{
    public GameObject objectToCreate;
    public Vector3 creationAreaCenter;
    public Vector3 creationAreaSize;
    public float creationDelay = 5f;
    public int objectsToCreate = 10;
    public TextMeshProUGUI countNumber;
    public GameObject CreateButton;
    private int createdObjectsCount = 0;

    public static int Count = 5;
    private void Start()
    {
        // Start the creation cycle
        StartCoroutine(CreateObjectCycle());
    }
    private void Update()
    {
        if (Count<=0)
        {
            CreateButton.SetActive(false);
        }
    }
    public void PushTheButton()
    {
        StartCoroutine(CreateObjectCycle());
        Count -= 1;
        countNumber.text = Count.ToString();
    }

    private IEnumerator CreateObjectCycle()
    {

        // Generate a random point within the creation area
        Vector3 randomPosition = new Vector3(
            Random.Range(creationAreaCenter.x - creationAreaSize.x / 2, creationAreaCenter.x + creationAreaSize.x / 2),
            Random.Range(creationAreaCenter.y - creationAreaSize.y / 2, creationAreaCenter.y + creationAreaSize.y / 2),
            Random.Range(creationAreaCenter.z - creationAreaSize.z / 2, creationAreaCenter.z + creationAreaSize.z / 2)
        );

        // Create the object at the random position
        GameObject newObject = Instantiate(objectToCreate, randomPosition, Quaternion.identity);

        // Start the countdown
        StartCoroutine(CountdownCoroutine(newObject));

        // Wait for the creation delay
        yield return new WaitForSeconds(creationDelay);

        // Increase the object count
        createdObjectsCount++;

        // Check if the required number of objects have been created
        if (createdObjectsCount >= objectsToCreate)
        {
            // Reset the object count and loop back to the beginning
            createdObjectsCount = 0;
        }
    }

    private IEnumerator CountdownCoroutine(GameObject obj)
    {
        // Wait for the countdown delay
        yield return new WaitForSeconds(creationDelay);

        // Destroy the object
        Destroy(obj);
    }
}
