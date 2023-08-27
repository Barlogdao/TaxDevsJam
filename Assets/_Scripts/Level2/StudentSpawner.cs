using UnityEngine;

public class StudentSpawner : MonoBehaviour
{
    public GameObject studentPrefab; 
    public Transform spawnPoint; 
    [SerializeField] private float minRandomOffset = -2f; 
    [SerializeField] private float maxRandomOffset = 2f; 

    private void Start()
    {
        int score = 3; 
        SpawnStudents(score);
    }
    private void SpawnStudents(int count)
    {
        for (int i = 0; i < count; i++)
        {

            Vector3 randomOffset = new Vector3(
                Random.Range(minRandomOffset, maxRandomOffset),
                0f,
                Random.Range(minRandomOffset, maxRandomOffset)
            );

            GameObject newStudent = Instantiate(studentPrefab, spawnPoint.position + randomOffset, Quaternion.identity);

        }
    }
}