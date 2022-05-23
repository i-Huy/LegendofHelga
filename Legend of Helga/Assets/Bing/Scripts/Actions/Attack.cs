using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerControl.Actions
{
    public class AttackContext
    {
        public string type;
        public int atkSide;
        public int atkNbr;

        public AttackContext(string type, int side, int nbr)
        {
            this.type = type;
            this.atkSide = side;
            this.atkNbr = nbr;
        }

        public AttackContext(string type, string side, int nbr)
        {
            this.type = type;
            this.atkNbr = nbr;

            switch (side.ToLower())
            {
                case "none":
                    this.atkSide = (int)AttackSide.None;
                    break;
                case "left":
                    this.atkSide = (int)AttackSide.Left;
                    break;
                case "right":
                    this.atkSide = (int)AttackSide.Right;
                    break;
            }
        }
    }

    public class Attack : BaseHandler<AttackContext>
    {
        public override bool CanStartAction(PlayerController controller)
        {
            return !actionActive && controller.canAction;
        }

        public override bool CanEndAction(PlayerController controller)
        {
            return actionActive;
        }

        protected override void _StartAction(
            PlayerController controller, AttackContext ctx)
        {
            int atkSide = (int)ctx.atkSide;
            int atkNbr = ctx.atkNbr;
            float duration = 0f;

            if (atkNbr == -1)
            {
                atkNbr = AnimationData.RandomAttackNbr(atkSide);
            }

            duration = AnimationData.AttackDuration();

            if (controller.isMoving)
            {
                controller.MoveAttack(atkNbr, duration);
            }
            else
            {
                controller.Attack(atkNbr, duration);
            }
            EndAction(controller);
        }

        protected override void _EndAction(PlayerController controller)
        {

        }
    }
}
