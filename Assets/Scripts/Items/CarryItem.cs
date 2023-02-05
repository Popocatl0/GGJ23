using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CarryItem : MonoBehaviour{
    public abstract void Init(Transform parent);

    public abstract CarryItem OnCarry();

    public abstract void OnSet(PlayerController controller);
    public abstract void OnCharge(CarryItem item);

}
