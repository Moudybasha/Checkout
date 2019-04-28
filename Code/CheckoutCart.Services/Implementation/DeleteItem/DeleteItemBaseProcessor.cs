using System.Linq;
using CheckoutCart.DataContract.RequestEntities;
using CheckoutCart.Services.Config;

namespace CheckoutCart.Services.Implementation.DeleteItem
{
    /// <summary>
    ///     Base class for Delete cart item orchestrator
    /// </summary>
    public abstract class DeleteItemBaseProcessor
    {
        private DeleteItemBaseProcessor _nextStep;

        private static StepsConfig Steps =>
            Helper.DeserializeJsonFile<StepsConfig>("./Config/DeleteItemStepsConfig.json");

        protected DeleteItemBaseProcessor()
        {
            SetNextStep();
        }


        /// <summary>
        ///     Set next step of the current step based on the config file
        /// </summary>
        private void SetNextStep()
        {
            var nextStepAssembly = Steps.Steps.FirstOrDefault(s => s.From == GetType().FullName)?.To;

            _nextStep = Helper.GetNextStep<DeleteItemBaseProcessor>(nextStepAssembly);
        }

        /// <summary>
        ///     Process next step if any
        /// </summary>
        /// <param name="cartItem"></param>
        public virtual void Process(CartItemUpdateEntity cartItem)
        {
            _nextStep?.Process(cartItem);
        }
    }
}