using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace System.Resources
{
    // TODO: Remove after dotnet core 2.0. Missing GetObject on ResourceManager in netstandard < 2.0
    public class ResourceManager
    {
        private readonly Dictionary<string, string> _fullyQualifiedResourceIds;
        public string Namespace { get; }
        private Assembly Assembly { get; }


        public ResourceManager(string @namespace, Assembly assembly)
        {
            Namespace = @namespace;
            Assembly = assembly;
            _fullyQualifiedResourceIds = Assembly.GetManifestResourceNames()
                .ToDictionary<string, string>(x =>
                {
                    var value = BuildRelativeResourceId(x);
                    return value;
                });
        }

        private string BuildRelativeResourceId(string fullyQualifiedResourceId)
        {
            var index = fullyQualifiedResourceId.IndexOf('.', Namespace.Length + 1);
            if (index == -1)
            {
                return fullyQualifiedResourceId.Substring(Namespace.Length + 1);
            }

            return fullyQualifiedResourceId.Substring(Namespace.Length + 1, index - Namespace.Length - 1);
        }

        public string GetString(string resourceId, CultureInfo resourceCulture)
        {
            using (var streamReader = new StreamReader(GetResourceStream(resourceId)))
            {
                return streamReader.ReadToEnd();
            }
        }

        public object GetObject(string resourceId, CultureInfo resourceCulture)
        {
            using (var streamReader = new BinaryReader(GetResourceStream(resourceId)))
            {
                return streamReader.ReadBytes((int) streamReader.BaseStream.Length);
            }
        }

        private Stream GetResourceStream(string resourceId)
        {
            var fullyQualifiedResourceId = _fullyQualifiedResourceIds[resourceId];
            var stream = Assembly.GetManifestResourceStream(fullyQualifiedResourceId);
            return stream;
        }
    }
}