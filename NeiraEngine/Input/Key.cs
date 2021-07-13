using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiraEngine.Input
{
    public enum Key
    {
        //
        // Сводка:
        //     A key outside the known keys.
        Unknown = 0,
        //
        // Сводка:
        //     The left shift key.
        ShiftLeft = 1,
        //
        // Сводка:
        //     The left shift key (equivalent to ShiftLeft).
        LShift = 1,
        //
        // Сводка:
        //     The right shift key.
        ShiftRight = 2,
        //
        // Сводка:
        //     The right shift key (equivalent to ShiftRight).
        RShift = 2,
        //
        // Сводка:
        //     The left control key.
        ControlLeft = 3,
        //
        // Сводка:
        //     The left control key (equivalent to ControlLeft).
        LControl = 3,
        //
        // Сводка:
        //     The right control key.
        ControlRight = 4,
        //
        // Сводка:
        //     The right control key (equivalent to ControlRight).
        RControl = 4,
        //
        // Сводка:
        //     The left alt key.
        AltLeft = 5,
        //
        // Сводка:
        //     The left alt key (equivalent to AltLeft.
        LAlt = 5,
        //
        // Сводка:
        //     The right alt key.
        AltRight = 6,
        //
        // Сводка:
        //     The right alt key (equivalent to AltRight).
        RAlt = 6,
        //
        // Сводка:
        //     The left win key.
        WinLeft = 7,
        //
        // Сводка:
        //     The left win key (equivalent to WinLeft).
        LWin = 7,
        //
        // Сводка:
        //     The right win key.
        WinRight = 8,
        //
        // Сводка:
        //     The right win key (equivalent to WinRight).
        RWin = 8,
        //
        // Сводка:
        //     The menu key.
        Menu = 9,
        //
        // Сводка:
        //     The F1 key.
        F1 = 10,
        //
        // Сводка:
        //     The F2 key.
        F2 = 11,
        //
        // Сводка:
        //     The F3 key.
        F3 = 12,
        //
        // Сводка:
        //     The F4 key.
        F4 = 13,
        //
        // Сводка:
        //     The F5 key.
        F5 = 14,
        //
        // Сводка:
        //     The F6 key.
        F6 = 15,
        //
        // Сводка:
        //     The F7 key.
        F7 = 16,
        //
        // Сводка:
        //     The F8 key.
        F8 = 17,
        //
        // Сводка:
        //     The F9 key.
        F9 = 18,
        //
        // Сводка:
        //     The F10 key.
        F10 = 19,
        //
        // Сводка:
        //     The F11 key.
        F11 = 20,
        //
        // Сводка:
        //     The F12 key.
        F12 = 21,
        //
        // Сводка:
        //     The F13 key.
        F13 = 22,
        //
        // Сводка:
        //     The F14 key.
        F14 = 23,
        //
        // Сводка:
        //     The F15 key.
        F15 = 24,
        //
        // Сводка:
        //     The F16 key.
        F16 = 25,
        //
        // Сводка:
        //     The F17 key.
        F17 = 26,
        //
        // Сводка:
        //     The F18 key.
        F18 = 27,
        //
        // Сводка:
        //     The F19 key.
        F19 = 28,
        //
        // Сводка:
        //     The F20 key.
        F20 = 29,
        //
        // Сводка:
        //     The F21 key.
        F21 = 30,
        //
        // Сводка:
        //     The F22 key.
        F22 = 31,
        //
        // Сводка:
        //     The F23 key.
        F23 = 32,
        //
        // Сводка:
        //     The F24 key.
        F24 = 33,
        //
        // Сводка:
        //     The F25 key.
        F25 = 34,
        //
        // Сводка:
        //     The F26 key.
        F26 = 35,
        //
        // Сводка:
        //     The F27 key.
        F27 = 36,
        //
        // Сводка:
        //     The F28 key.
        F28 = 37,
        //
        // Сводка:
        //     The F29 key.
        F29 = 38,
        //
        // Сводка:
        //     The F30 key.
        F30 = 39,
        //
        // Сводка:
        //     The F31 key.
        F31 = 40,
        //
        // Сводка:
        //     The F32 key.
        F32 = 41,
        //
        // Сводка:
        //     The F33 key.
        F33 = 42,
        //
        // Сводка:
        //     The F34 key.
        F34 = 43,
        //
        // Сводка:
        //     The F35 key.
        F35 = 44,
        //
        // Сводка:
        //     The up arrow key.
        Up = 45,
        //
        // Сводка:
        //     The down arrow key.
        Down = 46,
        //
        // Сводка:
        //     The left arrow key.
        Left = 47,
        //
        // Сводка:
        //     The right arrow key.
        Right = 48,
        //
        // Сводка:
        //     The enter key.
        Enter = 49,
        //
        // Сводка:
        //     The escape key.
        Escape = 50,
        //
        // Сводка:
        //     The space key.
        Space = 51,
        //
        // Сводка:
        //     The tab key.
        Tab = 52,
        //
        // Сводка:
        //     The backspace key.
        BackSpace = 53,
        //
        // Сводка:
        //     The backspace key (equivalent to BackSpace).
        Back = 53,
        //
        // Сводка:
        //     The insert key.
        Insert = 54,
        //
        // Сводка:
        //     The delete key.
        Delete = 55,
        //
        // Сводка:
        //     The page up key.
        PageUp = 56,
        //
        // Сводка:
        //     The page down key.
        PageDown = 57,
        //
        // Сводка:
        //     The home key.
        Home = 58,
        //
        // Сводка:
        //     The end key.
        End = 59,
        //
        // Сводка:
        //     The caps lock key.
        CapsLock = 60,
        //
        // Сводка:
        //     The scroll lock key.
        ScrollLock = 61,
        //
        // Сводка:
        //     The print screen key.
        PrintScreen = 62,
        //
        // Сводка:
        //     The pause key.
        Pause = 63,
        //
        // Сводка:
        //     The num lock key.
        NumLock = 64,
        //
        // Сводка:
        //     The clear key (Keypad5 with NumLock disabled, on typical keyboards).
        Clear = 65,
        //
        // Сводка:
        //     The sleep key.
        Sleep = 66,
        //
        // Сводка:
        //     The keypad 0 key.
        Keypad0 = 67,
        //
        // Сводка:
        //     The keypad 1 key.
        Keypad1 = 68,
        //
        // Сводка:
        //     The keypad 2 key.
        Keypad2 = 69,
        //
        // Сводка:
        //     The keypad 3 key.
        Keypad3 = 70,
        //
        // Сводка:
        //     The keypad 4 key.
        Keypad4 = 71,
        //
        // Сводка:
        //     The keypad 5 key.
        Keypad5 = 72,
        //
        // Сводка:
        //     The keypad 6 key.
        Keypad6 = 73,
        //
        // Сводка:
        //     The keypad 7 key.
        Keypad7 = 74,
        //
        // Сводка:
        //     The keypad 8 key.
        Keypad8 = 75,
        //
        // Сводка:
        //     The keypad 9 key.
        Keypad9 = 76,
        //
        // Сводка:
        //     The keypad divide key.
        KeypadDivide = 77,
        //
        // Сводка:
        //     The keypad multiply key.
        KeypadMultiply = 78,
        //
        // Сводка:
        //     The keypad subtract key.
        KeypadSubtract = 79,
        //
        // Сводка:
        //     The keypad minus key (equivalent to KeypadSubtract).
        KeypadMinus = 79,
        //
        // Сводка:
        //     The keypad add key.
        KeypadAdd = 80,
        //
        // Сводка:
        //     The keypad plus key (equivalent to KeypadAdd).
        KeypadPlus = 80,
        //
        // Сводка:
        //     The keypad decimal key.
        KeypadDecimal = 81,
        //
        // Сводка:
        //     The keypad period key (equivalent to KeypadDecimal).
        KeypadPeriod = 81,
        //
        // Сводка:
        //     The keypad enter key.
        KeypadEnter = 82,
        //
        // Сводка:
        //     The A key.
        A = 83,
        //
        // Сводка:
        //     The B key.
        B = 84,
        //
        // Сводка:
        //     The C key.
        C = 85,
        //
        // Сводка:
        //     The D key.
        D = 86,
        //
        // Сводка:
        //     The E key.
        E = 87,
        //
        // Сводка:
        //     The F key.
        F = 88,
        //
        // Сводка:
        //     The G key.
        G = 89,
        //
        // Сводка:
        //     The H key.
        H = 90,
        //
        // Сводка:
        //     The I key.
        I = 91,
        //
        // Сводка:
        //     The J key.
        J = 92,
        //
        // Сводка:
        //     The K key.
        K = 93,
        //
        // Сводка:
        //     The L key.
        L = 94,
        //
        // Сводка:
        //     The M key.
        M = 95,
        //
        // Сводка:
        //     The N key.
        N = 96,
        //
        // Сводка:
        //     The O key.
        O = 97,
        //
        // Сводка:
        //     The P key.
        P = 98,
        //
        // Сводка:
        //     The Q key.
        Q = 99,
        //
        // Сводка:
        //     The R key.
        R = 100,
        //
        // Сводка:
        //     The S key.
        S = 101,
        //
        // Сводка:
        //     The T key.
        T = 102,
        //
        // Сводка:
        //     The U key.
        U = 103,
        //
        // Сводка:
        //     The V key.
        V = 104,
        //
        // Сводка:
        //     The W key.
        W = 105,
        //
        // Сводка:
        //     The X key.
        X = 106,
        //
        // Сводка:
        //     The Y key.
        Y = 107,
        //
        // Сводка:
        //     The Z key.
        Z = 108,
        //
        // Сводка:
        //     The number 0 key.
        Number0 = 109,
        //
        // Сводка:
        //     The number 1 key.
        Number1 = 110,
        //
        // Сводка:
        //     The number 2 key.
        Number2 = 111,
        //
        // Сводка:
        //     The number 3 key.
        Number3 = 112,
        //
        // Сводка:
        //     The number 4 key.
        Number4 = 113,
        //
        // Сводка:
        //     The number 5 key.
        Number5 = 114,
        //
        // Сводка:
        //     The number 6 key.
        Number6 = 115,
        //
        // Сводка:
        //     The number 7 key.
        Number7 = 116,
        //
        // Сводка:
        //     The number 8 key.
        Number8 = 117,
        //
        // Сводка:
        //     The number 9 key.
        Number9 = 118,
        //
        // Сводка:
        //     The tilde key.
        Tilde = 119,
        //
        // Сводка:
        //     The grave key (equivaent to Tilde).
        Grave = 119,
        //
        // Сводка:
        //     The minus key.
        Minus = 120,
        //
        // Сводка:
        //     The plus key.
        Plus = 121,
        //
        // Сводка:
        //     The left bracket key.
        BracketLeft = 122,
        //
        // Сводка:
        //     The left bracket key (equivalent to BracketLeft).
        LBracket = 122,
        //
        // Сводка:
        //     The right bracket key.
        BracketRight = 123,
        //
        // Сводка:
        //     The right bracket key (equivalent to BracketRight).
        RBracket = 123,
        //
        // Сводка:
        //     The semicolon key.
        Semicolon = 124,
        //
        // Сводка:
        //     The quote key.
        Quote = 125,
        //
        // Сводка:
        //     The comma key.
        Comma = 126,
        //
        // Сводка:
        //     The period key.
        Period = 127,
        //
        // Сводка:
        //     The slash key.
        Slash = 128,
        //
        // Сводка:
        //     The backslash key.
        BackSlash = 129,
        //
        // Сводка:
        //     The secondary backslash key.
        NonUSBackSlash = 130,
        //
        // Сводка:
        //     Indicates the last available keyboard key.
        LastKey = 131
    }
}