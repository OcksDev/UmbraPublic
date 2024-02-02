using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnData : MonoBehaviour
{
    public string Type = "";
    public List<string> Data = new List<string>();
    public bool IsReal; // a boolean for the ages
    [NonSerialized]
    public Card card;

    private void Start()
    {
        if (Data.Count == 0) Data.Add(RandomFunctions.Instance.GenerateObjectID());

        Tags.DefineReference(gameObject, Data[0]);
    }

    private void OnDestroy()
    {
        Tags.ClearAllOf(Data[0]);
    }
}
