using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PlayerControl.Actions
{
    public interface PlayerActionHandler
    {
        bool CanStartAction(PlayerController controller);
        void StartAction(PlayerController controller, object ctx);
        void AddStartListener(Action callback);
        void RemoveStartListener(Action callback);

        bool CanEndAction(PlayerController controller);
        void EndAction(PlayerController controller);
        void AddEndListener(Action callback);
        void RemoveEndListener(Action callback);

        bool IsActive();
    }

    public class EmptyContext
    {
    }

    public abstract class BaseHandler<T> : PlayerActionHandler
    {
        private event Action OnStart = delegate { };
        private event Action OnEnd = delegate { };

        public bool actionActive;

        public abstract bool CanStartAction(PlayerController controller);
        public abstract bool CanEndAction(PlayerController controller);
        protected abstract void _StartAction(
            PlayerController controller, T ctx);
        protected abstract void _EndAction(PlayerController controller);

        public virtual void StartAction(PlayerController controller, object ctx)
        {
            if (CanStartAction(controller))
            {
                actionActive = true;
                _StartAction(controller, (T)ctx);
                OnStart();
            }
        }

        public virtual void AddStartListener(Action callback)
        {
            OnStart += callback;
        }

        public virtual void RemoveStartListener(Action callback)
        {
            OnStart -= callback;
        }

        public virtual void EndAction(PlayerController controller)
        {
            if (CanEndAction(controller))
            {
                actionActive = false;
                _EndAction(controller);
                OnEnd();
            }
        }

        public virtual void AddEndListener(Action callback)
        {
            OnEnd += callback;
        }

        public virtual void RemoveEndListener(Action callback)
        {
            OnEnd -= callback;
        }

        public virtual bool IsActive()
        {
            return actionActive;
        }
    }
}
