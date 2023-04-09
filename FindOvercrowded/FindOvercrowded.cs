using ICities;
using UnityEngine;
using System.Collections.Generic;

namespace FindOvercrowdedMod
{
    #region Mod Definition
    /// <summary>
    /// Provide description of the mod to the game, this is shown during mod loading screen.
    /// </summary>
    public class FindOvercrowdedMod : IUserMod
    {

        public string Name
        {
            get { return "Highlight Overcrowded Stops Mod"; }
        }

        public string Description
        {
            get { return "Highlights public transport stops with too many passengers."; }
        }
        public void OnEnabled()
        {
            SimulationManager.RegisterManager((UnityEngine.Object)ColossalFramework.Singleton<PTStopHighlightManager>.instance);
        }


        public void OnDisabled()
        {

        }

    }
    #endregion
    public class FindOvercrowdedModThreading : ThreadingExtensionBase
    {
        private bool _processed = false;


        public override void OnUpdate(float realTimeDelta, float simulationTimeDelta)
        {
            if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKey(KeyCode.I))
            {
                // cancel if they key input was already processed in a previous frame
                if (_processed) return;

                _processed = true;
                OnToggleMod(!_isModActive);

            }
            else
            {
                // not both keys pressed: Reset processed state
                _processed = false;
            }
        }


        private List<ushort> _overcrowdedStops = new List<ushort>();
        private PTStopManager _ptStopManager = new PTStopManager();

        public bool _isModActive = false;


        private void OnToggleMod(bool value)
        {
            _isModActive = value;
            if (_isModActive)
            {
                _ptStopManager.UpdateOvercrowdedStopsList(_overcrowdedStops);
                setHighlightedStops();
            }
            else
            {
                _overcrowdedStops.Clear();
                setHighlightedStops();
            }
        }

        private void setHighlightedStops()
        {

            PTStopHighlightManager.instance.Highlight(_overcrowdedStops);

        }

    }
}
