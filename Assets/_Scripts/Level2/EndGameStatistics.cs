using System.Collections.Generic;
using UnityEngine;

public class EndGameStatistics : MonoBehaviour
{
    public int Collectibles { get; set; }
    public int CalculateEndScore()
    {
        List<Student> students = new List<Student>(FindObjectsOfType<Student>());

        int totalScore = 0;

        foreach (Student student in students)
        {
            int studentScore = student.Experience;

            if (studentScore == 100)
            {
                studentScore *= 2;
            }
            
            totalScore += studentScore + Collectibles;
        }

        return totalScore;
    }
}