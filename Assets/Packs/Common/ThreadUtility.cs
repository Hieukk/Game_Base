﻿using System.Threading;

namespace Lance.Common
{
    public static class ThreadUtility
    {
        public static void MemoryBarrier()
        {
#if NET_4_6 || NET_STANDARD_2_0 || NETSTANDARD2_0
            Interlocked.MemoryBarrier();
#else
            Thread.MemoryBarrier();
#endif
        }

        /// <summary>
        /// The main difference between Yield and Sleep(0) is that Yield doesn't allow a switch of context
        /// that is the core is given to a thread that is already running on that core. Sleep(0) may cause
        /// a context switch
        /// to another thread.  
        /// </summary>
        public static void Yield()
        {
#if NET_4_6 || NET_STANDARD_2_0 || NETSTANDARD2_0
            Thread.Yield();
#else
            Thread.Sleep(0);
#endif
        }

        public static void TakeItEasy() { Thread.Sleep(1); }

        /// <summary>
        /// Yield the thread every so often
        /// </summary>
        /// <param name="quickIterations">will be increment by 1</param>
        /// <param name="frequency">must be power of 2</param>
        public static void Wait(ref int quickIterations, int frequency = 256)
        {
            if ((quickIterations++ & (frequency - 1)) == 0)
                Yield();
        }


        public static bool VolatileRead(ref bool val)
        {
#if NET_4_6 || NET_STANDARD_2_0 || NETSTANDARD2_0
            return Volatile.Read(ref val);
#else
            Thread.MemoryBarrier();

            return val;
#endif
        }

        public static long VolatileRead(ref long val)
        {
#if NET_4_6 || NET_STANDARD_2_0 || NETSTANDARD2_0
            return Volatile.Read(ref val);
#else
            Thread.MemoryBarrier();

            return val;
#endif
        }

        public static byte VolatileRead(ref byte val)
        {
#if NET_4_6 || NET_STANDARD_2_0 || NETSTANDARD2_0
            return Volatile.Read(ref val);
#else
            Thread.MemoryBarrier();

            return val;
#endif
        }

        public static int VolatileRead(ref int val)
        {
#if NET_4_6 || NET_STANDARD_2_0 || NETSTANDARD2_0
            return Volatile.Read(ref val);
#else
            Thread.MemoryBarrier();

            return val;
#endif
        }

        public static uint VolatileRead(ref uint val)
        {
#if NET_4_6 || NET_STANDARD_2_0 || NETSTANDARD2_0
            return Volatile.Read(ref val);
#else
            Thread.MemoryBarrier();

            return val;
#endif
        }

        public static float VolatileRead(ref float val)
        {
#if NET_4_6 || NET_STANDARD_2_0 || NETSTANDARD2_0
            return Volatile.Read(ref val);
#else
            Thread.MemoryBarrier();

            return val;
#endif
        }

        public static void VolatileWrite(ref bool var, bool val)
        {
#if NET_4_6 || NET_STANDARD_2_0 || NETSTANDARD2_0
            Volatile.Write(ref var, val);
#else
            var = val;
            Thread.MemoryBarrier();
#endif
        }

        public static void VolatileWrite(ref int var, int val)
        {
#if NET_4_6 || NET_STANDARD_2_0 || NETSTANDARD2_0
            Volatile.Write(ref var, val);
#else
            var = val;
            Thread.MemoryBarrier();
#endif
        }

        public static void VolatileWrite(ref long var, long val)
        {
#if NET_4_6 || NET_STANDARD_2_0 || NETSTANDARD2_0
            Volatile.Write(ref var, val);
#else
            var = val;
            Thread.MemoryBarrier();
#endif
        }

        public static void VolatileWrite(ref byte var, byte val)
        {
#if NET_4_6 || NET_STANDARD_2_0 || NETSTANDARD2_0
            Volatile.Write(ref var, val);
#else
            var = val;
            Thread.MemoryBarrier();
#endif
        }

        public static void VolatileWrite(ref float var, float val)
        {
#if NET_4_6 || NET_STANDARD_2_0 || NETSTANDARD2_0
            Volatile.Write(ref var, val);
#else
            var = val;
            Thread.MemoryBarrier();
#endif
        }
    }

#if NET_4_6 || NET_STANDARD_2_0 || NETSTANDARD2_0
    public sealed class ManualResetEventEx
    {
        private readonly ManualResetEventSlim _manualReset = new ManualResetEventSlim(false);

        public void Wait() { _manualReset.Wait(); }

        public void Wait(int ms) { _manualReset.Wait(ms); }

        public void Reset() { _manualReset.Reset(); }

        public void Set() { _manualReset.Set(); }

        public void Dispose() { _manualReset.Dispose(); }
    }
#else
    public class ManualResetEventEx
    {
        private readonly ManualResetEvent _manualReset = new ManualResetEvent(false);
        
        public void Wait()
        {
            _manualReset.WaitOne();
        }

        public void Wait(int ms)
        {
            _manualReset.WaitOne(ms);
        }

        public void Reset()
        {
            _manualReset.Reset();
        }

        public void Set()
        {
            _manualReset.Set();
        }

        public void Dispose()
        {
            _manualReset.Close();
        }
    }
#endif
}