using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDataFactory
{
    //private Stat[] stats;
    private Buff[] buffs;

    private int minBuffCount;
    private int maxBuffCount;

    public EntityDataFactory(Data data)
    {
        //stats = data.stats;
        buffs = data.buffs;

        minBuffCount = Mathf.Clamp(data.settings.buffCountMin, 1, buffs.Length);
        maxBuffCount = Mathf.Clamp(data.settings.buffCountMax, minBuffCount, buffs.Length);
    }

    public List<Buff> GetRandomBuffs()
    {
        var buffCount = Random.Range(minBuffCount, maxBuffCount);
        var randomBuffs = new List<Buff>(buffs);

        while(randomBuffs.Count > buffCount)
        {
            var randomBuffIndex = Random.Range(0, randomBuffs.Count - 1);
            randomBuffs.RemoveAt(randomBuffIndex);
        }

        return randomBuffs;
    }
}
