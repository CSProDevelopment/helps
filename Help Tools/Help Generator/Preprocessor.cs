using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Help_Generator
{
    [Serializable]
    public class Preprocessor
    {
        [Serializable]
        public class TopicPreprocessor
        {
            private string _title;
            public string Title { get { return _title == null ? Path.GetFileName(Filename) : _title; } set { _title = value; } }

            public string Filename { get; set; }
            public DateTime ModifiedDate { get; set; }

            public TopicPreprocessor(FileInfo fi)
            {
                Filename = fi.FullName;
                ModifiedDate = fi.LastWriteTimeUtc;

                try
                {
                    Topic topic = new Topic(fi.FullName);
                    Title = topic.Title;
                }

                catch( Exception )
                {
                    Title = null;
                }
            }
        }

        [Serializable]
        public class ImagePreprocessor
        {
            public string Filename { get; set; }

            public ImagePreprocessor(FileInfo fi)
            {
                Filename = fi.FullName;
            }
        }

        private string _projectPath;
        private string _preprocessorFilename;
        private Dictionary<string,TopicPreprocessor> _topics;
        private Dictionary<string,ImagePreprocessor> _images;

        private Preprocessor()
        {
        }

        public static Preprocessor Create(string projectPath)
        {
            Preprocessor preprocessor = null;
            string preprocessorFilename = Path.Combine(projectPath,Constants.PreprocessorFilename);

            if( File.Exists(preprocessorFilename) )
            {
                try
                {
                    using( Stream stream = new FileStream(preprocessorFilename,FileMode.Open,FileAccess.Read,FileShare.Read) )
                        preprocessor = (Preprocessor)new BinaryFormatter().Deserialize(stream);
                }

                catch( Exception exception )
                {
                    MessageBox.Show("The preprocessor file was corrupt and will be regenerated: " + exception.Message);
                }
            }

            if( preprocessor == null )
            {
                preprocessor = new Preprocessor();
                preprocessor._topics = new Dictionary<string,TopicPreprocessor>();
                preprocessor._images = new Dictionary<string,ImagePreprocessor>();
            }

            preprocessor._projectPath = projectPath;
            preprocessor._preprocessorFilename = preprocessorFilename;

            return preprocessor;
        }

        public bool Refresh()
        {
            bool savePreprocessor = false;

            // remove any deleted topics
            List<string> deletedTopics = new List<string>();

            foreach( var kp in _topics )
            {
                if( !File.Exists(kp.Value.Filename) )
                {
                    deletedTopics.Add(kp.Key);
                    savePreprocessor = true;
                }
            }

            foreach( string name in deletedTopics )
                _topics.Remove(name);

            // process the topics folder
            string topicsPath = Path.Combine(_projectPath,Constants.TopicsDirectoryName);

            if( Directory.Exists(topicsPath) )
            {
                DirectoryInfo diTopics = new DirectoryInfo(topicsPath);
                HashSet<string> topicNames = new HashSet<string>();

                foreach( FileInfo fi in diTopics.GetFiles("*",SearchOption.AllDirectories) )
                {
                    string topicKey = fi.Name.ToUpper();

                    if( topicNames.Contains(topicKey) ) // duplicate topic
                        throw new Exception("You cannot have more than one topic named " + fi.Name);

                    bool addTopic = !_topics.ContainsKey(topicKey); // a new topic

                    if( !addTopic && _topics[topicKey].ModifiedDate != fi.LastWriteTimeUtc ) // a modified topic
                    {
                        _topics.Remove(topicKey);
                        addTopic = true;
                    }

                    if( addTopic )
                    {
                        _topics.Add(topicKey,new TopicPreprocessor(fi));
                        savePreprocessor = true;
                    }

                    topicNames.Add(topicKey);
                }
            }


            // remove any deleted images
            List<string> deletedImages = new List<string>();

            foreach( var kp in _images )
            {
                if( !File.Exists(kp.Value.Filename) )
                {
                    deletedImages.Add(kp.Key);
                    savePreprocessor = true;
                }
            }

            foreach( string name in deletedImages )
                _images.Remove(name);

            // process the images folder
            string imagesPath = Path.Combine(_projectPath,Constants.ImagesDirectoryName);

            if( Directory.Exists(imagesPath) )
            {
                DirectoryInfo diImages = new DirectoryInfo(imagesPath);
                HashSet<string> imageNames = new HashSet<string>();

                foreach( FileInfo fi in diImages.GetFiles("*",SearchOption.AllDirectories) )
                {
                    string imageKey = fi.Name.ToUpper();

                    if( imageNames.Contains(imageKey) ) // duplicate image
                        throw new Exception("You cannot have more than one image named " + fi.Name);

                    if( !_images.ContainsKey(imageKey) ) // a new image
                    {
                        _images.Add(imageKey,new ImagePreprocessor(fi));
                        savePreprocessor = true;
                    }

                    imageNames.Add(imageKey);
                }
            }


            if( savePreprocessor )
            {
                using( Stream stream = new FileStream(_preprocessorFilename,FileMode.Create,FileAccess.Write,FileShare.None) )
                    new BinaryFormatter().Serialize(stream,this);
            }
            
            return savePreprocessor;
        }

        public TopicPreprocessor GetTopic(string filename)
        {
            string topicKey = filename.ToUpper();

            if( !_topics.ContainsKey(topicKey) )
                throw new Exception("The topic filename could not be located: " + filename);

            return _topics[topicKey];
        }

        public HashSet<TopicPreprocessor> GetAllTopics()
        {
            HashSet<TopicPreprocessor> topicsHashSet = new HashSet<TopicPreprocessor>();

            foreach( var kp in _topics )
                topicsHashSet.Add(kp.Value);

            return topicsHashSet;
        }

        public HashSet<ImagePreprocessor> GetAllImages()
        {
            HashSet<ImagePreprocessor> imageHashSet = new HashSet<ImagePreprocessor>();

            foreach( var kp in _images )
                imageHashSet.Add(kp.Value);

            return imageHashSet;
        }
    }
}
