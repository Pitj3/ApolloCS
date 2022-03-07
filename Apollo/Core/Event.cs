using System;

namespace Apollo.Core
{
    public class Event
    {
        public bool Handled { get; internal set; }

        public Type GetEventType => GetType();
        public string GetName() => GetType().Name;
    }

    public class EventDispatcher
    {
        private readonly Event _event;
        public delegate bool EventHandler<in T>(T e);

        public EventDispatcher(Event e)
        {
            _event = e;
        }

        public bool Dispatch<T>(EventHandler<T> func) where T : Event
        {
            if (_event.GetEventType != typeof(T)) return false;

            _event.Handled |= func.Invoke((T)_event);
            
            return true;
        }
    }
}
