using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemsInside : MonoBehaviour
{
    class ItemType
    {
        public GameObject item;
        public string type;
        public ItemType(GameObject go, string t)
        {
            item = go;
            type = t;
        }
    }
    
    List<ItemType> items = new List<ItemType>();
    [SerializeField] List<string> neededItems = new List<string>();

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("GasCylinder")){
            items.Add(new ItemType(other.gameObject, "GasCylinder"));
        }
        else if (other.CompareTag("Water"))
        {
            items.Add(new ItemType(other.gameObject, "Water"));
        }
        else if (other.CompareTag("Battery"))
        {
            items.Add(new ItemType(other.gameObject, "Battery"));
        }
        else if (other.CompareTag("Tools"))
        {
            items.Add(new ItemType(other.gameObject, "Tools"));
        }
        PrintObjectNumber();
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (ItemType item in items)
        {
            if (other.gameObject.GetInstanceID() == item.item.GetInstanceID())
            {
                items.Remove(item);
                break;
            }
        }
        PrintObjectNumber();
    }

    public void RemoveObjects()
    {
        for (int i = neededItems.Count - 1; i >= 0; i--)
        {
            foreach(ItemType item in items)
            {
                if (neededItems[i] == item.type)
                {
                    neededItems.RemoveAt(i);
                    Destroy(item.item);
                    items.Remove(item);
                    break;
                }
            }
        }
        if(neededItems.Count == 0)
        {
            Debug.Log("Mission Succesful");
        }
        else
        {
            Debug.Log("Missiion Failed");
        }
    }

    void PrintObjectNumber()
    {
        Debug.Log(items.Count);
    }

}
