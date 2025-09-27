using UnityEngine;

[CreateAssetMenu(menuName = "Items/Health Pack")]
public class HealthPackData : Item
{
    public int healAmount = 25;

    public override bool OnUse(IInteractor interactor)
    {
        if (interactor.GameObject.TryGetComponent<Health>(out var health))
        {
            health.Heal(healAmount);
            Debug.Log($"{interactor.GameObject.name} healed for {healAmount}");
            return true;
        }
        return false;
    }
}
