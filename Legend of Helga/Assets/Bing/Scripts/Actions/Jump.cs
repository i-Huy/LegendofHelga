using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerControl.Actions
{
    public class Jump : MovementActionHandler<EmptyContext>
    {
        public Jump(MovementController mvctr) : base(mvctr)
        {

        }

        public override bool CanStartAction(PlayerController controller)
        {
            return mvController.canJump &&
                controller.canAction &&
                controller.onGround;

        }

        protected override void _StartAction(
            PlayerController controller, EmptyContext ctx)
        {
            mvController.currState = PlayerState.Jump;
        }

        public override bool IsActive()
        {
            return mvController.currState != null &&
                (PlayerState)mvController.currState == PlayerState.Jump;
        }
    }
}
