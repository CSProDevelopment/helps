SET CSDocument="C:\Program Files (x86)\CSPro 8.0\CSDocument.exe"

cd /d %~dp0
del /q /f /s "Outputs\CHM" 

%CSDocument% -build "Compiled HTML Help" -input "CSBatch\CSBatch.csdocset" "CSCode\CSCode.csdocset" "CSConcat\CSConcat.csdocset" "CSDeploy\CSDeploy.csdocset" "CSDiff\CSDiff.csdocset" "CSDocument\CSDocument.csdocset" "CSEntry\CSEntry.csdocset" "CSExport\CSExport.csdocset" "CSFreq\CSFreq.csdocset" "CSIndex\CSIndex.csdocset" "CSPack\CSPack.csdocset" "CSPro\CSPro.csdocset" "CSReFmt\CSReFmt.csdocset" "CSSort\CSSort.csdocset" "CSTab\CSTab.csdocset" "CSView\CSView.csdocset" "CSWeb\CSWeb.csdocset" "DataViewer\DataViewer.csdocset" "Excel2CSPro\Excel2CSPro.csdocset" "GetStart\GetStart.csdocset" "ParadataConcat\ParadataConcat.csdocset" "ParadataViewer\ParadataViewer.csdocset" "TblView\TblView.csdocset" "TextView\TextView.csdocset"
