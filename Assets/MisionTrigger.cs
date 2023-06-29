using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MisionTrigger : MonoBehaviour
{
    [SerializeField] OpenCloseDoors doorsControll;

    public UnityEvent missionComplete;
    bool isDone;
    private void Start()
    {
        isDone = false;
    }

    IEnumerator OpenDoorDelay()
    {
        yield return new WaitForSeconds(2);
        missionComplete.Invoke();
        yield return new WaitForSeconds(10);
        doorsControll.DoorOpenClose();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDone) return;
        if (other.CompareTag("Player"))
        {
            isDone=true;
            doorsControll.DoorOpenClose();
            StartCoroutine(OpenDoorDelay());
            
        }
    }
}
