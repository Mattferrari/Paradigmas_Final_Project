using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    // M�todo com�n para configurar al jugador que interact�a con el power-up
    public abstract void SetMario(PlayerController player);
}