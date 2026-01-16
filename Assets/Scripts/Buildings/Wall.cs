using UnityEngine;

public class Wall : Building
{
    public override void Destroy()
    {
        Destroy(gameObject);
    }
}
