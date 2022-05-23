using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerControl.Actions
{
    public class Move : MovementActionHandler<EmptyContext>
    {
        public Move(MovementController mvctr) : base(mvctr)
        {

        }

        public override bool CanStartAction(PlayerController controller)
        {
            return controller.canMove &&
                controller.moveInput != Vector3.zero &&
                controller.onGround;
        }

        protected override void _StartAction(
            PlayerController controller, EmptyContext ctx)
        {
            mvController.currState = PlayerState.Move;
        }

        public override bool IsActive()
        {
            return mvController.currState != null &&
                (PlayerState)mvController.currState == PlayerState.Move;
        }
    }
}
