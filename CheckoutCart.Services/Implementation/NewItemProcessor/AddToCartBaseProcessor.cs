using System.IO;
using System.Linq;
using CheckoutCart.Data.Model.Core.Abstraction;
using CheckoutCart.DataContract;
using CheckoutCart.Services.Config;
using Newtonsoft.Json;

namespace CheckoutCart.Services.Implementation.NewItemProcessor
{
    public abstract class AddToCartBaseProcessor
    {
        protected static IUnitOfWork UnitOfWork;
        protected AddToCartBaseProcessor NextStep;

        
        protected AddToCartBaseProcessor(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public AddToCartBaseProcessor SetNextStep(AddToCartBaseProcessor nextStep)
        {
            NextStep = nextStep;
            return this;
        }

        public virtual void Process(CartItemEntity cartItem)
        {
            NextStep?.Process(cartItem);

        }

        protected void CreateType()
        {
            
            var jsonFile = File.ReadAllText("./Config/NewItemStepsConfig.json");
            var test = JsonConvert.DeserializeObject<StepsConfig>(jsonFile);
            var type = test.Steps.FirstOrDefault(c => c.From == GetType().Name);
            
        }
    }
}
