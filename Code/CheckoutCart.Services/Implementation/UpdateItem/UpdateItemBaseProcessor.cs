using System.Linq;
using CheckoutCart.Data.Model.ShoppingCartModels;
using CheckoutCart.DataContract.RequestEntities;
using CheckoutCart.DataContract.ResponseEntities;
using CheckoutCart.Services.Config;

namespace CheckoutCart.Services.Implementation.UpdateItem
{
    /// <summary>
    ///     Base class for updating cart item processors
    /// </summary>
    public abstract class UpdateItemBaseProcessor
    {
        protected static CartItem ExistingCartItem { get; set; }
        private UpdateItemBaseProcessor _nextStep;

        private static StepsConfig Steps =>
            Helper.DeserializeJsonFile<StepsConfig>("./Config/UpdateItemStepsConfig.json");

        protected UpdateItemBaseProcessor()
        {
            SetNextStep();
        }

        /// <summary>
        ///     Sets next step for the current step from config
        /// </summary>
        private void SetNextStep()
        {
            var nextStepAssembly = Steps.Steps.FirstOrDefault(s => s.From == GetType().FullName)?.To;
            _nextStep = Helper.GetNextStep<UpdateItemBaseProcessor>(nextStepAssembly);
        }

        /// <summary>
        ///     Process next steps if any
        /// </summary>
        /// <param name="cartItemUpdate"></param>
        /// <returns></returns>
        public virtual ShoppingCartResponse Process(CartItemUpdateEntity cartItemUpdate)
        {
            return _nextStep?.Process(cartItemUpdate);
        }
    }
}