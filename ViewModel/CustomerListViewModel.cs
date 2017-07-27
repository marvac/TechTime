using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTime.Models;

namespace TechTime.ViewModel
{
    public class CustomerListViewModel
    {
        public string Value { get; set; }
        public string Text { get; set; }

        public static string GetCustomerString()
        {
            List<CustomerListViewModel> customers = new List<CustomerListViewModel>();

            customers.Add(new CustomerListViewModel { Text = "qweqeqeqeqe", Value = "qweqeqeqeqe" });
            customers.Add(new CustomerListViewModel { Text = "trhrhrthrthrh", Value = "trhrhrthrthrh" });
            customers.Add(new CustomerListViewModel { Text = "rthrthtrhtrt", Value = "rthrthtrhtrt" });
            customers.Add(new CustomerListViewModel { Text = "sadasdsaf", Value = "sadasdsaf" });
            customers.Add(new CustomerListViewModel { Text = "fghfghfgh", Value = "fghfghfgh" });

            return JsonConvert.SerializeObject(customers, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }

        public static List<Customer> GetCustomerList()
        {
            List<Customer> customers = new List<Customer>();

            customers.Add(new Customer { Name = "Herpaderpins", CustomerId = "123133213" });
            customers.Add(new Customer { Name = "Jerpaderps", CustomerId = "12121212" });
            customers.Add(new Customer { Name = "Herflerpa", CustomerId = "323232323" });
            customers.Add(new Customer { Name = "Hi234ef", CustomerId = "5656464" });
            customers.Add(new Customer { Name = "fhfghfhfh", CustomerId = "7787887" });

            return customers;
        }
    }
}
