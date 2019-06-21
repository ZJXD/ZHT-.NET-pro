using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatterns
{
    /// <summary>
    /// 解决问题：在不同的状态下表现出不同的行为
    /// 状态模式：当一个对象的内在状态改变时，允许改变其行为，这个对象看起来像是改变了其类一样
    /// </summary>
    class StatePattern
    {

    }

    public enum StateEnum
    {
        OPEN = 1,
        CLOSE = 2,
        STOP = 3,
        RUN = 4
    }
}
