using UnityEngine;

public class LastTrigger : MonoBehaviour
{
    [SerializeField] private Key goodEndingKey;
    [SerializeField] private Transform finalPivot;
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private Transform handPivot;
    [SerializeField] private EnemyPatrol enemy;
    [SerializeField] private int keyCountToFinish;
    [SerializeField] private string goodEndingMessage;
    [SerializeField] private string badEndingMessage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && PlayerManager.Instance.Keys.Count >= keyCountToFinish)
        {
            PlayerManager.Instance.CameraPivot = cameraPivot;
            PlayerManager.Instance.SetPositionToHand(handPivot);
            enemy.gameObject.SetActive(false);
            PlayerManager.Instance.EndingNote(PlayerManager.Instance.Keys.Contains(goodEndingKey) ? goodEndingMessage : badEndingMessage, finalPivot);
            gameObject.SetActive(false);
        }
    }
}
