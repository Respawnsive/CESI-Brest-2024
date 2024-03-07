using KaamelottSampler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaamelottSampler.Datatemplates
{
    public class SaampleTemplateSelector : DataTemplateSelector
    {
        public DataTemplate NormalTemplate { get; set; }
        public DataTemplate ArthurTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var saample = /*(Saample)*/item as Saample;
            if (saample.Character.Contains("Arthur"))
                return ArthurTemplate;
            else
                return NormalTemplate;
        }
    }
}
