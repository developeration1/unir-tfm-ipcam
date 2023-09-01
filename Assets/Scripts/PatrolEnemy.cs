using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] Transform[] patrolPoints;
    [SerializeField] float speed;
    [SerializeField] float detectionRange = 10f; // Rango de detección

    [SerializeField] LayerMask lyMsk;

    [SerializeField] Transform targetObject; // Objeto específico para detectar y detenerse
    private bool isDetectingObject = false; // Indicador de si se detecta el objeto

    private int targetPoint = 0; // Índice del punto de patrulla actual
    private Animator animator; // Referencia al componente Animator

    void Start()
    {
        animator = GetComponent<Animator>(); // Obtiene el componente Animator
    }

    void Update()
    {
        if (patrolPoints.Length == 0)
        {
            Debug.LogError("No hay puntos de patrulla asignados.");
            return;
        }

        if (isDetectingObject)
        {
            // Detenerse y deshabilitar el Animator
            animator.enabled = false;
            print("matar");
            PlayerManager.Instance.Die();
            return;
        }
        else
        {
            animator.enabled = true;
        }

        // Calcula la distancia al siguiente waypoint
        float distanceToWaypoint = Vector3.Distance(transform.position, patrolPoints[targetPoint].position);

        if (distanceToWaypoint <= 0.1f)
        {
            // Cambiar al siguiente waypoint
            targetPoint = (targetPoint + 1) % patrolPoints.Length;
        }

        // Si hay un objeto a detectar y está dentro del rango de detección
        if (targetObject != null && Vector3.Distance(transform.position, targetObject.position) <= detectionRange)
        {
            if (CanSeeTarget(targetObject.position) && Mathf.Abs(transform.position.y - targetObject.position.y) < 2f)
            {
                isDetectingObject = true;
                // Detener el movimiento
                speed = 0f;

                // Calcular la dirección hacia el objetivo
                Vector3 directionToTarget = targetObject.position - transform.position;
                directionToTarget.y = 0;

                // Rotar para mirar al objetivo
                Quaternion newRotation = Quaternion.LookRotation(directionToTarget);
                transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 5f);
                return;
            }
        }
        // Si no se detecta el objeto, seguir patrullando hacia el waypoint actual
        Vector3 directionToWaypoint = patrolPoints[targetPoint].position - transform.position;
        directionToWaypoint.y = 0;
        Quaternion newRot = Quaternion.LookRotation(directionToWaypoint);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRot, Time.deltaTime * 5f);

        // Mover al enemigo hacia el waypoint actual
        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[targetPoint].position, speed * Time.deltaTime);
    }

    bool CanSeeTarget(Vector3 targetPosition)
    {
        Vector3 directionToTarget = targetPosition - transform.position;
        directionToTarget.y = 0;
        float distanceToTarget = directionToTarget.magnitude;

        // Define las capas con las que el rayo interactuará
        // LayerMask raycastLayerMask = LayerMask.GetMask(lyMsk); // Solo colisiona con la capa "Obstacles"
        LayerMask raycastLayerMask = lyMsk;

        // Lanza un rayo hacia el objeto y verifica si hay obstáculos en el camino
        if (Physics.Raycast(transform.position, directionToTarget, out RaycastHit hit, distanceToTarget, raycastLayerMask))
        {
            // Dibuja el rayo en la escena (color rojo para obstáculos)
            Debug.DrawRay(transform.position, directionToTarget, Color.red);
            return false; // Si el rayo impacta en un obstáculo, no hay línea de visión
        }

        // Dibuja el rayo en la escena (color verde para línea de visión)
        Debug.DrawRay(transform.position, directionToTarget, Color.green);
        return true; // No hay obstáculos
    }

}
