using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Unity;
using Component = Castle.MicroKernel.Registration.Component;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new UnityContainer();
          
       // var tt =  test.Resolve<IPaymentMethod>();
            var container = new WindsorContainer();
            container.Register(
                Component.For<IPaymentService>().ImplementedBy<PaymentService>(),
                Component.For<IPaymentMethodResolver>().ImplementedBy<PaymentMethodResolver>(),
                Classes.FromThisAssembly().IncludeNonPublicTypes().BasedOn<IPaymentMethod>()
                    .WithService.FromInterface()
                
            );
            var xxx = container.Resolve(typeof(IPaymentMethod));


        }
    }
    public interface IPaymentService
    {
        void Pay(string paymentMethodName);
    }

    public class PaymentService : IPaymentService
    {
        private readonly IPaymentMethodResolver _paymentMethodResolver;

        public PaymentService(IPaymentMethodResolver paymentMethodResolver)
        {
            _paymentMethodResolver = paymentMethodResolver;
        }

        public void Pay(string paymentMethodName)
        {
            IPaymentMethod paymentMethod = _paymentMethodResolver.Resolve(paymentMethodName);
             paymentMethod.Process();
        }
    }

    public interface IPaymentMethod
    {
        string Name { get; }
        void Process( );
    }

    public class CashOnDelivery : IPaymentMethod
    {
        public CashOnDelivery()
        {
            Name = "Cash";
        }

        public string Name { get; private set; }

        public void Process( )
        {
            Console.WriteLine("Pay with cash on delivery");
           
        }
    }

    public class CreditCardPayment : IPaymentMethod
    {
        public CreditCardPayment()
        {
            Name = "CreditCard";
        }

        public string Name { get; private set; }

        public void Process( )
        {
            Console.WriteLine("Pay with credit card");
           
        }
    }


    public interface IPaymentMethodResolver
    {
        IPaymentMethod Resolve(string name);
    }

    public class PaymentMethodResolver : IPaymentMethodResolver
    {
        private readonly IEnumerable<IPaymentMethod> _paymentMethods;

        public PaymentMethodResolver(IEnumerable<IPaymentMethod> paymentMethods)
        {
            _paymentMethods = paymentMethods;
        }

        public IPaymentMethod Resolve(string name)
        {
            IPaymentMethod paymentMethod = _paymentMethods.FirstOrDefault(item => item.Name == name);
            if (paymentMethod == null)
            {
                throw new ArgumentException("Payment method not found", name);
            }
            return paymentMethod;
        }
    }
}
