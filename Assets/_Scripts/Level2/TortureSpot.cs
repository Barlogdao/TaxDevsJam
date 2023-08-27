using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TortureSpot : MonoBehaviour
{
    private bool occupied = false;

    public bool IsOccupied()
    {
        return occupied;
    }

    public void SetOccupied(bool status)
    {
        occupied = status;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Student"))
        {
            occupied = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Student"))
        {
            occupied = false;
        }
    }
}
