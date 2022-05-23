using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerControl.Actions
{
    public abstract class MovementActionHandler<T> : InstantActionHandler<T>
    {
        protected MovementController mvController;

        public MovementActionHandler(MovementController mvctrller)
        {
            mvController = mvctrller;
        }
    }
}
