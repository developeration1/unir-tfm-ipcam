using UnityEngine;

public class QuitAppOnTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            Application.Quit();
    }
}
