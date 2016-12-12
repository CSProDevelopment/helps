SET help_generator=".\Help Tools\Help Generator\bin\Release\Help Generator.exe"

del /s *.chm
del /s *.chw

%help_generator% /generate ".\CSBatch\CSBatch.hghelp"
%help_generator% /generate ".\CSConcat\CSConcat.hghelp"
%help_generator% /generate ".\CSDiff\CSDiff.hghelp"
%help_generator% /generate ".\CSEntry\CSEntry.hghelp"
%help_generator% /generate ".\CSExport\CSExport.hghelp"
%help_generator% /generate ".\CSFreq\CSFreq.hghelp"
%help_generator% /generate ".\CSIndex\CSIndex.hghelp"
%help_generator% /generate ".\CSPack\CSPack.hghelp"
%help_generator% /generate ".\CSReFmt\CSReFmt.hghelp"
%help_generator% /generate ".\CSSort\CSSort.hghelp"
%help_generator% /generate ".\CSTab\CSTab.hghelp"
%help_generator% /generate ".\GetStart\GetStart.hghelp"
%help_generator% /generate ".\MapView\MapView.hghelp"
%help_generator% /generate ".\ShpToMap\ShpToMap.hghelp"
%help_generator% /generate ".\TblView\TblView.hghelp"
%help_generator% /generate ".\TextView\TextView.hghelp"
%help_generator% /generate ".\TRSSetup\TRSSetup.hghelp"
%help_generator% /generate ".\TRSWin\TRSWin.hghelp"
