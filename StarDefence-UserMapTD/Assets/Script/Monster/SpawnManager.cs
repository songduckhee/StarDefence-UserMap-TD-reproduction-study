using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;


public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    public List<Monster> monsters = new List<Monster>();
    public SplineContainer curSpline;
    // Start is called before the first frame update
    void Start()
    {
        curSpline = Object.FindAnyObjectByType<SplineContainer>();
        foreach (Monster monster in monsters)
        {
            monster.SetSpline(curSpline);
        }
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
