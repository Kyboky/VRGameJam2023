using System.Collections.Generic;
using UnityEngine;

public class ItemsInside : MonoBehaviour
{
    class ItemType
    {
        public GameObject Item;
        public string Type;
        public ItemType(GameObject go, string t)
        {
            Item = go;
            Type = t;
        }
    }
    [SerializeField] private Printer _printer;
    [SerializeField] private GameObject _finishedObj;
    [SerializeField] private GameObject _nextMission;
    [SerializeField] private List<string> _neededItems = new List<string>();

    private List<ItemType> _items = new List<ItemType>();

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("GasCylinder")){
            _items.Add(new ItemType(other.gameObject, "GasCylinder"));
        }
        else if (other.CompareTag("Water"))
        {
            _items.Add(new ItemType(other.gameObject, "Water"));
        }
        else if (other.CompareTag("Battery"))
        {
            _items.Add(new ItemType(other.gameObject, "Battery"));
        }
        else if (other.CompareTag("Tools"))
        {
            _items.Add(new ItemType(other.gameObject, "Tools"));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (ItemType item in _items)
        {
            if (other.gameObject.GetInstanceID() == item.Item.GetInstanceID())
            {
                _items.Remove(item);
                break;
            }
        }
    }
    public void RemoveObjects()
    {
        for (int i = _neededItems.Count - 1; i >= 0; i--)
        {
            foreach(ItemType item in _items)
            {
                if (_neededItems[i] == item.Type)
                {
                    _neededItems.RemoveAt(i);
                    Destroy(item.Item);
                    _items.Remove(item);
                    break;
                }
            }
        }
        GameObject go = Instantiate(_finishedObj);
        if (_neededItems.Count == 0)
        {
            go.GetComponent<DocumentData>().ChangeData(_neededItems);
        }
        else
        {
            go.GetComponent<DocumentData>().ChangeData(_neededItems);
        }
        _printer.AddDocumentToQueue(go);
        if (_nextMission)
        {
            _printer.AddDocumentToQueue(_nextMission);
        }
        _printer.PrintDocument();
    }

}
