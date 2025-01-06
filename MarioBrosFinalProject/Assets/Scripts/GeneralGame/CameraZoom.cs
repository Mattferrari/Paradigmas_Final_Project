using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Vector3 targetPosition = new Vector3(0, 50, -10); // Posición final de la cámara
    public float zoomDuration = 3f; // Tiempo total del zoom (en segundos)
    private Vector3 startPosition; // Posición inicial de la cámara
    private float elapsedTime = 0f; // Tiempo transcurrido

    void Start()
    {
        // Guardar la posición inicial de la cámara
        startPosition = transform.position;
    }

    void Update()
    {
        // Si el tiempo transcurrido es menor que la duración del zoom, mover la cámara
        if (elapsedTime < zoomDuration)
        {
            elapsedTime += Time.deltaTime;

            // Interpolar la posición de la cámara desde la posición inicial a la posición objetivo
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / zoomDuration);
        }
    }
}

