﻿using System.ComponentModel;

namespace GoWorkFactoryBusinessLogic.ViewModels
{
    public class MaterialViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название материала")]
        public string NameMaterial { get; set; }

        [DisplayName("Количество материала")]
        public int CountMaterial { get; set; }
    }
}
