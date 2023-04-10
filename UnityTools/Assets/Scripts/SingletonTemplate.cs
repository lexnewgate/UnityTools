using System;

namespace Lex.UnityTools
{
    /// <summary>
    /// https://csharpindepth.com/articles/singleton
    /// </summary>
    public sealed class SingletonTemplate
    {
        private static readonly Lazy<SingletonTemplate> lazy =
            new Lazy<SingletonTemplate>(() => new SingletonTemplate());

        public static SingletonTemplate Instance { get { return lazy.Value; } }

        private SingletonTemplate()
        {
        }
    }
}