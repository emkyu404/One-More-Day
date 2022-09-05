using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MakeObstacleTransparent : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private List<TransparencyEnvironement> currentObjectInTheWay = new List<TransparencyEnvironement>();
    [SerializeField] private List<TransparencyEnvironement> alreadyTransparent = new List<TransparencyEnvironement>();
    [SerializeField] private Transform target;
    private Transform camera;

    void Start()
    {
        camera = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        GetAllObjectsInTheWay();
        MakeObjectsSolid();
        MakeObjectsTransparent();
    }

    private void GetAllObjectsInTheWay()
    {
        currentObjectInTheWay.Clear();
        float camToPlayerDistance = Vector3.Magnitude(camera.position - target.position);
        Ray rayForward = new Ray(camera.position, target.position - camera.position);
        Ray rayBackward = new Ray(target.position, camera.position - target.position);

        var hitsForward = Physics.RaycastAll(rayForward, camToPlayerDistance);
        var hitsBackward = Physics.RaycastAll(rayBackward, camToPlayerDistance);

        foreach(var hit in hitsForward)
        {
            if(hit.collider.gameObject.TryGetComponent(out TransparencyEnvironement tp))
            {
                if (!currentObjectInTheWay.Contains(tp))
                {
                    currentObjectInTheWay.Add(tp);
                }
            }
        }

        foreach (var hit in hitsBackward)
        {
            if (hit.collider.gameObject.TryGetComponent(out TransparencyEnvironement tp))
            {
                if (!currentObjectInTheWay.Contains(tp))
                {
                    currentObjectInTheWay.Add(tp);
                }
            }
        }
    }

    private void MakeObjectsTransparent()
    {
        foreach (TransparencyEnvironement tp in currentObjectInTheWay.ToArray())
        {
            if (!alreadyTransparent.Contains(tp))
            {
                tp.MakeTransparent();
                alreadyTransparent.Add(tp);
            }
        }
    }

    private void MakeObjectsSolid()
    {
        foreach (TransparencyEnvironement tp in alreadyTransparent.ToArray())
        {
            if (!currentObjectInTheWay.Contains(tp))
            {
                tp.Restore();
                alreadyTransparent.Remove(tp);
            }
        }
    }
}
