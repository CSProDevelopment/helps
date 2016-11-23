using System;
using System.Collections.Generic;

namespace Help_Generator
{
    class AttributesParser
    {
        public class AttributeType
        {
            public string Name;
            public bool Required;
            public bool MultipleAllowed;

            public AttributeType(string name,bool required,bool multipleAllowed)
            {
                Name = name.ToUpper();
                Required = required;
                MultipleAllowed = multipleAllowed;
            }
        }

        private Dictionary<string,AttributeType> _attributeTypesMap;
        private Dictionary<string,List<string>> _parsedAttributesMap;

        public AttributesParser(AttributeType[] attributeTypes)
        {
            _attributeTypesMap = new Dictionary<string,AttributeType>();
            _parsedAttributesMap = null;

            if( attributeTypes != null )
            {
                foreach( AttributeType attributeType in attributeTypes )
                    _attributeTypesMap.Add(attributeType.Name,attributeType);
            }
        }

        public void Parse(string[] lines)
        {
            bool checkAgainstSpecification = ( _attributeTypesMap.Count > 0 );
            _parsedAttributesMap = new Dictionary<string,List<string>>();

            for( int i = 0; i < lines.Length; i++ )
            {
                string line = lines[i].Trim();

                if( line.Length == 0 || line[0] == '#' ) // an empty line or a comment
                    continue;

                try
                {
                    int equalsPos = line.IndexOf('=');

                    if( equalsPos < 0 )
                        throw new Exception("does not contain a '='");

                    string attribute = line.Substring(0,equalsPos).Trim().ToUpper();

                    if( attribute.Length == 0 )
                        throw new Exception("does not contain an attribute");

                    string value = line.Substring(equalsPos + 1). Trim();

                    if(  value.Length == 0 )
                        throw new Exception("does not contain a value");

                    bool multipleAttributesAllowed = false;

                    if( checkAgainstSpecification )
                    {
                        if( _attributeTypesMap.ContainsKey(attribute) )
                            multipleAttributesAllowed = _attributeTypesMap[attribute].MultipleAllowed;

                        else
                            throw new Exception("cannot defined the unknown attribute " + attribute);
                    }

                    if( _parsedAttributesMap.ContainsKey(attribute) )
                    {
                        if( !multipleAttributesAllowed )
                            throw new Exception("cannot define more than one " + attribute);
                    }

                    else
                        _parsedAttributesMap.Add(attribute,new List<string>());

                    _parsedAttributesMap[attribute].Add(value);
                }

                catch( Exception exception )
                {
                    throw new Exception(String.Format("The attributes line #{0} \"{1}\" {2}",i+ 1,line,exception.Message));
                }
            }

            // make sure all required attributes are defined
            if( checkAgainstSpecification )
            {
                foreach( var kp in _attributeTypesMap )
                {
                    if( kp.Value.Required && !_parsedAttributesMap.ContainsKey(kp.Key) )
                        throw new Exception(String.Format("The required attribute {0} was not defined",kp.Key));
                }
            }
        }

        public string GetSingleValue(string attribute)
        {
            attribute = attribute.ToUpper();
            return _parsedAttributesMap.ContainsKey(attribute) ? _parsedAttributesMap[attribute.ToUpper()][0] : null;
        }

        public List<string> GetValues(string attribute)
        {
            attribute = attribute.ToUpper();
            return _parsedAttributesMap.ContainsKey(attribute) ? _parsedAttributesMap[attribute] : new List<string>();
        }

        public Dictionary<string,List<string>> GetPairs()
        {
            return _parsedAttributesMap;
        }
    }
}
