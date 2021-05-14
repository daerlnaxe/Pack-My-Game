using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using System.Windows.Markup;

namespace Pack_My_Game.Language
{
    public class PathExtension : MarkupExtension
    {
        [ConstructorArgument("ReplacementKey")]
        public string ReplacementKey { get; set; }

        [ConstructorArgument("Converter")]
        public IMultiValueConverter Converter { get; set; }

        [ConstructorArgument("Binding")]
        public Binding Property { get; set; }

        public PathExtension()
        {

        }


        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (string.IsNullOrEmpty(ReplacementKey))
                throw new InvalidOperationException("The key property cannot be null or empty");


            // --- On crée une nouvelle liaison autonome
            var multiBinding = new MultiBinding()
            {
                Converter = Converter,
            };
            multiBinding.Bindings.Add(Property);
            multiBinding.Bindings.Add
                (
                    new Binding("Value")
                    {
                        Source = new DataModule(ReplacementKey),
                    }
                );

            return multiBinding.ProvideValue(serviceProvider);
        }

    }
}
