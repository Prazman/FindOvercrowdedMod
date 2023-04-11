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
        }


        public void OnDisabled()
        {

        }

    }
    #endregion

    #region Mod Behavior
    /// <summary>
    /// Here we are creating a custom ILoadingExtension; 
    /// LoadingExtensionBase implemented ILoadingExtension and provides some default behavior so we are inheriting from that.
    /// </summary>
    public class CustomLoader : LoadingExtensionBase
    {
        /// <summary>
        /// This event is triggerred when a level is loaded
        /// </summary>
        public override void OnLevelLoaded(LoadMode mode)
        {
            // Instantiate a custom object
            GameObject go = new GameObject("FindOvercrowdedMod");
            go.AddComponent<FindOvercrowdedComponent>();

            base.OnLevelLoaded(mode);
        }
    }
    #endregion

    #region Custom Game Object Components
    /// <summary>
    /// Here we creating a custom game object that directly utilize Unity Game Engine;
    /// See https://docs.unity3d.com/Manual/CreatingAndUsingScripts.html for more detail.
    /// </summary>
    public class FindOvercrowdedComponent : MonoBehaviour
    {
        /// <summary>
        /// This event is triggered when this object is created
        /// </summary>
        private List<ushort> _overcrowdedStops = new List<ushort>();
        private PTStopManager _ptStopManager = new PTStopManager();

        public bool _isModActive = false;
        private bool _processed = false;
        void Start()
        {
            SimulationManager.RegisterManager((UnityEngine.Object)ColossalFramework.Singleton<PTStopHighlightManager>.instance);
        }

        /// <summary>
        /// This event is triggered every frame, we can use this to add some animation etc.
        /// </summary>
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.O) && Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift))
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

            if (_isModActive)
            {
                if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Escape)
                {
                    OnToggleMod(false);
                }

            }
        }



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
    #endregion
}

