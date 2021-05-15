using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabCollection : MonoBehaviour
{
    public Prefabs[] prefabs;
    private GameObject prefab;
    
    void Start()
    {
        Generate();
    }
    
    public void Generate()
    {
        transform.DestroyChildren(true);
        var weights = new Dictionary<GameObject, int>();
        foreach (var _prefabCollection in prefabs)
        {
            weights.Add(_prefabCollection.prefab, _prefabCollection.weight);
        }
        GameObject selected = WeightedRandomizer.From(weights).TakeOne(); 
        GameObject PrefabCollec = (GameObject) Instantiate(selected, transform.position, transform.rotation);
        PrefabCollec.transform.parent = this.transform;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawWireCube(transform.position, new Vector3(1, 1, 1));
    }
}
