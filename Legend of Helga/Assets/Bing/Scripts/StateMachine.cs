using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    protected float enterTime;
    private Dictionary<Enum, Dictionary<string, System.Delegate>> _cache =
        new Dictionary<Enum, Dictionary<string, System.Delegate>>();

    private static void DoNothing() { }
    protected virtual void BeforeStateUpdate() { }
    protected virtual void AfterStateUpdate() { }

    public class State
    {
        public Enum currState;

        public System.Action DoStateUpdate = DoNothing;
        public System.Action EnterState = DoNothing;
        public System.Action ExitState = DoNothing;
    }

    public State state = new State();
    public Enum lastState;
    public Enum currState
    {
        get { return state.currState; }
        set
        {
            if (state.currState == value) { return; }
            BookKeeping();
            state.currState = value;
            ConfigureNewState();
        }
    }

    private void BookKeeping()
    {
        lastState = state.currState;
        enterTime = Time.time;
    }

    private void ConfigureNewState()
    {
        if (state.ExitState != null) { state.ExitState(); }

        state.DoStateUpdate = ConfigureDelegate<System.Action>(
            "DoUpdate", DoNothing);
        state.EnterState = ConfigureDelegate<System.Action>(
            "EnterState", DoNothing);
        state.ExitState = ConfigureDelegate<System.Action>(
            "ExitState", DoNothing);

        state.EnterState();
    }

    private T ConfigureDelegate<T>(string methodName, T Default) where T : class
    {
        if (!_cache.TryGetValue(
            state.currState, out Dictionary<string, Delegate> methodDict))
        {
            methodDict = new Dictionary<string, Delegate>();
            _cache[state.currState] = methodDict;
        }

        if (!methodDict.TryGetValue(methodName, out Delegate rtval))
        {
            System.Reflection.MethodInfo methodInfo = GetType().GetMethod(
                state.currState.ToString() + "_" + methodName,
                System.Reflection.BindingFlags.InvokeMethod |
                System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance
            );

            if (methodInfo != null)
            {
                rtval = Delegate.CreateDelegate(typeof(T), this, methodInfo);
            }
            else { rtval = Default as Delegate; }

            methodDict[methodName] = rtval;
        }

        return rtval as T;
    }

    protected void CallUpdate()
    {
        BeforeStateUpdate();
        state.DoStateUpdate();
        AfterStateUpdate();
    }
}
