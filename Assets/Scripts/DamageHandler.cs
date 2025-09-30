using System.Collections;
using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    public void ApplyDamage(Damage damage, Health health)
    {
        if (damage is DiscreteDamage)
        {
            damage.Apply(health);
            // Debug.Log("Discrete Damage");
        }
        else if (damage is ContinuousDamage continuous)
        {
            StartCoroutine(ApplyContinousDamage(continuous, health));
            // Debug.Log("Continuous Damage");
        }
        else
        {
            // Debug.Log("No Damage Match");
        }
    }

    public IEnumerator ApplyContinousDamage(ContinuousDamage damage, Health health)
    {
        yield return new WaitForSeconds(damage.StartDelay);

        float elapsed = 0.0f;

        WaitForSeconds wait = new WaitForSeconds(damage.Interval);
        while (elapsed < damage.Duration)
        {
            damage.Tick(health);

            yield return wait;
            elapsed += damage.Interval;
        }
    }
}
