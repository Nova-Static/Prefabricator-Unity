using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PrefabCollection : MonoBehaviour
{
    public Prefabs[] prefabs;
    private GameObject prefab;
    Dictionary<GameObject, int> weights = new Dictionary<GameObject, int>();
    private bool instantiated = false;
    private GameObject childPrefab;
    private List<Dictionary<GameObject, int>> childWeights = new List<Dictionary<GameObject, int>>();
    void Start()
    {
        Generate();
    }
    
    public void Generate()
    {
        transform.DestroyChildren(true);
        foreach (var _prefabCollection in prefabs)
        {
            if (!weights.ContainsKey(_prefabCollection.prefab))
                weights.Add(_prefabCollection.prefab, _prefabCollection.weight);
        }
        
        GameObject selected = WeightedRandomizer.From(weights).TakeOne();
        foreach (var _prefabCollection in prefabs)
        {
            if (_prefabCollection.prefab.name.Equals(selected.name))
            {
                if (_prefabCollection.childPrefabs == null) break;

                foreach (var _childPrefabs in _prefabCollection.childPrefabs)
                {
                    Dictionary<GameObject, int> childWeights = new Dictionary<GameObject, int>();
                    foreach (var _childPrefabCollection in _childPrefabs.childPrefabCollection)
                    {
                        if (!childWeights.ContainsKey(_childPrefabCollection.prefab))
                            childWeights.Add(_childPrefabCollection.prefab, _childPrefabCollection.weight);
                    }
                    this.childWeights.Add(childWeights);
                }
            }
        }
        GameObject PrefabCollec = (GameObject) Instantiate(selected, transform.position, transform.rotation, transform);
        
        foreach (var _childPrefabCollection in childWeights)
        {
            GameObject selectedChild = WeightedRandomizer.From(_childPrefabCollection).TakeOne();
            GameObject PrefabCollecChild = (GameObject) Instantiate(selectedChild, transform.position, transform.rotation, PrefabCollec.transform);
        }
        childWeights.Clear();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawWireCube(transform.position, new Vector3(1, 1, 1));
    }
}
