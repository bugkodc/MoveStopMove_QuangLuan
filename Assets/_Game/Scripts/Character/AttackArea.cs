using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour, IHit
{
    [SerializeField] private Transform TF;
    public Character character;
    public float chaseRange => transform.lossyScale.x;
    private void OnTriggerEnter(Collider other)
    {
        CheckAttackAreaEnter(other);
    }
    private void OnTriggerExit(Collider other)
    {
        CheckAttackAreaExit(other);
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(TF.position, chaseRange / 2);
    }
    public void CheckAttackAreaEnter(Collider other)
    {
        Character charInArea = Cache.GetCharacter(other);
        if (charInArea != null && character?.gameObject != null && charInArea?.gameObject != character?.gameObject)
        {
            character?.listCharInAttact.Add(charInArea);
            charInArea.UnderObj.SetActive(true);
        }
    }
    public void CheckAttackAreaExit(Collider other)
    {
        Character charInArea = Cache.GetCharacter(other);
        if (charInArea != null)
        {
            character?.listCharInAttact.Remove(charInArea);
            charInArea.UnderObj.SetActive(false);
        }
    }

    public void OnHit(Bullet bullet, Character character)
    {

    }
    public void OnHitExit(Bullet bullet, Character character)
    {
        if (bullet.character == this.character)
        {
            bullet.OnDespawn();
        }
    }
}
