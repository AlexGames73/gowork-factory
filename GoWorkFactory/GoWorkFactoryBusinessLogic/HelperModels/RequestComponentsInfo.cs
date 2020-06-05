using System;
using System.Collections.Generic;
using System.Text;

namespace GoWorkFactoryBusinessLogic.HelperModels
{
    public class RequestComponentsInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<(string, int)> Materials { get; set; }
    }
}
