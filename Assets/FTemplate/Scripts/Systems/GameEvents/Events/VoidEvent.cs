using UnityEngine;

[CreateAssetMenu(fileName = "Void Event", menuName = "Game Events/Create Void Event")]
public class VoidEvent : BaseGameEvent<Void>
{
    public void Raise() => Raise(new Void());
}

