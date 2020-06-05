using System;
using System.Collections.Generic;
using System.Text;

namespace GoWorkFactoryBusinessLogic.HelperModels
{
    public class RequestMaterialsInfo
    {
        public string Title { get; set; }
        public List<(string, int)> Materials { get; set; }
    }
}
