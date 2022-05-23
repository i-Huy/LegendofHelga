using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PlayerControl.Actions
{
    public abstract class InstantActionHandler<T> : BaseHandler<T>
    {
        public override void StartAction(
            PlayerController controller, object ctx)
        {
            base.StartAction(controller, ctx);
            base.EndAction(controller);
        }

        public override bool IsActive()
        {
            return false;
        }

        public override bool CanEndAction(PlayerController controller)
        {
            return true;
        }

        protected override void _EndAction(PlayerController controller)
        {

        }
    }
}
