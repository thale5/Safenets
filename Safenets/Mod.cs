using ICities;

namespace Safenets
{
    public sealed class Mod : IUserMod, ILoadingExtension
    {
        static bool created = false;
        public string Name => "Safenets";
        public string Description => "Fix for RoadBaseAI.CanEnableTrafficLights";

        public void OnLevelLoaded(LoadMode mode) { }
        public void OnLevelUnloading() { }

        public void OnCreated(ILoading loading)
        {
            if (!created)
            {
                Fixes.Create().Deploy();
                created = true;
            }
        }

        public void OnReleased()
        {
            Fixes.instance?.Dispose();
            created = false;
        }
    }
}
