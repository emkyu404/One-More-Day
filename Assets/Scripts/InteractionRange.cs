using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionRange : MonoBehaviour
{

    public List<InteractableObject> inRangeObjects = new List<InteractableObject>();
    public void AddInteractable(InteractableObject iobject)
    {
        inRangeObjects.Add(iobject);
    }

    public void RemoveInteractable(InteractableObject iobject)
    {
        inRangeObjects.Remove(iobject);
    }

    public void Interact(PlayerController pc)
    {
        foreach(InteractableObject io in inRangeObjects)
        {
            io.InteractAction(pc);
        }
    }
}
