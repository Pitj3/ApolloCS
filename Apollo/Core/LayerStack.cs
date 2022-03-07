using System.Collections.Generic;

namespace Apollo.Core
{
    public class LayerStack
    {
        internal readonly List<Layer> layers = new List<Layer>();
        private int _layerInsertIndex;

        public void PushLayer(Layer layer)
        {
            layers.Insert(_layerInsertIndex++, layer);
            layer.OnAttach();
        }

        public void PushOverlay(Layer layer)
        {
            layers.Add(layer);
            layer.OnAttach();
        }

        public void PopLayer(Layer layer)
        {
            layer.OnDetach();
            layers.Remove(layer);
            _layerInsertIndex--;
        }

        public void PopOverlay(Layer layer)
        {
            layer.OnDetach();
            layers.Remove(layer);
        }
    }
}
