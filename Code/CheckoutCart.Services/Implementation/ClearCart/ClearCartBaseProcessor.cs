using System.Linq;
using CheckoutCart.Services.Config;

namespace CheckoutCart.Services.Implementation.ClearCart
{
    /// <summary>
    ///     Base class for clear cart operation orchestrator
    /// </summary>
    public abstract class ClearCartBaseProcessor
    {
        private ClearCartBaseProcessor _nextStep;

        private static StepsConfig Steps =>
            Helper.DeserializeJsonFile<StepsConfig>("./Config/ClearCartStepsConfig.json");

        protected ClearCartBaseProcessor()
        {
            SetNextStep();
        }

        /// <summary>
        ///     Sets the next step from the config file
        /// </summary>
        private void SetNextStep()
        {
            var nextStepAssembly = Steps.Steps.FirstOrDefault(s => s.From == GetType().FullName)?.To;

            _nextStep = Helper.GetNextStep<ClearCartBaseProcessor>(nextStepAssembly);
        }

        /// <summary>
        ///     Process the next step if any
        /// </summary>
        /// <param name="userId"></param>
        public virtual void Process(long userId)
        {
            _nextStep?.Process(userId);
        }
    }
}