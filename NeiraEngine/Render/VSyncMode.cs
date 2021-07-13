using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiraEngine.Render
{
    public enum VSyncMode
    {
        /// <summary>
        /// Vsync disabled.
        /// </summary>
        Off = 0,
        /// <summary>
        /// VSync enabled.
        /// </summary>
        On = 1,
        //
        // Сводка:
        //     VSync enabled, unless framerate falls below one half of target framerate. If
        //     no target framerate is specified, this behaves exactly like OpenTK.VSyncMode.On.
        /// <summary>
        /// VSync enabled, unless framerate falls below one half of target framerate. If no target framerate is specified, this behaves exactly like OpenTK.VSyncMode.On.
        /// </summary>
        Adaptive = 2
    }
}
