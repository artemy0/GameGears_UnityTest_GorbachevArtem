using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntityPanel : MonoBehaviour
{
    public event Action OnAttackButtonClicked;

    //[SerializeField] private
    //[Space]
    [SerializeField] private SpriteConfig spriteConfig;
    [Space]
    [SerializeField] private Transform panelParent;
    [SerializeField] private EntityPanelComponent buffPanelPrefab;
    [Space]
    [SerializeField] private Button attackButton;

    private void Awake()
    {
        attackButton.onClick.AddListener(AttackButtonClicked);
    }

    //TODO Again about abstraction
    public void UpdateData(IEnumerable<Stat> stats, IEnumerable<Buff> buffs)
    {
        UpdateStats(stats);
        UpdateBuffs(buffs);
    }
    public void UpdateStats(IEnumerable<Stat> stats)
    {
        foreach (Transform child in panelParent.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var stat in stats)
        {
            var statPanelInst = Instantiate(buffPanelPrefab, panelParent);
            var sprite = spriteConfig.GetSprite(stat.icon);
            statPanelInst.Initialize(sprite, stat.value);
        }
    }
    public void UpdateBuffs(IEnumerable<Buff> buffs)
    {
        foreach (var buff in buffs)
        {
            var buffPanelInst = Instantiate(buffPanelPrefab, panelParent);
            var sprite = spriteConfig.GetSprite(buff.icon);
            buffPanelInst.Initialize(sprite, buff.title);
        }
    }

    private void AttackButtonClicked()
    {
        OnAttackButtonClicked?.Invoke();
    }
}
