using System.Collections.Generic;

namespace GoWorkFactoryBusinessLogic.HelperModels
{
    public class RequestMaterialsInfo
    {
        public string Title { get; set; }
        public List<(string, int)> Materials { get; set; }
    }
}
