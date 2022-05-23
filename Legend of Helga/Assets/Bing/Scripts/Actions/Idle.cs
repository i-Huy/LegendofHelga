using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerControl.Actions
{
    public class Idle : MovementActionHandler<EmptyContext>
    {
        public Idle(MovementController mvctr) : base(mvctr)
        {

        }

        public override bool CanStartAction(PlayerController controller)
        {
            if (controller.isMoving)
            {
                return controller.moveInput.magnitude < 0.2f;
            }
            return controller.onGround;
        }

        protected override void _StartAction(
            PlayerController controller, EmptyContext ctx)
        {
            mvController.currState = PlayerState.Idle;
        }

        public override bool IsActive()
        {
            return mvController.currState != null &&
                (PlayerState)mvController.currState == PlayerState.Idle;
        }
    }
}
