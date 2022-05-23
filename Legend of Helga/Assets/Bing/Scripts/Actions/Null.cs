using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerControl.Actions
{
    public class Null : InstantActionHandler<EmptyContext>
    {
        public override bool CanStartAction(PlayerController controller)
        {
            return false;
        }

        protected override void _StartAction(
            PlayerController controller, EmptyContext ctx)
        {

        }
    }
}
