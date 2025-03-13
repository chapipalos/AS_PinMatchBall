using UnityEngine;
using UnityEngine.Rendering;

public class Beat : MonoBehaviour
{

    public float tiempoExpansion = 2f; // Tiempo para expandirse
    public float tiempoContraccion = 1f; // Tiempo para contraerse
    public Vector3 escalaInicial;  // Tamaño normal
    public Vector3 escalaFinal; // Doble de tamaño
    public bool activarExpansion = false; // Controla si debe expandirse o no

    private float tiempoInicio;

    void Update()
    {
        if (activarExpansion)
        {
            float cicloTotal = tiempoExpansion + tiempoContraccion;
            float tiempoCiclo = (Time.time - tiempoInicio) % cicloTotal; // Control del tiempo

            float factor;

            if (tiempoCiclo < tiempoExpansion)
            {
                // Expandir
                factor = tiempoCiclo / tiempoExpansion;
            }
            else
            {
                // Contraer
                factor = 1 - ((tiempoCiclo - tiempoExpansion) / tiempoContraccion);
            }

            transform.localScale = Vector3.Lerp(escalaInicial, escalaFinal, factor);

            // Si el ciclo ha terminado, desactivar el bool
            if (tiempoCiclo >= cicloTotal - Time.deltaTime)
            {
                activarExpansion = false;
            }
        }
    }

    // Método para activar la expansión
    public void IniciarExpansion()
    {
        if (!activarExpansion)
        {
            activarExpansion = true;
            tiempoInicio = Time.time; // Reinicia el tiempo de inicio
        }
    }
}
