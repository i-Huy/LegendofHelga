using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerControl
{
    public enum AttackSide
    {
        None = 0,
        Left = 1,
        Right = 2,
    }

    public enum AnimatorTrigger
    {
        JumpTrigger = 1,
        AttackTrigger = 4,
        IdleTrigger = 18,
        DeathTrigger = 20,
        ReviveTrigger = 21,
        DiveRollTrigger = 28
    }

    public class AnimationData
    {
        public static float AttackDuration()
        {
            return 0.75f;
        }

        public static int RandomAttackNbr(int attackSide)
        {
            int start = 1;
            int end = 4;

            switch (attackSide)
            {
                case 0:
                    break;
                case 1:
                    end = 4;
                    break;
                case 2:
                    start = 4;
                    end = 7;
                    break;
            }

            return Random.Range(start, end);
        }
    }
}
