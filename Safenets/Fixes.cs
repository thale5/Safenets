using System;
using ColossalFramework;

namespace Safenets
{
    public sealed class Fixes : DetourUtility<Fixes>
    {
        // Redirect RoadBaseAI.CanEnableTrafficLights to my identical version below, except mine is safe.
        private Fixes()
        {
            init(typeof(RoadBaseAI), "CanEnableTrafficLights");
        }

        public static bool CanEnableTrafficLights(RoadBaseAI ai, ushort nodeID, ref NetNode data)
        {
            try
            {
                if ((data.m_flags & NetNode.Flags.Junction) == NetNode.Flags.None)
                    return false;

                int num = 0;
                int num2 = 0;
                int num3 = 0;
                NetManager instance = Singleton<NetManager>.instance;

                for (int i = 0; i < 8; i++)
                {
                    ushort segment = data.GetSegment(i);

                    if (segment != 0)
                    {
                        NetInfo info = instance.m_segments.m_buffer[(int) segment].Info;

                        if (info == null)
                        {
                            Util.DebugPrint("NetSegment has null NetInfo:", segment, data.Info?.name);
                            data.RemoveSegment(segment);
                            continue;
                        }

                        if (info.m_class == null)
                        {
                            Util.DebugPrint("NetInfo has null ItemClass:", info.name);
                            continue;
                        }

                        if (info.m_class.m_service == ItemClass.Service.Road)
                            num++;
                        else if ((info.m_vehicleTypes & VehicleInfo.VehicleType.Train) != VehicleInfo.VehicleType.None)
                            num2++;
                        if (info.m_hasPedestrianLanes)
                            num3++;
                    }
                }

                return (num < 1 || num2 < 1) && ((data.m_flags & NetNode.Flags.OneWayIn) == NetNode.Flags.None || num3 != 0);
            }
            catch (Exception e)
            {
                Util.DebugPrint("Handled exception from CanEnableTrafficLights:");
                UnityEngine.Debug.LogException(e);
            }

            return false;
        }
    }
}
