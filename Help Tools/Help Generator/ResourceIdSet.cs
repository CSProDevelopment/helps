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
            Action<Dictionary<string, int>> fill_ids = ( ids ) =>
            {
                foreach( var kp in ids )
                {
                    if( !_resourceIds.ContainsKey(kp.Key) )
                        _resourceIds.Add(kp.Key, new List<int>());

                    _resourceIds[kp.Key].Add(kp.Value);
                }
            };

            // if this is the first project, also load the afxres resources
            if( _resourceIds.Count == 0 )
                fill_ids(ResourceHeader.ReadAfxResIds());

            fill_ids(ResourceHeader.ReadIds(project_name));
        }

        public void CheckContextExists(string context)
        {
            if( !_resourceIds.ContainsKey(context) )
                throw new Exception($"The context {context} is not defined in any of the resource files.");
        }

        public int GetId(string context, bool throw_exception_if_multiple_definitions = true)
        {
            CheckContextExists(context);
            
            var ids = _resourceIds[context];
            
            if( throw_exception_if_multiple_definitions && ids.Count > 1 )
                throw new Exception($"The context {context} is defined in multiple resource files.");

            return ( ids[0] | ResourceHeader.ResourceTypes.First(x => ( context.IndexOf(x.Prefix) == 0 )).IdIndex );
        }

        public HashSet<string> GetAllContext()
        {
            return _resourceIds.Keys.ToHashSet();
        }
    }
}
