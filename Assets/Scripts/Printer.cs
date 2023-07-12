
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Printer : MonoBehaviour
{
    [SerializeField] private Material _transmitionLight;
    [SerializeField] private Material _glowingButton;
    [SerializeField] private List<GameObject> _printerQueue;

    private GameObject _document;
    private bool _documentReady;

    public void AddDocumentToQueue(GameObject document)
    {
        _printerQueue.Add(document);
    }

    public void PrintDocument()
    {
        if (!_documentReady || _printerQueue.Count == 0) return;
        _documentReady = false;
        _glowingButton.SetFloat("_Glow", 0);
        _document = Instantiate(_printerQueue[0]);
        _printerQueue.RemoveAt(0);
        _document.transform.SetParent(this.transform);
        _document.GetComponent<DocumentData>().OnDocumentPickup += PrinterReady;
        _transmitionLight.SetFloat("_Transfer", 1);
        StartCoroutine(TransmitionFinished(_document));
    }

    IEnumerator TransmitionFinished(GameObject go)
    {
        yield return new WaitForSeconds(4);
        _transmitionLight.SetFloat("_Transfer", 0);
        go.GetComponent<Animator>().enabled = false;

    }

    public void PrinterReady()
    {
        _document.GetComponent<DocumentData>().OnDocumentPickup -= PrinterReady;
        _documentReady = true;
    }

    void Start()
    {
        _documentReady = true;            
    }

    void Update()
    {
        if(_printerQueue.Count != 0 && _documentReady)
        _glowingButton.SetFloat("_Glow", 1);
    }
}
