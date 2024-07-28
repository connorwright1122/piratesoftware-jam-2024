using UnityEngine;

public interface I_Transmutable
{
    //public void TakeDamage(int damageAmount);
    //public bool IsDestroyed();
    //public string  

    //public int type;

    public bool IsDestroyable();
    public void InitiateDestruction();
    public int GetDestructionNumber();
}
