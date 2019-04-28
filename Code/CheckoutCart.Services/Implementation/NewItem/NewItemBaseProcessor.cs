using System.Linq;
using CheckoutCart.DataContract.RequestEntities;
using CheckoutCart.DataContract.ResponseEntities;
using CheckoutCart.Services.Config;

namespace CheckoutCart.Services.Implementation.NewItem
{
    /// <summary>
    ///     Base processor for Adding new Item to shopping cart
    /// </summary>
    public abstract class NewItemBaseProcessor
    {
        private NewItemBaseProcessor _nextStep;

        private static StepsConfig Steps => Helper.DeserializeJsonFile<StepsConfig>("./Config/NewItemStepsConfig.json");

        protected NewItemBaseProcessor()
        {
            SetNextStep();
        }

        /// <summary>
        ///     Sets the next step for the current step
        /// </summary>
        private void SetNextStep()
        {
            var nextStepAssembly = Steps.Steps.FirstOrDefault(s => s.From == GetType().FullName)?.To;

            _nextStep = Helper.GetNextStep<NewItemBaseProcessor>(nextStepAssembly);
        }

        /// <summary>
        ///     Process the next step of the current step if any
        /// </summary>
        /// <param name="cartItem"></param>
        /// <returns></returns>
        public virtual ShoppingCartResponse Process(CartItemEntity cartItem)
        {
            return _nextStep?.Process(cartItem);
        }
    }
}