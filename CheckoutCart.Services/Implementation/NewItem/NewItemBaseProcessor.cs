using System.Linq;
using CheckoutCart.DataContract.RequestEntities;
using CheckoutCart.DataContract.ResponseEntities;
using CheckoutCart.Services.Config;

namespace CheckoutCart.Services.Implementation.NewItem
{
    public abstract class NewItemBaseProcessor
    {
        private NewItemBaseProcessor _nextStep;

        private static StepsConfig Steps => Helper.DeserializeJsonFile<StepsConfig>("./Config/NewItemStepsConfig.json");

        protected NewItemBaseProcessor()
        {
            SetNextStep();
        }

        private void SetNextStep()
        {
            var nextStepAssembly = Steps.Steps.FirstOrDefault(s => s.From == GetType().FullName)?.To;

            _nextStep = Helper.GetNextStep<NewItemBaseProcessor>(nextStepAssembly);
        }

        public virtual ShoppingCartResponse Process(CartItemEntity cartItem)
        {
            return _nextStep?.Process(cartItem);
        }
    }
}