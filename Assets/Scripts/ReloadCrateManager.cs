using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadCrateManager : InteractableObject
{
    [SerializeField] private float cooldown;
    [SerializeField] GameObject UIHint;
    private bool OnCoolDown = false;

    private void Start()
    {
        StartCoroutine(Cooldown());
    }
    public override void InteractAction(PlayerController pc)
    {
        if (!OnCoolDown)
        {
            pc.gameObject.GetComponent<Inventory>().RefillWeaponAmmo();
            StartCoroutine(Cooldown());
        }
        else
        {
            Debug.Log("On Cooldown ! ");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InteractionRange ir = other.gameObject.GetComponent<InteractionRange>();
            ir.AddInteractable(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InteractionRange ir = other.gameObject.GetComponent<InteractionRange>();
            ir.RemoveInteractable(this);
        }
    }

    private IEnumerator Cooldown()
    {
        OnCoolDown = true;
        Debug.Log("Start cooldown");
        UIHint.SetActive(false);
        yield return new WaitForSeconds(cooldown);
        OnCoolDown = false;
        Debug.Log("End cooldown");
        UIHint.SetActive(true);
    }
}
