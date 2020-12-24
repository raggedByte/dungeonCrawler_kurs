using UnityEngine;

public interface IInteractive
{
    void Interact(GameObject sender);
    void InteractEnd(GameObject sender);
}