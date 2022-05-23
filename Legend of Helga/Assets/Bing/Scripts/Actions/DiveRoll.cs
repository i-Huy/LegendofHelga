using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerControl.Actions
{
    public class DiveRoll : MovementActionHandler<int>
    {
        public DiveRoll(MovementController mvctr) : base(mvctr)
        {

        }

        public override bool CanStartAction(PlayerController controller)
        {
            return controller.canAction && controller.onGround;
        }

        protected override void _StartAction(
            PlayerController controller, int ctx)
        {
            controller.DiveRoll(ctx);
            mvController.currState = PlayerState.DiveRoll;
        }

        public override bool IsActive()
        {
            return mvController.currState != null &&
                (PlayerState)mvController.currState == PlayerState.DiveRoll;
        }
    }
}
