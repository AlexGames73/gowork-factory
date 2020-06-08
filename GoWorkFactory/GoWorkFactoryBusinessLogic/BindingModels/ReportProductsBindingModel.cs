using System;

namespace GoWorkFactoryBusinessLogic.BindingModels
{
    public class ReportProductsBindingModel
    {
        public int UserId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
