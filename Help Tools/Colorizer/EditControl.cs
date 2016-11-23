using ScintillaNET;

namespace Colorizer
{
    class EditControl : Scintilla
    {
        public EditControl()
        {
            this.StyleResetDefault();
            this.Styles[ScintillaNET.Style.Default].Font = "Courier New";
            this.Styles[ScintillaNET.Style.Default].Size = 10;
            this.StyleClearAll();

            foreach( var margin in this.Margins )
                margin.Width = 0;

            this.CharAdded += new System.EventHandler<ScintillaNET.CharAddedEventArgs>(EditControl_CharAdded);
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
    }
}
