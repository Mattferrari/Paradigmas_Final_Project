using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    // Método común para configurar al jugador que interactúa con el power-up
    public abstract void SetMario(PlayerController player);
}