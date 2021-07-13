﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiraEngine.Render.OpenGL
{
    public enum BeginMode
    {
        //
        // Сводка:
        //     Original was GL_POINTS = 0x0000
        Points = 0,
        //
        // Сводка:
        //     Original was GL_LINES = 0x0001
        Lines = 1,
        //
        // Сводка:
        //     Original was GL_LINE_LOOP = 0x0002
        LineLoop = 2,
        //
        // Сводка:
        //     Original was GL_LINE_STRIP = 0x0003
        LineStrip = 3,
        //
        // Сводка:
        //     Original was GL_TRIANGLES = 0x0004
        Triangles = 4,
        //
        // Сводка:
        //     Original was GL_TRIANGLE_STRIP = 0x0005
        TriangleStrip = 5,
        //
        // Сводка:
        //     Original was GL_TRIANGLE_FAN = 0x0006
        TriangleFan = 6,
        //
        // Сводка:
        //     Original was GL_QUADS = 0x0007
        Quads = 7,
        //
        // Сводка:
        //     Original was GL_QUAD_STRIP = 0x0008
        QuadStrip = 8,
        //
        // Сводка:
        //     Original was GL_POLYGON = 0x0009
        Polygon = 9,
        //
        // Сводка:
        //     Original was GL_LINES_ADJACENCY = 0xA
        LinesAdjacency = 10,
        //
        // Сводка:
        //     Original was GL_LINE_STRIP_ADJACENCY = 0xB
        LineStripAdjacency = 11,
        //
        // Сводка:
        //     Original was GL_TRIANGLES_ADJACENCY = 0xC
        TrianglesAdjacency = 12,
        //
        // Сводка:
        //     Original was GL_TRIANGLE_STRIP_ADJACENCY = 0xD
        TriangleStripAdjacency = 13,
        //
        // Сводка:
        //     Original was GL_PATCHES = 0x000E
        Patches = 14
    }
}
