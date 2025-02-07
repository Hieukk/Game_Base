﻿using System;

namespace Lance.Common
{
    public static class SafeEvent
    {
        public static void SafeRaise<T>(this Action<T> onEvent, T val)
        {
            if (onEvent != null)
            {
                var length = onEvent.GetInvocationList().Length;
                for (int index = 0; index < length; index++)
                {
                    Action<T> handler = (Action<T>) onEvent.GetInvocationList()[index];

                    handler?.Invoke(val);
                }
            }
        }

        public static void SafeRaise(this Action onEvent)
        {
            if (onEvent != null)
            {
                var length = onEvent.GetInvocationList().Length;
                for (int index = 0; index < length; index++)
                {
                    Action handler = (Action) onEvent.GetInvocationList()[index];

                    handler?.Invoke();
                }
            }
        }
    }
}