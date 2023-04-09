using UnityEngine;

namespace FindOvercrowdedMod
{
    public class PTStopHighlighter
    {
        private ushort _nodeId;

        public void SetNode(ushort nodeId)
        {
            _nodeId = nodeId;
        }

        public void Render(RenderManager.CameraInfo cameraInfo)
        {
            if (!IsValidData()) return;

            NetNode node = NetManager.instance.m_nodes.m_buffer[_nodeId];
            Color color = Color.red;
            color.a = 0.7f;
            RenderManager.instance.OverlayEffect.DrawCircle(cameraInfo, color, node.m_position, node.m_bounds.size.x * 10, node.m_position.y - 20f, node.m_position.y + 20f, false, false);
        }

        private bool IsValidData()
        {
            if (_nodeId != 0)
            {
                if ((NetManager.instance.m_nodes.m_buffer[_nodeId].m_flags & NetNode.Flags.Created) != 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
