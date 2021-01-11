using System;
using System.Collections.Generic;
using System.Linq;

namespace Help_Generator
{
    class ResourceIdSet
    {
        private Dictionary<string, List<int>> _resourceIds = new Dictionary<string, List<int>>();

        public void AddProject(string project_name)
        {
            foreach( var kp in ResourceHeader.ReadIds(project_name) )
            {
                if( !_resourceIds.ContainsKey(kp.Key) )
                    _resourceIds.Add(kp.Key, new List<int>());

                _resourceIds[kp.Key].Add(kp.Value);
            }
        }

        public void CheckContextExists(string context)
        {
            if( !_resourceIds.ContainsKey(context) )
                throw new Exception($"The context {context} is not defined in any of the resource files.");
        }

        public int GetId(string context)
        {
            CheckContextExists(context);
            
            var ids = _resourceIds[context];
            
            if( ids.Count > 1 )
                throw new Exception($"The context {context} is defined in multiple resource files.");

            return ids[0] | ResourceHeader.ResourceTypes.First(x => ( context.IndexOf(x.Prefix) == 0 )).IdIndex;
        }

        public HashSet<string> GetAllContext()
        {
            return _resourceIds.Keys.ToHashSet();
        }
    }
}
