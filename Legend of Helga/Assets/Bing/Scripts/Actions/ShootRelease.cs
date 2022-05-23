using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerControl.Actions
{
    public class ShootRelease : BaseHandler<EmptyContext>
    {
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
            controller.ShootRelease();
        }

        protected override void _EndAction(PlayerController controller)
        {
            // controller.ShootAimCleanup();
        }
    }
}
