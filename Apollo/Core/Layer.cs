namespace Apollo.Core
{
    public class Layer
    {
        public string Name { get; private set; }

        public Layer(string name = "Layer")
        {
            Name = name;
        }

        public virtual void OnAttach()
        {

        }

        public virtual void OnDetach()
        {

        }

        public virtual void OnUpdate(double delta)
        {

        }

        public virtual void OnRender(double delta)
        {

        }

        public virtual void OnEvent(Event e)
        {

        }
    }
}
