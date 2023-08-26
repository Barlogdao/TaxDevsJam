using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    private GameObject selectedObject;
    private bool isRotating = false;
    private bool isFloating = false;

    private Camera mainCamera;
    private Plane dragPlane;

    private Vector3 initialPosition; // Initial position before floating
    private float floatingHeight = 5f; // Floating height

    private void Start()
    {
        mainCamera = Camera.main;
        dragPlane = new Plane(Vector3.up, Vector3.zero);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (selectedObject == null)
            {
                RaycastHit hit = CastRay();

                if (hit.collider != null && hit.collider.CompareTag("Student"))
                {
                    selectedObject = hit.collider.gameObject;
                    initialPosition = selectedObject.transform.position; // Store initial position before floating
                    Cursor.visible = false;
                }
            }
            else
            {
                TeleportSelectedObject();
                Cursor.visible = true;
            }
        }

        if (selectedObject != null)
        {
            MoveSelectedObject();
            RotateSelectedObject();
        }
    }

    private void TeleportSelectedObject()
    {
        Vector3 position = MouseToWorldPosition(selectedObject.transform.position.y);
        selectedObject.transform.position = new Vector3(position.x, 0f, position.z);
        selectedObject = null;
        isFloating = false;
    }

    private void MoveSelectedObject()
    {
        if (!isFloating && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(FloatingCoroutine());
        }

        if (isFloating)
        {
            Vector3 position = MouseToWorldPosition(floatingHeight);
            selectedObject.transform.position = new Vector3(position.x, floatingHeight, position.z);
        }
    }

    private void RotateSelectedObject()
    {
        if (Input.GetMouseButtonDown(1) && !isRotating)
        {
            StartCoroutine(RotateCoroutine());
        }
    }

    private IEnumerator RotateCoroutine()
    {
        isRotating = true;
        float startRotationY = selectedObject.transform.rotation.eulerAngles.y;
        float targetRotationY = startRotationY + 90f;
        float elapsedTime = 0f;
        float rotationDuration = 0.3f;

        while (elapsedTime < rotationDuration)
        {
            float rotationY = Mathf.Lerp(startRotationY, targetRotationY, elapsedTime / rotationDuration);
            selectedObject.transform.rotation = Quaternion.Euler(new Vector3(0f, rotationY, 0f));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        selectedObject.transform.rotation = Quaternion.Euler(new Vector3(0f, targetRotationY, 0f));
        isRotating = false;
    }

    private IEnumerator FloatingCoroutine()
    {
        isFloating = true;
        float elapsedTime = 0f;
        float floatDuration = 0.5f;

        Vector3 startPosition = selectedObject.transform.position;
        Vector3 targetPosition = startPosition + Vector3.up * floatingHeight;

        while (elapsedTime < floatDuration)
        {
            selectedObject.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / floatDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        selectedObject.transform.position = targetPosition;
    }
    

    private RaycastHit CastRay()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);

        return hit;
    }

    private Vector3 MouseToWorldPosition(float yPos)
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        float distance;
        dragPlane.Raycast(ray, out distance);
        return ray.GetPoint(distance) + Vector3.up * yPos;
    }
}
