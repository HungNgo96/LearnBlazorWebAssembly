using Microsoft.Extensions.DependencyInjection;
using System;

namespace ApplicationClient.Ultilities
{
    public class LazyInstanceUtils<T> : Lazy<T>
    {
        public LazyInstanceUtils(IServiceProvider serviceProvider)
       : base(() => serviceProvider.GetRequiredService<T>())
        {
        }
    }
}