using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerControl.Actions
{
    public class Fall : MovementActionHandler<EmptyContext>
    {
        public Fall(MovementController mvctr) : base(mvctr)
        {

        }

        public override bool CanStartAction(PlayerController controller)
        {
            return !controller.onGround;
        }

        protected override void _StartAction(
            PlayerController controller, EmptyContext ctx)
        {
            mvController.currState = PlayerState.Fall;
        }

        public override bool IsActive()
        {
            return mvController.currState != null &&
                (PlayerState)mvController.currState == PlayerState.Fall;
        }
    }
}
