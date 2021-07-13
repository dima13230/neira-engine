using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiraEngine
{
    public class FrameEventArgs : EventArgs
    {
        //
        // Сводка:
        //     Constructs a new FrameEventArgs instance.
        public FrameEventArgs() { }
        //
        // Сводка:
        //     Constructs a new FrameEventArgs instance.
        //
        // Параметры:
        //   elapsed:
        //     The amount of time that has elapsed since the previous event, in seconds.
        public FrameEventArgs(double elapsed) { Time = elapsed; }

        //
        // Сводка:
        //     Gets a System.Double that indicates how many seconds of time elapsed since the
        //     previous event.
        public double Time { get; }
    }
}