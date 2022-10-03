using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    public bool IsAlive => model.IsAlive;

    [SerializeField] private EntityView view;

    private EntityModel model;

    public void Initialize(IEnumerable<Stat> stats, IEnumerable<Buff> buffs)
    {
        model = new EntityModel(stats, buffs);
        view.Initialize(stats, buffs);

        Initialize();
    }
    public void Initialize(IEnumerable<Stat> stats)
    {
        model = new EntityModel(stats);
        view.Initialize(stats);

        Initialize();
    }
    private void Initialize()
    {
        view.UpdateView(model);

        //Sub
        model.OnHealthChanged += view.UpdateHealth;
        model.OnDied += view.PlayDeath;
    }

    private void OnDestroy()
    {
        //Unsub
        model.OnHealthChanged -= view.UpdateHealth;
        model.OnDied -= view.PlayDeath;
    }

    public void DealDamage(EntityController controller)
    {
        if (IsAlive && controller.IsAlive)
        {
            var damage = model.DealDamage();
            view.PlayAttack();

            controller.TakeDamage(damage);
        }
    }

    public void TakeDamage(float damage)
    {
        model.TakeDamage(damage);
    }
}
