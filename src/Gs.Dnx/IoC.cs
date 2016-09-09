using System;

namespace Gs.Dnx
{
    public class IoC
    {
        private static IServiceProvider _service;

        public static void SetServiceProvider(IServiceProvider p_service)
        {
            if (p_service == null)
            {
                throw new ArgumentNullException(nameof(p_service));
            }

            _service = p_service;
        }

        public static T Get<T>()
        {
            return (T)GetService(typeof(T));
        }

        public static object GetService(Type p_type)
        {
            return _service.GetService(p_type);
        }
    }
}
