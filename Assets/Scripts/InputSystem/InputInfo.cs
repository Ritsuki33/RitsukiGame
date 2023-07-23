using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputInfo: IConsumable
{
    private InputController controller;

    public InputController Controller => (!m_isConsumed) ? controller : null;

    bool m_isConsumed = false;  

    public InputInfo(InputController ctr)
    {
        controller = ctr;
    }

    public void Consume()
    {
        m_isConsumed = true;
    }
}
