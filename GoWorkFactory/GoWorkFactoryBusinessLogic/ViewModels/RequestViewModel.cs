using System;
using System.Collections.Generic;
using System.Text;

namespace GoWorkFactoryBusinessLogic.ViewModels
{
    public class RequestViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public Dictionary<int, (int, int)> Materials { get; set; }
    }
}
