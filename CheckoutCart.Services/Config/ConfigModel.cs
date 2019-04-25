using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CheckoutCart.Services.Config
{
    public class StepsConfig
    {
        [JsonProperty("steps")]
        public Step[] Steps { get; set; }
    }

    public class Step
    {
        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }
    }
}
