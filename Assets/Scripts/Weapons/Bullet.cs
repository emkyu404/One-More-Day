using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private ParticleSystem ImpactSystem;
    [SerializeField] private Light light;
    private Weapon weapon;

    

    private Rigidbody Rigidbody;

    private Vector3 prevPos;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Physics.IgnoreLayerCollision(0, 8);
    }

    private void Start()
    {
        Destroy(gameObject, 1.0f);
        prevPos = transform.position;
    }


    public void Shoot(Vector3 Position, Vector3 Direction, float Speed, Weapon wp)
    {
        weapon = wp;
        Rigidbody.velocity = Vector3.zero;
        transform.position = Position;
        transform.forward = Direction;
        Rigidbody.AddForce(Direction * Speed, ForceMode.VelocityChange);
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody.velocity = Vector3.zero;
        Rigidbody.detectCollisions = false;
        StartCoroutine(FadeLight());
    }

    IEnumerator FadeLight()
    {
        float t = 0;
        while (light.intensity > 0)
        {
            yield return null;
            t += Time.deltaTime / Time.time;
            light.intensity = Mathf.Lerp(light.intensity, 0, t);
        }
    }

    private void OnParticleSystemStopped()
    {
        Destroy(gameObject);
    }

    public float GetDamage()
    {
        float actualDamage = weapon.damage;
        float critDice = (float)Random.Range(0, 100) / 100f;
        if(critDice <= weapon.critRate)
        {
            actualDamage *= weapon.critDmg;
        }
        
        return actualDamage;
    }
}
