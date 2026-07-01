using UnityEngine;

public interface IDamage
{

    public void takeDamage(float amount);
    int Team { get; }
}
