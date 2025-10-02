using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class StatusEffect
    {
        protected float ElapsedTime = 0.0f; // seconds
        protected float Duration = 10.0f; // seconds
        protected float StatusBuildupThreshold = 100.0f;
        protected float BuildUp = 0;
        protected float DecayRate = 10.0f; // seconds
        private float _lastTick;

        protected ContinuousDamage Damage;

        private ParticleSystem _particleEffect;
        private Health _health;

        protected StatusType Name;
        protected EventType Stage = EventType.Inactive;

        public enum StatusType
        {
            Burn,
            Poison,
        }

        public enum EventType
        {
            Inactive,
            Start,
            Update,
            Exit,
        }

        public StatusEffect(ParticleSystem particleEffect, Health health)
        {
            _particleEffect = particleEffect;
            _health = health;
        }

        public void Apply(float amount)
        {
            BuildUp += amount;
            Debug.Log(amount + "amount");
            if (BuildUp > StatusBuildupThreshold)
            {
                Stage = EventType.Start;
            }
            Debug.Log(BuildUp);
        }

        public void Process()
        {
            if (Stage == EventType.Inactive) Decay();
            if (Stage == EventType.Start) Enter();
            if (Stage == EventType.Update) Update();
            if (Stage == EventType.Exit) Exit();
        }

        private void Decay()
        {
            BuildUp = Mathf.Clamp(BuildUp, 0, BuildUp - DecayRate * Time.deltaTime);
        }

        public virtual void Enter()
        {
            _particleEffect.Play();
            Stage = EventType.Update;
        }

        public virtual void Update()
        {
            Stage = EventType.Update;
            ElapsedTime += Time.deltaTime;

            if (ElapsedTime > Duration)
            {
                Stage = EventType.Exit;
                return;
            }

            // Damage Tick each every (Interval) seconds
            if (Time.time - _lastTick > Damage.Interval)
            {
                _lastTick = Time.time;
                Damage.Tick(_health);
            }
        }

        public virtual void Exit()
        {
            
            Stage = EventType.Inactive;
            BuildUp = 0f;
            ElapsedTime = 0f;
            _particleEffect.Stop();
        }

    }

    // public class StatusDictionary
    // {
    //     private Dictionary<StatusEffect.StatusType, float> lookup = new Dictionary<StatusEffect.StatusType, float>();
    //     public Dictionary<StatusEffect.StatusType, float> Lookup { get; }

    //     public StatusDictionary()
    //     {
    //         // set new status values here
    //         lookup[StatusEffect.StatusType.Burn] = 0;
    //     }
    // }

    public class Burn : StatusEffect
    {
        // how do we instantiate this with game object?
        // Status manager per game object instance?
        float baseDamage = 15.0f;

        public Burn(ParticleSystem particleEffect, Health health) : base(particleEffect, health)
        {
            Name = StatusType.Burn;
            Duration = 10.0f;
        }

        public override void Enter()
        {
            base.Enter();
            Damage = new FireDamage(baseDamage, 0, 1.0f, Duration);
        }


    }
    public class Poison : StatusEffect
    {
        // how do we instantiate this with game object?
        // Status manager per game object instance?
        float damagePerTick = 1.0f;

        public Poison(ParticleSystem particleEffect, Health health) : base(particleEffect, health)
        {
            Name = StatusType.Poison;
            Duration = 10.0f;
        }

        public override void Enter()
        {
            base.Enter();
            Damage = new PoisonDamage(damagePerTick, 0, 0.02f, Duration);
        }


    }
}





// status can apply effect
// status applies Continuous damage?


// bullet has damage + status affliciton buildup
// when a bullet hits, increase the current enities status amount for that
// particular affliction
// if the status amount > statusThreshold
// apply the status
