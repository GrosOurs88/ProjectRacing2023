using UnityEngine;

namespace KartGame.KartSystems 
{
    public class KeyboardInput : BaseInput
    {
        public string TurnInputName = "Horizontal";
        public string AccelerateButtonName = "Accelerate";
        public string BrakeButtonName = "Brake";

        //Controller Inputs
        public string AccelerateControllerButtonName = "AccelerateController";
        public string BrakeControllerButtonName = "BrakeController";

        public override InputData GenerateInput() 
        {
            return new InputData
            {
                Accelerate = Input.GetButton(AccelerateButtonName),
                Brake = Input.GetButton(BrakeButtonName),
                TurnInput = Input.GetAxis("Horizontal"),

                //Controller inputs
                AccelerateController = Input.GetAxis("AccelerateController"),
                BrakeController = Input.GetAxis("BrakeController")
            };
        }
    }
}
