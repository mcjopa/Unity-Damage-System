using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(StatusManager), typeof(Health))]
    public class Enemy : MonoBehaviour, IStatusEffectable
    {
        Health _health;
        DamageHandler _damageHandler;

        [SerializeField]
        private StatusManager _statusManager;

        [SerializeField]
        bool ApplyFire = true,
             ApplyPoison = true;

        List<SimpleStatusApplicator> effects;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

            _health = GetComponent<Health>();
            _damageHandler = GetComponent<DamageHandler>();

            _health.EntityHealed += LogHealthUpdate;
            _health.EntityDamaged += LogHealthUpdate;

            _health.EntityDied += () => Debug.Log("Entity Is Dead");
            _health.EntityDied += Kill;

            StartCoroutine(ApplyRandomEffect());
            _health.EntityFullHealed += () => StartCoroutine(ApplyRandomEffect());

            // simple logic for showcase
            effects = new List<SimpleStatusApplicator>();
            if (ApplyFire)
            {
                effects.Add(new SimpleStatusApplicator(StatusEffect.StatusType.Burn, 1000));
            }
            if (ApplyPoison)
            {
                effects.Add(new SimpleStatusApplicator(StatusEffect.StatusType.Poison, 1000));
            }
        }

        IEnumerator ApplyRandomEffect()
        {
            yield return new WaitForSeconds(1.0f);
            ApplyStatus(effects[UnityEngine.Random.Range(0, effects.Count)]);
        }

        private void LogHealthUpdate(object sender, HealthChangedEventArgs args)
        {
            // Debug.Log($"Health Changed: {args.Prev} {args.Cur}");
        }

        void DamageEnemy()
        {
            Damage damage = new DiscreteDamage(10);
            _damageHandler.ApplyDamage(damage, _health);
        }


        public void ApplyStatus(IStatusApplicator status)
        {
            _statusManager.ApplyStatus(status);
        }

        void Kill()
        {
            Destroy(gameObject);
        }
    }
}
