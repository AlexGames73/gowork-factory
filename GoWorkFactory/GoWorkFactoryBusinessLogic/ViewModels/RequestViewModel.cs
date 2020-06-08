using System;
using System.Collections.Generic;

namespace GoWorkFactoryBusinessLogic.ViewModels
{
    public class RequestViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public Dictionary<int, (string, int, int)> Materials { get; set; }
    }
}
