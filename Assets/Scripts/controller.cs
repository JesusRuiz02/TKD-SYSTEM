using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.InputSystem;
public class controller : MonoBehaviour
{
    public static ReadOnlyArray<InputDevice> disconnectedDevices { get; }
    
    public void message2()
    {
        Debug.Log("hola");
    }

    public void message1()
    {
        Debug.Log("adios");
    }
}
