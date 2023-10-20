using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    void OnCharacterStay(PLayerController controller)
    {
        print($"lava: {controller.name}");
    }

    void OnCharacterEnter()
    {
        print("enter lava");
    }

    void OnCharacterExit()
    {
        print("exit lava");
    }
}
