using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MisionTrigger : MonoBehaviour
{
    [SerializeField] private OpenCloseDoors _doorsControll;
    private bool _isDone;

    public UnityEvent MissionComplete;
    
    private void Start()
    {
        _isDone = false;
    }

    IEnumerator OpenDoorDelay()
    {
        yield return new WaitForSeconds(2);
        MissionComplete?.Invoke();
        yield return new WaitForSeconds(10);
        _doorsControll.DoorOpenClose();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isDone) return;
        if (other.CompareTag("Player"))
        {
            _isDone=true;
            _doorsControll.DoorOpenClose();
            StartCoroutine(OpenDoorDelay());
        }
    }
}
