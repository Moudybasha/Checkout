using System.Linq;
using CheckoutCart.Data.Model.Core.Abstraction;
using CheckoutCart.Data.Model.ShoppingCartModels;
using CheckoutCart.Services.Config;

namespace CheckoutCart.Services.Implementation.ClearCart
{
    public abstract class ClearCartBaseProcessor
    {
        protected ClearCartBaseProcessor NextStep;
        private static StepsConfig Steps => Helper.DeserializeJsonFile<StepsConfig>("./Config/ClearCartStepsConfig.json");

        protected ClearCartBaseProcessor()
        {
            SetNextStep();;
        }

        private void SetNextStep()
        {
            var nextStepAssembly = Steps.Steps.FirstOrDefault(s => s.From == GetType().FullName)?.To;

            NextStep = Helper.GetNextStep<ClearCartBaseProcessor>(nextStepAssembly);

        }


        public virtual void Process(long userId)
        {
            NextStep?.Process(userId);
        }
    }
}