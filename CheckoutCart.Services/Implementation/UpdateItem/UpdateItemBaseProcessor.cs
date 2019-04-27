using System.Linq;
using Checkout.CrossCutting.Core.ServiceLocator;
using CheckoutCart.Data.Model.Core.Abstraction;
using CheckoutCart.Data.Model.ShoppingCartModels;
using CheckoutCart.DataContract.RequestEntities;
using CheckoutCart.DataContract.ResponseEntities;
using CheckoutCart.Services.Config;

namespace CheckoutCart.Services.Implementation.UpdateItem
{
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

        private void SetNextStep()
        {
            var nextStepAssembly = Steps.Steps.FirstOrDefault(s => s.From == GetType().FullName)?.To;
            _nextStep = Helper.GetNextStep<UpdateItemBaseProcessor>(nextStepAssembly);
        }


        public virtual ShoppingCartResponse Process(CartItemUpdateEntity cartItemUpdate)
        {
            return _nextStep?.Process(cartItemUpdate);
        }
    }
}
