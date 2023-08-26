using UnityEngine;

public class CursorSpot : MonoBehaviour
{
    public LayerMask groundLayer; // Слой для определения пола
    public float heightOffset = 0.1f; // Смещение над полом

    private void Update()
    {
        // Получаем позицию мыши в экранных координатах
        Vector3 mouseScreenPosition = Input.mousePosition;

        // Проецируем позицию мыши на плоскость пола только если попали в объект с заданным слоем
        Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
        {
            // Получаем позицию точки на полу с учетом смещения
            Vector3 targetPosition = hit.point + Vector3.up * heightOffset;

            // Перемещаем объект на заданную позицию
            transform.position = targetPosition;
        }
    }
}