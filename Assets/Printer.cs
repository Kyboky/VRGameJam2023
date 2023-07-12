
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Printer : MonoBehaviour
{
    [SerializeField] Material transmitionLight;
    [SerializeField] Material glowingButton;

    GameObject doc;
    //[SerializeField] GameObject documentToBePrinted;
    [SerializeField] List<GameObject> printerQueue;

    bool documentReady;

    public void AddDocumentToQueue(GameObject document)
    {
        printerQueue.Add(document);
        Debug.Log(printerQueue.Count);
    }

    public void PrintDocument()
    {
        if (!documentReady || printerQueue.Count == 0) return;
        documentReady = false;
        glowingButton.SetFloat("_Glow", 0);
        doc = Instantiate(printerQueue[0]);
        printerQueue.RemoveAt(0);
        doc.transform.SetParent(this.transform);
        doc.GetComponent<DocumentData>().OnDocumentPickup += PrinterReady;
        transmitionLight.SetFloat("_Transfer", 1);
        StartCoroutine(TransmitionFinished(doc));
    }

    IEnumerator TransmitionFinished(GameObject go)
    {
        yield return new WaitForSeconds(4);
        transmitionLight.SetFloat("_Transfer", 0);
        go.GetComponent<Animator>().enabled = false;

    }

    public void PrinterReady()
    {
        doc.GetComponent<DocumentData>().OnDocumentPickup -= PrinterReady;
        documentReady = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        documentReady = true;            
    }

    // Update is called once per frame
    void Update()
    {
        if(printerQueue.Count != 0 && documentReady)
        glowingButton.SetFloat("_Glow", 1);
    }
}
