using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Input;

namespace NeiraEngine.Input
{
    /// <summary>
    /// Defines the event data for <see cref="MouseDevice"/> events.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Do not cache instances of this type outside their event handler.
    /// If necessary, you can clone an instance using the 
    /// <see cref="MouseEventArgs(MouseEventArgs)"/> constructor.
    /// </para>
    /// </remarks>
    public class MouseEventArgs : EventArgs
    {
        #region Fields

        int x, y;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public MouseEventArgs()
        {
        }

        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        /// <param name="x">The X position.</param>
        /// <param name="y">The Y position.</param>
        public MouseEventArgs(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        /// <param name="args">The <see cref="MouseEventArgs"/> instance to clone.</param>
        public MouseEventArgs(MouseEventArgs args)
            : this(args.x, args.y)
        {
        }

        #endregion

        #region Public Members

        /// <summary>
        /// Gets the X position of the mouse for the event.
        /// </summary>
        public int X { get { return x; } internal set { x = value; } }

        /// <summary>
        /// Gets the Y position of the mouse for the event.
        /// </summary>
        public int Y { get { return y; } internal set { y = value; } }

        public Vector2 Position { get { return new Vector2(x, y); } }

        #endregion
    }

    /// <summary>
    /// Defines the event data for <see cref="MouseDevice.Move"/> events.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Do not cache instances of this type outside their event handler.
    /// If necessary, you can clone an instance using the 
    /// <see cref="MouseMoveEventArgs(MouseMoveEventArgs)"/> constructor.
    /// </para>
    /// </remarks>
    public class MouseMoveEventArgs : MouseEventArgs
    {
        #region Fields

        int x_delta, y_delta;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new <see cref="MouseMoveEventArgs"/> instance.
        /// </summary>
        public MouseMoveEventArgs() { }

        /// <summary>
        /// Constructs a new <see cref="MouseMoveEventArgs"/> instance.
        /// </summary>
        /// <param name="x">The X position.</param>
        /// <param name="y">The Y position.</param>
        /// <param name="xDelta">The change in X position produced by this event.</param>
        /// <param name="yDelta">The change in Y position produced by this event.</param>
        public MouseMoveEventArgs(int x, int y, int xDelta, int yDelta)
            : base(x, y)
        {
            XDelta = xDelta;
            YDelta = yDelta;
        }

        /// <summary>
        /// Constructs a new <see cref="MouseMoveEventArgs"/> instance.
        /// </summary>
        /// <param name="args">The <see cref="MouseMoveEventArgs"/> instance to clone.</param>
        public MouseMoveEventArgs(MouseMoveEventArgs args)
            : this(args.X, args.Y, args.XDelta, args.YDelta)
        {
        }

        #endregion

        #region Public Members

        /// <summary>
        /// Gets the change in X position produced by this event.
        /// </summary>
        public int XDelta { get { return x_delta; } internal set { x_delta = value; } }

        /// <summary>
        /// Gets the change in Y position produced by this event.
        /// </summary>
        public int YDelta { get { return y_delta; } internal set { y_delta = value; } }

        #endregion
    }

    /// <summary>
    /// Defines the event data for <see cref="MouseDevice.ButtonDown"/> and <see cref="MouseDevice.ButtonUp"/> events.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Do not cache instances of this type outside their event handler.
    /// If necessary, you can clone an instance using the 
    /// <see cref="MouseButtonEventArgs(MouseButtonEventArgs)"/> constructor.
    /// </para>
    /// </remarks>
    public class MouseButtonEventArgs : MouseEventArgs
    {
        bool pressed;

        /// <summary>
        /// Constructs a new <see cref="MouseButtonEventArgs"/> instance.
        /// </summary>
        public MouseButtonEventArgs() { }

        /// <summary>
        /// Constructs a new <see cref="MouseButtonEventArgs"/> instance.
        /// </summary>
        /// <param name="x">The X position.</param>
        /// <param name="y">The Y position.</param>
        /// <param name="button">The mouse button for the event.</param>
        /// <param name="pressed">The current state of the button.</param>
        public MouseButtonEventArgs(int x, int y, MouseButton button, bool pressed)
            : base(x, y)
        {
            this.Button = button;
            this.pressed = pressed;
        }

        /// <summary>
        /// Constructs a new <see cref="MouseButtonEventArgs"/> instance.
        /// </summary>
        /// <param name="args">The <see cref="MouseButtonEventArgs"/> instance to clone.</param>
        public MouseButtonEventArgs(MouseButtonEventArgs args)
            : this(args.X, args.Y, args.Button, args.IsPressed)
        {
        }

        /// <summary>
        /// The mouse button for the event.
        /// </summary>
        public MouseButton Button { get; internal set; }

        /// <summary>
        /// Gets a System.Boolean representing the state of the mouse button for the event.
        /// </summary>
        public bool IsPressed { get { return pressed; } internal set { pressed = value; } }
    }

    /// <summary>
    /// Defines the event data for <see cref="MouseDevice.WheelChanged"/> events.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Do not cache instances of this type outside their event handler.
    /// If necessary, you can clone an instance using the 
    /// <see cref="MouseWheelEventArgs(MouseWheelEventArgs)"/> constructor.
    /// </para>
    /// </remarks>
    public class MouseWheelEventArgs : MouseEventArgs
    {

        float value;
        float delta;

        /// <summary>
        /// Constructs a new <see cref="MouseWheelEventArgs"/> instance.
        /// </summary>
        public MouseWheelEventArgs() { }

        /// <summary>
        /// Constructs a new <see cref="MouseWheelEventArgs"/> instance.
        /// </summary>
        /// <param name="x">The X position.</param>
        /// <param name="y">The Y position.</param>
        /// <param name="value">The value of the wheel.</param>
        /// <param name="delta">The change in value of the wheel for this event.</param>
        public MouseWheelEventArgs(int x, int y, int value, int delta)
            : base(x, y)
        {
            this.value = value;
            this.delta = delta;
        }

        /// <summary>
        /// Constructs a new <see cref="MouseWheelEventArgs"/> instance.
        /// </summary>
        /// <param name="args">The <see cref="MouseWheelEventArgs"/> instance to clone.</param>
        public MouseWheelEventArgs(MouseWheelEventArgs args)
            : this(args.X, args.Y, args.Value, args.Delta)
        {
        }

        /// <summary>
        /// Gets the value of the wheel in integer units.
        /// To support high-precision mice, it is recommended to use <see cref="ValuePrecise"/> instead.
        /// </summary>
        public int Value { get { return (int)Math.Round(value, MidpointRounding.AwayFromZero); } }

        /// <summary>
        /// Gets the change in value of the wheel for this event in integer units.
        /// To support high-precision mice, it is recommended to use <see cref="DeltaPrecise"/> instead.
        /// </summary>
        public int Delta { get { return (int)Math.Round(delta, MidpointRounding.AwayFromZero); } }

        /// <summary>
        /// Gets the precise value of the wheel in floating-point units.
        /// </summary>
        public float ValuePrecise { get { return value; } internal set { this.value = value; } }

        /// <summary>
        /// Gets the precise change in value of the wheel for this event in floating-point units.
        /// </summary>
        public float DeltaPrecise { get { return delta; } internal set { delta = value; } }
    }
}
