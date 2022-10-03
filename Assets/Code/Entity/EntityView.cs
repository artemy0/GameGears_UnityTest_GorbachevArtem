using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EntityView : MonoBehaviour
{
    public const string ATTACK_KEY = "Attack";
    public const string DEATH_KEY = "Death";
    public const string RESTORE_KEY = "Restore";

    [Header("UI")]
    [SerializeField] private Image healthSlider;
    [SerializeField] private Text healthText;
    [Space]
    [SerializeField] private EntityPanel entityPanel;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    public void Initialize(IEnumerable<Stat> stats)
    {
        PlayRestore();

        entityPanel.UpdateStats(stats);
    }
    public void Initialize(IEnumerable<Stat> stats, IEnumerable<Buff> buffs)
    {
        Initialize(stats);

        entityPanel.UpdateBuffs(buffs);
    }

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        healthSlider.fillAmount = Mathf.Clamp01(currentHealth / maxHealth);
        healthText.text = $"{currentHealth}/{maxHealth}";
    }

    public void UpdateView(EntityModel entityModel)
    {
        UpdateHealth(entityModel.Health, entityModel.MaxHealth);
    }

    public void PlayAttack()
    {
        animator.SetTrigger(ATTACK_KEY);
    }

    public void PlayDeath()
    {
        animator.SetTrigger(DEATH_KEY);
    }

    public void PlayRestore()
    {
        animator.SetTrigger(RESTORE_KEY);
    }
}
