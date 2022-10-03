using System;
using System.Collections.Generic;
using UnityEngine;

public class EntityModel
{
    public event Action<float, float> OnHealthChanged;
    public event Action OnDied;

    public bool IsAlive => Health > 0;

    public float Health { get; private set; }
    public float Armor { get; private set; }
    public float Lifesteal { get; private set; }
    public float Attack { get; private set; }

    public float MaxHealth { get; private set; }

    //TODO Add the possibility of buffs during the game
    public EntityModel(IEnumerable<Stat> stats)
    {
        //Default stats
        foreach (var stat in stats)
        {
            AddStat(stat);
        }
    }
    public EntityModel(IEnumerable<Stat> stats, IEnumerable<Buff> buffs) : this(stats)
    {
        //Buff stats
        foreach (var buff in buffs)
        {
            foreach (var stat in buff.stats)
            {
                AddStat(stat);
            }
        }
    }

    //TODO Should not have separated stats and buff stats, should have combined them with abstraction
    private void AddStat(Stat stat)
    {
        AddStat(stat.id, stat.value);
    }
    private void AddStat(BuffStat buffStat)
    {
        AddStat(buffStat.statId, buffStat.value);
    }

    private void AddStat(int id, float value)
    {
        switch (id)
        {
            case StatsId.LIFE_ID:
                MaxHealth += value;
                Health = MaxHealth;
                break;
            case StatsId.ARMOR_ID:
                Armor += value;
                break;
            case StatsId.LIFE_STEAL_ID:
                Lifesteal += value;
                break;
            case StatsId.DAMAGE_ID:
                Attack += value;
                break;
        }
    }

    public void TakeDamage(float value)
    {
        var damage =(1 - Mathf.Clamp01(Armor / 100)) * Mathf.Abs(value);
        if (damage != 0f)
        {
            var targetHealth = Health - damage;
            ChangeHealth(targetHealth);
        }
    }

    public float DealDamage()
    {
        var lifesteal = Mathf.Clamp01(Lifesteal / 100) * Mathf.Abs(Attack);
        if (lifesteal != 0f)
        {
            var targetHealth = Health + lifesteal;
            ChangeHealth(targetHealth);
        }

        return Attack;
    }

    private void ChangeHealth(float targetValue)
    {
        Health = Mathf.Clamp(targetValue, 0f, MaxHealth);
        OnHealthChanged?.Invoke(Health, MaxHealth);

        if (Health == 0f)
        {
            OnDied?.Invoke();
        }
    }
}
