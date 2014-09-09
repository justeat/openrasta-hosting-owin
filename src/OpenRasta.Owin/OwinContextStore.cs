using System.Collections;
using System.Collections.Generic;
using OpenRasta.Pipeline;

namespace OpenRasta.Owin
{
    public class OwinContextStore : IContextStore
    {
        private readonly IDictionary store;

        public OwinContextStore()
        {
            store = new Dictionary<string, object>();
        }

        public object this[string key]
        {
            get { return store[key]; }
            set { store[key] = value; }
        }
    }
}