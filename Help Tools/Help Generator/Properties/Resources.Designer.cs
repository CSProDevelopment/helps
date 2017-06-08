﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Help_Generator.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Help_Generator.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap Checkmark {
            get {
                object obj = ResourceManager.GetObject("Checkmark", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Icon similar to (Icon).
        /// </summary>
        internal static System.Drawing.Icon Help_Generator {
            get {
                object obj = ResourceManager.GetObject("Help_Generator", resourceCulture);
                return ((System.Drawing.Icon)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;topic filename&gt;
        ///&lt;topic filename&gt; | &lt;title to override the topic title&gt;
        ///	&lt;subtopic filename&gt;
        ///	&lt;subtopic filename&gt; | &lt;title to override the subtopic title&gt;
        ///|| HelpFile.chm # help file&apos;s index to merge
        ///# comment.
        /// </summary>
        internal static string IndexHelp {
            get {
                return ResourceManager.GetString("IndexHelp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Title=&lt;helps title&gt;
        ///PdfTitle=&lt;optional PDF title; also an indication that a PDF should be created&gt;
        ///DefaultTopic=&lt;topic filename of the default topic&gt;
        ///DefinitionsFile=&lt;optional path with definitions that can be retrieved using the definition tag&gt;
        ///ResourceFile=&lt;optional path pointing to the header files used to define context sensitive helps&gt;
        ///ResourceFile=&lt;multiple ResourceFile entries are allowed&gt;
        ///# comment.
        /// </summary>
        internal static string SettingsHelp {
            get {
                return ResourceManager.GetString("SettingsHelp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;!doctype html&gt;
        ///&lt;html&gt;
        ///&lt;head&gt;&lt;meta http-equiv=&apos;Content-Type&apos; content=&apos;text/html; charset=UTF-8&apos; /&gt;&lt;title&gt;CSPro Help Generator Syntax&lt;/title&gt;&lt;/head&gt;
        ///&lt;body&gt;
        ///&lt;div style=&apos;word-wrap:break-word;margin:0px;padding:0px;border:0px;background-color:#ffffff;color:#000000;font-family:Courier New;font-size:10pt;&apos;&gt;
        ///
        ///&lt;p&gt;&lt;strong&gt;Page Title:&lt;/strong&gt; &amp;lt;title&amp;gt;page title&amp;lt;/title&amp;gt; &amp;lt;title noheader&amp;gt;page title (not displayed)&amp;lt;/title&amp;gt;&lt;/p&gt;
        ///
        ///&lt;p&gt;&lt;strong&gt;Context Sensitive Help:&lt;/strong&gt; &amp;lt;context HIDD_F [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string SyntaxHelp {
            get {
                return ResourceManager.GetString("SyntaxHelp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to | &lt;chapter name&gt;
        ///	&lt;topic filename&gt;
        ///	&lt;topic filename&gt; | &lt;title to override the topic title&gt;
        ///	| &lt;subchapter name&gt;
        ///		&lt;topic filename&gt;
        ///|| HelpFile.chm # help file to list as a chapter
        ///# comment.
        /// </summary>
        internal static string TableOfContentsHelp {
            get {
                return ResourceManager.GetString("TableOfContentsHelp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to body
        ///{
        ///	margin: 10px;
        ///	padding: 0px;
        ///}
        ///
        ///body, td, h1, h2
        ///{
        ///	background-color: white;
        ///	font-family: Calibri, Candara, Segoe, &quot;Segoe UI&quot;, Optima, Arial, Helvetica, sans-serif;
        ///	font-size: 14px;
        ///	color: black;
        ///}
        ///
        ///h1
        ///{
        ///    text-align: center;
        ///    font-size: 4em;
        ///    font-weight: bold;
        ///    color: #003050;
        ///    page-break-before: always;
        ///}    
        ///
        ///a { color: #5080ff; }
        ///
        ///li { margin: 0 0 10px 0; }
        ///
        ///table { margin: 0; }
        ///th, td { text-align: left; vertical-align:top; padding-right: 20px; }
        ///t [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string TopicStylesheet {
            get {
                return ResourceManager.GetString("TopicStylesheet", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;UTF-8&quot;?&gt;
        ///&lt;configuration&gt;
        ///	&lt;system.webServer&gt;
        ///		&lt;defaultDocument enabled=&quot;true&quot;&gt;
        ///			&lt;files&gt;
        ///				&lt;clear/&gt;
        ///				&lt;add value=&quot;%template-topic%&quot;/&gt;
        ///			&lt;/files&gt;      
        ///		&lt;/defaultDocument&gt;
        ///	&lt;/system.webServer&gt;
        ///&lt;/configuration&gt;
        ///.
        /// </summary>
        internal static string WebConfig {
            get {
                return ResourceManager.GetString("WebConfig", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap WebsiteChapterClosed {
            get {
                object obj = ResourceManager.GetObject("WebsiteChapterClosed", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap WebsiteChapterOpen {
            get {
                object obj = ResourceManager.GetObject("WebsiteChapterOpen", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to .toc_ul { padding: 0 20px 0; }
        ///.toc_li_chapter { margin: 5px 0 5px 0; list-style-image: url(&apos;hgweb_chapter_closed.png&apos;); }
        ///.toc_li_chapter_current { margin: 5px 0 5px 0; list-style-image: url(&apos;hgweb_web_chapter_open.png&apos;); }
        ///.toc_li_topic { margin: 5px 0 5px 0; list-style-image: url(&apos;hgweb_web_topic.png&apos;); }
        ///.toc_li_topic_current { margin: 5px 0 5px 0; list-style-image: url(&apos;hgweb_web_topic_current.png&apos;); }
        ///
        ///#container, html, body
        ///{
        ///    height: 99%;
        ///}
        ///
        ///#left
        ///{
        ///	float: left;
        ///    width: 200px;
        /// [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string WebsiteStylesheet {
            get {
                return ResourceManager.GetString("WebsiteStylesheet", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap WebsiteTopic {
            get {
                object obj = ResourceManager.GetObject("WebsiteTopic", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap WebsiteTopicCurrent {
            get {
                object obj = ResourceManager.GetObject("WebsiteTopicCurrent", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
    }
}
