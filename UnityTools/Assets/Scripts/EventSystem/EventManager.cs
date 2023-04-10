using System;
using System.Collections.Generic;

namespace Lex.UnityTools
{
    // A simple Event System that can be used for remote systems communication
    public static class EventManager
    {
        /// <summary>
        /// key: GameEvent具体的类型 value: 以GameEvent为参数的Action
        /// </summary>
        static readonly Dictionary<Type, Action<GameEvent>> s_events = new Dictionary<Type, Action<GameEvent>>();

        static readonly Dictionary<Delegate, Action<GameEvent>> s_eventLookups =
            new Dictionary<Delegate, Action<GameEvent>>();

        public static void AddListener<T>(Action<T> evt) where T : GameEvent
        {
            if (!s_eventLookups.ContainsKey(evt))
            {
                //这里需要新生成一个Action,否则后面再次添加时会影响原来的Action
                Action<GameEvent> newAction = (e) => evt((T) e);
                s_eventLookups[evt] = newAction;

                if (s_events.TryGetValue(typeof(T), out Action<GameEvent> internalAction))
                    s_events[typeof(T)] = internalAction + newAction;
                else
                    s_events[typeof(T)] = newAction;
            }
        }

        public static void RemoveListener<T>(Action<T> evt) where T : GameEvent
        {
            if (s_eventLookups.TryGetValue(evt, out var action))
            {
                if (s_events.TryGetValue(typeof(T), out var tempAction))
                {
                    tempAction -= action;
                    if (tempAction == null)
                        s_events.Remove(typeof(T));
                    else
                        s_events[typeof(T)] = tempAction;
                }

                s_eventLookups.Remove(evt);
            }
        }

        public static void Broadcast(GameEvent evt)
        {
            if (s_events.TryGetValue(evt.GetType(), out var action))
                action.Invoke(evt);
        }

        public static void Clear()
        {
            s_events.Clear();
            s_eventLookups.Clear();
        }
    }
}