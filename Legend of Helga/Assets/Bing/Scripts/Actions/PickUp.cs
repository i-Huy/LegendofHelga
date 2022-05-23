using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerControl.Actions
{
    public class PickUp: InstantActionHandler<EmptyContext>
    {

        public override bool CanStartAction(PlayerController controller)
        {
            return true;
        }

        protected override void _StartAction(
            PlayerController controller, EmptyContext ctx)
        {
            controller.PickUp();
        }

        public override bool IsActive()
        {
            return false;
        }
    }
}
