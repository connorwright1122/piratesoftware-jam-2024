using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private List<I_Transmutable> _damageablesInRange = new List<I_Transmutable>();

    private void OnTriggerEnter(Collider other)
    {

        var damageable = other.GetComponent<I_Transmutable>();
        if (damageable != null)
        {
            _damageablesInRange.Add(damageable);
            Debug.Log("New damageable");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var damageable = other.GetComponent<I_Transmutable>();
        if (damageable != null && _damageablesInRange.Contains(damageable))
        {
            _damageablesInRange.Remove(damageable);
        }
    }

    public List<I_Transmutable> GetDamageablesInRange()
    {
        return _damageablesInRange;
    }
}
