using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Vector3 targetPosition = new Vector3(0, 50, -10); // Posici�n final de la c�mara
    public float zoomDuration = 3f; // Tiempo total del zoom (en segundos)
    private Vector3 startPosition; // Posici�n inicial de la c�mara
    private float elapsedTime = 0f; // Tiempo transcurrido

    void Start()
    {
        // Guardar la posici�n inicial de la c�mara
        startPosition = transform.position;
    }

    void Update()
    {
        // Si el tiempo transcurrido es menor que la duraci�n del zoom, mover la c�mara
        if (elapsedTime < zoomDuration)
        {
            elapsedTime += Time.deltaTime;

            // Interpolar la posici�n de la c�mara desde la posici�n inicial a la posici�n objetivo
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / zoomDuration);
        }
    }
}

