using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(0)]
public class AIManager : MonoBehaviour
{
    private static AIManager _instance;

    public static AIManager Instance
    {
        get
        {
            return _instance;
        }

        private set
        {
            _instance = value;
        }
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            return;
        }

        Destroy(gameObject);
    }

    public Transform target;
    public float RadiusAroundTarget = 0.5f;
    public List<AIUnit> Units = new List<AIUnit>();

    private void Update()
    {
        MakeAgentsCircleTarget();
    }

    private void MakeAgentsCircleTarget()
    {
        for(int i = 0;   i < Units.Count; ++i)
        {
            Units[i].MoveTo(new Vector3(
                target.position.x + RadiusAroundTarget * Mathf.Cos(2 * Mathf.PI * i) / Units.Count,
                target.position.y,
                target.position.z + RadiusAroundTarget * Mathf.Sin(2 * Mathf.PI * i) / Units.Count));
        }
    }

}
