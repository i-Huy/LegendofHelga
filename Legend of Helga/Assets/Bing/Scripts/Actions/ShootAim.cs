using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerControl.Actions
{
    public class ShootAim : BaseHandler<EmptyContext>
    {
        public override bool CanStartAction(PlayerController controller)
        {
            return !actionActive && controller.canAction &&
                controller.weaponNbr == 2;
        }

        public override bool CanEndAction(PlayerController controller)
        {
            return actionActive;
        }

        protected override void _StartAction(
            PlayerController controller, EmptyContext ctx)
        {
            controller.ShootAim();
        }

        protected override void _EndAction(PlayerController controller)
        {
            EndAction(controller);
        }
    }
}
