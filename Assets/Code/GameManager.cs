using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EntityController firstEntity;
    [SerializeField] private EntityController secondEntity;

    [SerializeField] private EntityPanel firstEntityPanel;
    [SerializeField] private EntityPanel secondEntityPanel;

    private IDataParser dataParser;
    private Data data;

    private EntityDataFactory entityDataFactory;

    private void Start()
    {
        dataParser = new JsonDataParser();
        data = dataParser.GetData();

        entityDataFactory = new EntityDataFactory(data);

        InitGameWithoutBuff();
        StartGame();
    }

    public void InitGameWithoutBuff()
    {
        firstEntity.Initialize(data.stats);
        secondEntity.Initialize(data.stats);
    }
    
    public void InitGameWithBuff()
    {
        firstEntity.Initialize(data.stats, entityDataFactory.GetRandomBuffs());
        secondEntity.Initialize(data.stats, entityDataFactory.GetRandomBuffs());
    }

    private void StartGame()
    {
        firstEntityPanel.OnAttackButtonClicked += FirstAttackSecond;
        secondEntityPanel.OnAttackButtonClicked += SecondAttackFirst;
    }
    private void OnDestroy()
    {
        firstEntityPanel.OnAttackButtonClicked -= FirstAttackSecond;
        secondEntityPanel.OnAttackButtonClicked -= SecondAttackFirst;
    }

    private void FirstAttackSecond() => Attack(firstEntity, secondEntity);
    private void SecondAttackFirst() => Attack(secondEntity, firstEntity);
    private void Attack(EntityController attacker, EntityController target)
    {
        attacker.DealDamage(target);
    }
}