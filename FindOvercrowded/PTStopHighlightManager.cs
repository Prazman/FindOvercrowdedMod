using System.Collections.Generic;
using UnityEngine;

namespace FindOvercrowdedMod
{
 


    public class PTStopHighlightManager : SimulationManagerBase<PTStopHighlightManager, MonoBehaviour>, IRenderableManager
    {
        private static bool _instantiated;

        private List<PTStopHighlighter> _highlightables = new List<PTStopHighlighter>();
        protected override void Awake()
        {
            base.Awake();
            DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "Simulation Manager Awake");

            _instantiated = true;
        }

        private void OnDestroy()
        {
            _instantiated = false;

        }

        private void OnDisable()
        {
            _highlightables.Clear();
        }

        public void Highlight(List<ushort> _overcrowdedStops)
        {
            _highlightables.Clear();
            foreach (ushort stop in _overcrowdedStops)
            {
                PTStopHighlighter highlightable = new PTStopHighlighter();
                highlightable.SetNode(stop);
                _highlightables.Add(highlightable);
            }
        }


        protected override void BeginOverlayImpl(RenderManager.CameraInfo cameraInfo)
        {
            base.BeginOverlayImpl(cameraInfo);
            if (!IsInstanceValid()) return;
            foreach (PTStopHighlighter stopHighlighter in _highlightables)
            {
                stopHighlighter.Render(cameraInfo);
            }
        }
        private bool IsInstanceValid()
        {
            return _instantiated && _highlightables.Count > 0;
        }

    }

}
