using System;
using System.Collections.Generic;
using System.Linq;

namespace ConnectionMetadataInjector
{
    public class ResolverChain<TInput, TResult>
    {
        private readonly List<Func<TInput, TResult?>> resolvers;
        private readonly TResult defaultValue;
        private readonly IEnumerable<TResult> defaultResolverExpectedValues;
        private readonly List<TResult> additionalExpectedValues;

        public ResolverChain(TResult defaultValue, IEnumerable<TResult> defaultResolverExpectedValues, Func<TInput, TResult?> defaultResolver)
        {
            resolvers = new List<Func<TInput, TResult?>>
            {
                defaultResolver
            };
            additionalExpectedValues = new List<TResult>();

            this.defaultValue = defaultValue;
            this.defaultResolverExpectedValues = defaultResolverExpectedValues;
        }

        public void AddExpectedValue(TResult expectedValue)
        {
            additionalExpectedValues.Add(expectedValue);
        }

        public IEnumerable<TResult> GetExpectedValues()
        {
            return defaultResolverExpectedValues.Concat(additionalExpectedValues).Append(defaultValue);
        }

        public void AddResolver(Func<TInput, TResult?> resolver)
        {
            resolvers.Insert(0, resolver);
        }

        public TResult Resolve(TInput input)
        {
            foreach (Func<TInput, TResult?> resolver in resolvers)
            {
                TResult? result = resolver(input);
                if (result != null)
                {
                    return result;
                }
            }
            return defaultValue;
        }
    }
}
