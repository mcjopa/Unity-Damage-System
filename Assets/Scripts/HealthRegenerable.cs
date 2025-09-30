using System.Collections;
using UnityEngine;

public class HealthRegenerable : MonoBehaviour
{
    public Health Health;

    public float regenDelay = 5.0f;
    public float regenRate = 0.02f;
    public float regenAmountPerSecond = 5f;

    private Coroutine regenRoutine;

    void Start()
    {
        Health = GetComponent<Health>();
        Health.EntityDamaged += StartRegenRoutine;
    }

    private void StartRegenRoutine(object sender, HealthChangedEventArgs args)
    {
        if (regenRoutine != null)
            StopCoroutine(regenRoutine);
        regenRoutine = StartCoroutine(Regen());
    }

    private IEnumerator Regen()
    {
        yield return new WaitForSeconds(regenDelay);

        while (true)
        {
            yield return new WaitForSeconds(0);
            Health.Heal(regenAmountPerSecond * Time.deltaTime);
            // Debug.Log($"Health Regen: {Health.Cur}");
        }

    }
}
