using System;

namespace Safenets
{
    public abstract class Instance<T>
    {
        private static T inst;

        public static T instance
        {
            get => inst;
            set => inst = value;
        }

        public static bool HasInstance => inst != null;

        internal static T Create()
        {
            if (inst == null)
                inst = (T) Activator.CreateInstance(typeof(T), true);

            return inst;
        }
    }
}
