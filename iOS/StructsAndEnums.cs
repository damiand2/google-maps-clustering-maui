﻿using System.Runtime.InteropServices;

namespace GMapUtils.iOS
{
    [StructLayout(LayoutKind.Sequential)]
    public struct GQTPoint
    {
        public double x;

        public double y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GQTBounds
    {
        public double minX;

        public double minY;

        public double maxX;

        public double maxY;
    }
}

