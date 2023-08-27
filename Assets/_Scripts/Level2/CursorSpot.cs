using UnityEngine;

public class CursorSpot : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer; 
    [SerializeField] private float heightOffset = 0.1f;

    private void Update()
    {

        Vector3 mouseScreenPosition = Input.mousePosition;

        Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
        {
            Vector3 targetPosition = hit.point + Vector3.up * heightOffset;
            transform.position = targetPosition;
        }
    }
}