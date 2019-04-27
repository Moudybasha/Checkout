using System.Linq;
using CheckoutCart.DataContract.RequestEntities;
using CheckoutCart.Services.Config;

namespace CheckoutCart.Services.Implementation.DeleteItem
{
    public abstract class DeleteItemBaseProcessor
    {
        private DeleteItemBaseProcessor _nextStep;
        private static StepsConfig Steps => Helper.DeserializeJsonFile<StepsConfig>("./Config/DeleteItemStepsConfig.json");

        protected DeleteItemBaseProcessor()
        {
            SetNextStep();
        }

        private void SetNextStep()
        {
            var nextStepAssembly = Steps.Steps.FirstOrDefault(s => s.From == GetType().FullName)?.To;

            _nextStep = Helper.GetNextStep<DeleteItemBaseProcessor>(nextStepAssembly);
        }


        public virtual void Process(CartItemUpdateEntity cartItem)
        {
            _nextStep?.Process(cartItem);

        }
    }
}
