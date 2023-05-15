using Microsoft.Extensions.DependencyInjection;
using System;

namespace Application.Ultilities
{
    public class LazyInstanceUtils<T> : Lazy<T>
    {
        public LazyInstanceUtils(IServiceProvider serviceProvider)
       : base(() => serviceProvider.GetRequiredService<T>())
        {
        }
    }
}