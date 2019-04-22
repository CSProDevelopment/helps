using ScintillaNET;

namespace Colorizer
{
    interface TextModifiedHandlerInterface
    {
        void SetTextModified(bool modified);
    };

    class EditControl : Scintilla
    {
        private TextModifiedHandlerInterface _textModifiedHandler = null;

        public EditControl()
        {
            this.StyleResetDefault();
            this.Styles[ScintillaNET.Style.Default].Font = "Consolas";
            this.Styles[ScintillaNET.Style.Default].Size = 10;
            this.StyleClearAll();

            foreach( var margin in this.Margins )
                margin.Width = 0;

            this.CharAdded += new System.EventHandler<ScintillaNET.CharAddedEventArgs>(EditControl_CharAdded);
            this.SavePointLeft += EditControl_SavePointLeft;
            this.SavePointReached += EditControl_SavePointReached;
        }

        private void EditControl_CharAdded(object sender,ScintillaNET.CharAddedEventArgs e)
        {
            if( e.Char == '\n' )
            {
                this.Lines[this.CurrentLine].Indentation = this.Lines[this.CurrentLine - 1].Indentation;
                this.CurrentPosition = this.CurrentPosition + ( this.Lines[this.CurrentLine].Indentation / this.TabWidth );
                this.SelectionStart = this.CurrentPosition;
            }
        }

        public void SetTextModifiedHandler(TextModifiedHandlerInterface textModifiedHandler)
        {
            _textModifiedHandler = textModifiedHandler;
        }

        private void EditControl_SavePointReached(object sender,System.EventArgs e)
        {
            if( _textModifiedHandler != null )
                _textModifiedHandler.SetTextModified(false);
        }

        private void EditControl_SavePointLeft(object sender,System.EventArgs e)
        {
            if( _textModifiedHandler != null )
                _textModifiedHandler.SetTextModified(true);
        }
    }
}
