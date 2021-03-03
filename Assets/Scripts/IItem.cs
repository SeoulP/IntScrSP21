using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    void Pickup(Transform hand);
    void Use();

    void SecondaryUse();
    void Drop();
}
