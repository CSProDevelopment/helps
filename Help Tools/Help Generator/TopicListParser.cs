using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Help_Generator
{
    public class TopicListParser
    {
        public class TopicListNode
        {
            public Preprocessor.TopicPreprocessor Topic;
            public string Title;
            public bool TitleSpecified;
            public TopicListNode ChildNode;
            public TopicListNode SiblingNode;

            public TopicListNode(Preprocessor.TopicPreprocessor topic,string title)
            {
                Topic = topic;
                TitleSpecified = ( title != null );
                Title = TitleSpecified ? title : topic.Title;
                ChildNode = null;
                SiblingNode = null;
            }
        }

        private bool _indexParser;

        public TopicListParser(bool indexParser)
        {
            _indexParser = indexParser;
        }

        public TopicListNode Parse(string[] lines,Preprocessor preprocessor)
        {
            string fileDescription = _indexParser ? "index" : "table of contents";

            TopicListNode rootNode = null;
            List<TopicListNode> parentNodes = new List<TopicListNode>();

            for( int i = 0; i < lines.Length; i++ )
            {
                string line = lines[i];

                int zeroBasedLevel = 0; // the level is zero-based (but displayed as one-based)

                for( ; zeroBasedLevel < line.Length && line[zeroBasedLevel] == '\t'; zeroBasedLevel++ )
                    ;

                line = line.Trim();

                int hashPos = line.IndexOf('#');

                if( hashPos >= 0 ) // a comment
                    line = line.Substring(0,hashPos).Trim();

                if( line.Length == 0 ) // an empty line
                    continue;

                try
                {
                    if( parentNodes.Count == 0 && zeroBasedLevel >= 1 )
                        throw new Exception("must be at level 1 because it is the first node");

                    if( _indexParser && zeroBasedLevel >= 2 )
                        throw new Exception(String.Format("has more than the maximum allowed levels ({0} > 2)",zeroBasedLevel + 1));

                    if( zeroBasedLevel > parentNodes.Count )
                        throw new Exception(String.Format("cannot increase more than one level from {0} to {1}",parentNodes.Count,zeroBasedLevel + 1));

                    int pipePos = line.IndexOf('|');

                    string filename = ( pipePos >= 0 ) ? line.Substring(0,pipePos).Trim() : line;
                    string title = ( pipePos >= 0 ) ? line.Substring(pipePos + 1).Trim() : null;

                    if( title != null && String.IsNullOrWhiteSpace(title) )
                        throw new Exception("has a blank title");

                    Preprocessor.TopicPreprocessor topic = String.IsNullOrWhiteSpace(filename) ? null : preprocessor.GetTopic(filename);
                    bool isLinkToChm = ( title != null && GetLinkToChm(title) != null );

                    if( isLinkToChm && zeroBasedLevel > 0 )
                        throw new Exception("is invalid because links to other help files must only be at level one");

                    if( _indexParser ) // index
                    {
                        // an index entry must be either a topic or a link to a help file
                        if( topic == null && !isLinkToChm )
                            throw new Exception("must specify a topic filename");
                    }

                    else // table of contents
                    {
                        if( rootNode == null && topic != null )
                            throw new Exception("must be a chapter because it is the first node");
                    }
                    
                    TopicListNode newNode = new TopicListNode(topic,title);

                    if( rootNode == null ) // the root node
                    {
                        rootNode = newNode;
                        parentNodes.Add(rootNode);
                    }

                    else if( zeroBasedLevel == parentNodes.Count ) // child node
                    {
                        if( !_indexParser && parentNodes[zeroBasedLevel - 1].Topic != null )
                            throw new Exception("is invalid because nothing can come at a level under a topic");

                        parentNodes[zeroBasedLevel - 1].ChildNode = newNode;
                        parentNodes.Add(newNode);                        
                    }

                    else // sibling node (at the same or previous level)
                    {
                        while( parentNodes.Count > ( zeroBasedLevel + 1 ) )
                            parentNodes.RemoveAt(parentNodes.Count - 1);

                        if( !_indexParser && newNode.Topic != null && parentNodes[zeroBasedLevel].Topic == null )
                            throw new Exception("is invalid because a topic cannot come after a chapter at the same level");

                        parentNodes[zeroBasedLevel].SiblingNode = newNode;
                        parentNodes[zeroBasedLevel] = newNode; 
                    }
                }

                catch( Exception exception )
                {
                    throw new Exception(String.Format("The {0} line #{1} \"{2}\" {3}",fileDescription,i + 1,line,exception.Message));
                }
            }

            if( rootNode == null )
                throw new Exception(String.Format("The {0} does not have any entries",fileDescription));

            // make sure that every topic is included
            HashSet<Preprocessor.TopicPreprocessor> allTopics = preprocessor.GetAllTopics();

            RemoveTopicFilename(rootNode,allTopics);

            if( allTopics.Count > 0 )
            {
                StringBuilder sb = new StringBuilder();

                foreach( Preprocessor.TopicPreprocessor topic in allTopics )
                    sb.AppendLine(Path.GetFileName(topic.Filename));

                throw new Exception(String.Format("The {0} is missing entries for the topics:\r\n\r\n{1}",fileDescription,sb.ToString()));
            }

            return rootNode;
        }

        private void RemoveTopicFilename(TopicListNode node,HashSet<Preprocessor.TopicPreprocessor> allTopics)
        {
            while( node != null )
            {
                if( node.Topic != null )
                    allTopics.Remove(node.Topic);

                if( node.ChildNode != null )
                    RemoveTopicFilename(node.ChildNode,allTopics);

                node = node.SiblingNode;
            }
        }

        public static string GetLinkToChm(string title)
        {
            if( title.IndexOf("|") == 0 )
            {
                string filename = title.Substring(1).Trim();

                if( Path.GetExtension(filename).Equals(Constants.ChmFileExtension,StringComparison.InvariantCultureIgnoreCase) )
                    return filename;
            }

            return null;
        }


        private class FormatterLine : IComparable<FormatterLine>
        {
            public string Line;
            public string SortableKey;

            public int CompareTo(FormatterLine compareFormatterLine)
            {
                return SortableKey.CompareTo(compareFormatterLine.SortableKey);
            }
        }

        public static string Format(TopicListNode node,bool sort)
        {
            List<FormatterLine> formatterLines = new List<FormatterLine>();

            FormatNode(node,0,formatterLines,"");

            if( sort )
                formatterLines.Sort();

            StringBuilder sb = new StringBuilder();

            foreach( FormatterLine formatterLine in formatterLines )
                sb.AppendLine(formatterLine.Line);

            return sb.ToString();
        }

        private static void FormatNode(TopicListNode node,int level,List<FormatterLine> formatterLines,string sortableKeyPrefix)
        {
            const int CommentColumn = 80;
            const int SpacesInTab = 4;

            StringBuilder sb = new StringBuilder();
            StringBuilder sbSortable = new StringBuilder();

            while( node != null )
            {                
                sb.Clear();
                sbSortable.Clear();

                sb.Append('\t',level);

                bool isLinkToChm = false;

                if( node.Topic == null )
                {
                    sb.Append("|" + node.Title);
                    isLinkToChm = ( GetLinkToChm(node.Title) != null );
                }

                else
                {
                    sb.Append(Path.GetFileName(node.Topic.Filename));

                    if( node.TitleSpecified && !node.Topic.Title.Equals(node.Title) )
                        sb.Append(" | " + node.Title);
                }

                int lineLength = sb.Length + level * ( SpacesInTab - 1 );
                int spacingTabs = ( CommentColumn - lineLength ) / SpacesInTab;

                if( ( CommentColumn - lineLength ) % SpacesInTab == 0 )
                    spacingTabs--;

                if( spacingTabs > 0 )
                    sb.Append('\t',spacingTabs);

                string title =  isLinkToChm ? String.Format("<Link to help file {0}>",GetLinkToChm(node.Title)) :
                                node.TitleSpecified ? node.Title :
                                node.Topic.Title;

                sbSortable.Append('#');
                sbSortable.Append('\t',level + 1);
                sbSortable.Append(title);

                sb.Append(sbSortable);

                FormatterLine formatterLine = new FormatterLine();
                formatterLine.Line = sb.ToString();
                formatterLine.SortableKey = sortableKeyPrefix + sbSortable.ToString();

                // make sure that spaces are treated as high characters
                formatterLine.SortableKey = formatterLine.SortableKey.Replace(' ','\u26ff');

                if( isLinkToChm ) // insert a high character so that linked help files show at the end of a sorted index
                    formatterLine.SortableKey = '\u26ff' + formatterLine.SortableKey;                    

                formatterLines.Add(formatterLine);

                if( node.ChildNode != null )
                    FormatNode(node.ChildNode,level + 1,formatterLines,formatterLine.SortableKey);

                node = node.SiblingNode;
            }
        }
    }
}
