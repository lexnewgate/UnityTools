using System;

namespace Lex.UnityTools
{
    /// <summary>
    /// https://csharpindepth.com/articles/singleton
    /// </summary>
    public sealed class SingletonTemplate
    {
        private static readonly Lazy<SingletonTemplate> s_lazy =
            new Lazy<SingletonTemplate>(() => new SingletonTemplate());

        public static SingletonTemplate Instance { get { return s_lazy.Value; } }

        private SingletonTemplate()
        {
        }
    }
}