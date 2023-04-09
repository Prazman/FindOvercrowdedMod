using System.Collections.Generic;


namespace FindOvercrowdedMod
{
    public class PTStopManager
    {
        public void UpdateOvercrowdedStopsList(List<ushort> _overcrowdedStops)
        {
            _overcrowdedStops.Clear();
            TransportManager tm = TransportManager.instance;
            TransportLine[] mBuffer = tm.m_lines.m_buffer;
            for (int i = 0; i < tm.m_lines.m_size; i++)
            {

                if ((mBuffer[i].m_flags & TransportLine.Flags.Created) != 0)
                {
                    ushort lineNumber = (ushort)i;
                    ref TransportLine line = ref TransportManager.instance.m_lines.m_buffer[lineNumber];
                    int lineVehicleCapacity = line.GetLineVehicle(lineNumber).m_vehicleAI.GetPassengerCapacity(true);
                    ushort firstId = line.m_stops;
                    if (firstId != 0)
                    {
                        ushort next = TransportLine.GetPrevStop(firstId);
                        if (isOvercrowded(lineVehicleCapacity, line, firstId))
                        {
                            _overcrowdedStops.Add(next);
                        }

                        while (next != 0 && firstId != next)
                        {
                            if(isOvercrowded(lineVehicleCapacity, line, next)) {
                                _overcrowdedStops.Add(next);
                            }

                            next = TransportLine.GetPrevStop(next);
                        }
                    }

                }

            }

        }
        // A stop is considered overcrowded if waiting passenger number is over a single vehicle capacity
        private bool isOvercrowded(int lineVehicleCapacity, TransportLine line, ushort stop)
        {
            return (line.CalculatePassengerCount(stop) > lineVehicleCapacity);
        }
    }
}
