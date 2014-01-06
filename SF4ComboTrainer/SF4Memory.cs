﻿using System;

using MemoryEditor;

namespace SF4ComboTrainer
{
    /**
     * this class reads data from the memory of a running sf4 instance.
     * atm it only reads the current framecount and the x positions of both players.
     * other adresses are
     * 
     * INFO         TYPE        ADDRESS     OFFSETS
     * --------------------------------------------------
     * p1posY       float       0x80f0cc    8, 100
     * p1exmeter    int         0x80f0cc    8, 0x6c64
     * p1health     int         0x80f0cc    8, 0x6c5c
     * p1recovery   int         0x80f0cc    8, 160, 220
     * p1ultra      int         0x80f0cc    8, 0x6c78
     * p2posY       float       0x80f0cc    12, 100
     * p2exmeter    int         0x80f0cc    12, 0x6c64
     * p2health     int         0x80f0cc    12, 0x6c5c
     * p2ultra      int         0x80f0cc    12, 0x6c78
     * 
     * i haven't tested these other adresses they may or may not work
     * if they don't try removing the added steamVersionMemoryOffset
     */
    class SF4Memory
    {
        private int steamVersionMemoryOffset;
        private Memory memory = new Memory();

        public SF4Memory(bool steamVersion)
        {
            setSteamVersion(steamVersion);
        }

        public void setSteamVersion(bool steamVersion)
        {
            if (steamVersion)
            {
                steamVersionMemoryOffset = 0x10c0;
            }
            else
            {
                steamVersionMemoryOffset = 0;
            }
        }

        private int readIntFromGameMemory(int address, int[] offsets)
        {
            if (this.memory.OpenProcess("SSFIV"))
            {
                return Convert.ToInt32(this.memory.ReadInt(this.memory.BaseAddress() + (address + steamVersionMemoryOffset), offsets));

            }
            else
            {
                return -1;
            }
        }

        private float readFloatFromGameMemory(int address, int[] offsets)
        {
            if (this.memory.OpenProcess("SSFIV"))
            {
                return this.memory.ReadFloat(this.memory.BaseAddress() + (address + steamVersionMemoryOffset), offsets);

            }
            else
            {
                return -1;
            }
        }

        public int getFrameCount()
        {
            return readIntFromGameMemory(0x80f0d0, new int[] { 0x1c4 });
        }



        public float getP1PosX()
        {
            return readFloatFromGameMemory(0x80f0cc, new int[] { 8, 0x60 });
        }



        public float getP2PosX()
        {
            return readFloatFromGameMemory(0x80f0cc, new int[] { 12, 0x60 });

        }


    }
}
