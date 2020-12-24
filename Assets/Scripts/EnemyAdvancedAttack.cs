using UnityEngine;

public class EnemyAdvancedAttack : EnemySimpleAttack
{
    [SerializeField] private GameObject FireballPrefab;
    protected override void Attack(Character target)
    {
        GameObject fireballInstace = Instantiate(FireballPrefab, transform.position, Quaternion.identity);
        fireballInstace.GetComponent<Fireball>().SetTarget(target.transform.position);
    }
}