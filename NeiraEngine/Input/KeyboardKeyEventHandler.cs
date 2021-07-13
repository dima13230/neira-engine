using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Input;

namespace NeiraEngine.Input
{
    /// <summary>
    /// Defines the event data for <see cref="KeyboardDevice"/> events.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Do not cache instances of this type outside their event handler.
    /// If necessary, you can clone a KeyboardEventArgs instance using the 
    /// <see cref="KeyboardKeyEventArgs(KeyboardKeyEventArgs)"/> constructor.
    /// </para>
    /// </remarks>
    public class KeyboardKeyEventArgs : EventArgs
    {
        /// <summary>
        /// Constructs a new KeyboardEventArgs instance.
        /// </summary>
        public KeyboardKeyEventArgs() { }

        /// <summary>
        /// Constructs a new KeyboardEventArgs instance.
        /// </summary>
        /// <param name="args">An existing KeyboardEventArgs instance to clone.</param>
        public KeyboardKeyEventArgs(KeyboardKeyEventArgs args)
        {
            Key = args.Key;
        }

        internal KeyboardKeyEventArgs(Key key)
        {
            Key = key;
        }

        /// <summary>
        /// Gets the <see cref="Key"/> that generated this event.
        /// </summary>
        public Key Key { get; internal set; }
    }
}