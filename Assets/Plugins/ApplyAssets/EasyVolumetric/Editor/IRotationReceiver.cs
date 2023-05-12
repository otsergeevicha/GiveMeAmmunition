using UnityEngine;

/// <summary>
/// Interface to receive Vector3 rotation.
/// </summary>
public interface IRotationReceiver
{

    void ReceiveRotation(Vector3 rotation);

}