using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PlayerControl.Actions
{
    public class SimpleActionHandler : BaseHandler<EmptyContext>
    {
        public SimpleActionHandler(Action onStart, Action onEnd)
        {
            AddStartListener(onStart);
            AddEndListener(onEnd);
        }

        public override bool CanStartAction(PlayerController controller)
        {
            return !actionActive;
        }

        public override bool CanEndAction(PlayerController controller)
        {
            return actionActive;
        }

        protected override void _StartAction(
            PlayerController controller, EmptyContext ctx)
        {

        }

        protected override void _EndAction(PlayerController controller)
        {

        }
    }
}
