using UnityEngine;

public interface I_Transmutable
{
    //public void TakeDamage(int damageAmount);
    //public bool IsDestroyed();
    //public string  

    //public int type;

    public bool IsDestroyable();
    public void InitiateAction();
    public int GetDestructionNumber();
    public int GetCurrentType();
    public void SetCurrentType(int newType);
}
