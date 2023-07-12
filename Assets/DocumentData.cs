using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DocumentData : MonoBehaviour
{
    [SerializeField] TMP_Text _text;


    public delegate void DocumentPickup();
    public event DocumentPickup OnDocumentPickup;

    public void ChangeData(List<string> items)
    {
        if(items.Count == 0)
        {
            _text.text = "Missing items:\nNothing is missing";
        }
        else
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            string missingItems = "Missing items:\n";
            foreach (string item in items)
            {
                try
                {
                    dict[item]++;
                }
                catch
                {
                    dict[item] = 1;
                }
            }
            foreach (string item in dict.Keys)
            {
                missingItems += string.Format("- {0} x {1}\n", dict[item], item);
            }
            Debug.Log(missingItems);
            _text.text = missingItems;
        }
    }
    public void PickedUp()
    {
        OnDocumentPickup.Invoke();
    }
}
