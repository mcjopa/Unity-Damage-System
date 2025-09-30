using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts
{
    [RequireComponent(typeof(Health))]
    public class StatusManager : MonoBehaviour
    {
        private Dictionary<StatusEffect.StatusType, StatusEffect> Effects = new Dictionary<StatusEffect.StatusType, StatusEffect>();
        private Health _health;

        // the particle effects will be a child of this, so that
        // it is easy to add to the prefabs who want it.
        [SerializeField]
        private GameObject _particleEffectsParentGameObject;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _health = GetComponent<Health>();
            Effects[StatusEffect.StatusType.Burn] = new Burn(_particleEffectsParentGameObject.transform.Find("Burn").GetComponent<ParticleSystem>(), _health);
            Effects[StatusEffect.StatusType.Poison] = new Poison(_particleEffectsParentGameObject.transform.Find("Poison").GetComponent<ParticleSystem>(), _health);
        }

        // Update is called once per frame
        void Update()
        {
            foreach (var kvp in Effects)
            {
                StatusEffect status = kvp.Value;
                status.Process();
            }
        }

        public bool ApplyStatus(IStatusApplicator applicator)
        {
            StatusEffect status = Effects[applicator.StatusType];
            status.Apply(applicator.Amount);

            return true;
        }
    }
}
